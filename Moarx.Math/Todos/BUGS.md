# Moarx.Math – Bekannte Bugs

Quelle: Strukturbewertung von `Moarx.Math` im Vergleich mit etablierten Bibliotheken (pbrt-v4,
System.Numerics, GLM), 2026-08-08. Noch nicht gefixt — zum Abarbeiten wie
`Moarx.Math.Tests/Todos/TEST_COVERAGE_CHECKLIST.md`.

## 1. `Vector3D<T>.Empty` / `Vector2D<T>.Empty` haben den falschen Typ

- [ ] `Vector3D.cs:25` — `public static readonly Point3D<T> Empty = new();` innerhalb von `Vector3D<T>`.
- [ ] `Vector2D.cs:21` — `public static readonly Point2D<T> Empty = new();` innerhalb von `Vector2D<T>`.

Copy-Paste-Fehler aus `Point3D`/`Point2D`. Das Feld liefert de facto einen `Point3D<T>`/`Point2D<T>`
statt einen `Vector3D<T>`/`Vector2D<T>`. Fällt nicht auf, weil die bestehenden Tests
(`Vector3DTests.cs:39`, `Vector2DTests.cs:35`) `var p = Vector3D<double>.Empty;` schreiben — `var`
infert dadurch klammheimlich `Point3D<double>`/`Point2D<double>` statt `Vector3D<double>`/`Vector2D<double>`,
die Tests prüfen also nicht das, was der Name suggeriert.

**Fix:** Feldtyp auf `Vector3D<T>` bzw. `Vector2D<T>` ändern. Danach `var p = Vector3D<double>.Empty;`
in den betroffenen Tests neu kompilieren lassen (deckt evtl. weitere versteckte Fehlnutzung auf) und
die Assertions ggf. anpassen.

## 2. `rng.FloatOneMinusEpsilon` klemmt nicht

- [ ] `rng.cs:11` — `float FloatOneMinusEpsilon = 1 - float.Epsilon;`

`float.Epsilon` ist die kleinste denormalisierte positive float-Zahl (~1.4e-45), nicht die
Maschinen-Epsilon. `1f - float.Epsilon` rundet exakt auf `1.0f` zurück. Der Clamp in `Uniform()`
(`Math.Min(FloatOneMinusEpsilon, ...)`, `rng.cs:27`) greift dadurch nie — `Uniform()` kann `1.0`
zurückgeben. pbrt nutzt bewusst `0x1.fffffep-1` (≈ 0.99999994) für diese Konstante, genau um das zu
verhindern (Sampling-Code, der z.B. durch `1 - u` teilt, würde sonst durch 0 teilen können).

**Fix:** `FloatOneMinusEpsilon` auf den echten „ein ULP unter 1.0"-Wert setzen, z.B.
`BitConverter.Int32BitsToSingle(BitConverter.SingleToInt32Bits(1f) - 1)` oder das pbrt-Literal
`0.99999994f` direkt übernehmen. Regressionstest: viele Iterationen von `Uniform()` gegen `< 1.0`
prüfen (aktuell mit dem Bug technisch nicht garantiert, praktisch aber selten reproduzierbar — ggf.
über eine deterministische state-Manipulation gezielt den Fall erzwingen).

## 3. `SquareMatrix`: mutable Array in „readonly struct" → Aliasing-Bug

- [ ] `SquareMatrix.cs` — Operatoren `+` (Zeile ~298), `*(SquareMatrix, double)` (Zeile ~306),
  `/(SquareMatrix, double)` (Zeile ~327).

`SquareMatrix` ist ein `readonly struct`, hält aber sein `_matrix`-Feld als `double[,]` (Referenztyp).
Die genannten Operatoren mutieren `m._matrix` direkt in-place und geben `m` zurück:

```csharp
public static SquareMatrix operator +(SquareMatrix m, SquareMatrix m2) {
    for (...) m._matrix[i, j] += m2._matrix[i, j];
    return m;
}
```

Da C#-Structs beim Kopieren nur die Referenz auf `double[,]` kopieren (kein Deep-Copy), teilen sich
alle Kopien desselben `SquareMatrix`-Werts dasselbe Backing-Array. `a + b` mutiert dadurch `a` selbst
— und jede andere Stelle, die eine Kopie von `a` hält (z.B. `Transform._m`), sieht die Änderung
ebenfalls. Widerspricht der Werttyp-Erwartung von `readonly struct`. Die multiplikativen
Matrix-Matrix-Operatoren (`Mul3x3`/`Mul4x4`/`MulNxN`) sind davon **nicht** betroffen — die legen
bereits korrekt ein neues Array an.

**Fix:** In `+`, `*(scalar)`, `/(scalar)` ein neues `double[,]` anlegen statt `m._matrix` in-place zu
verändern (gleiches Muster wie `Mul3x3`/`Mul4x4`/`MulNxN`). Regressionstest: zwei `SquareMatrix`-Werte
`a`, `b` aus derselben Quellmatrix ableiten (z.B. `var b = a;`), `_ = a + irgendwas;` ausführen und
prüfen, dass `b` unverändert bleibt.
