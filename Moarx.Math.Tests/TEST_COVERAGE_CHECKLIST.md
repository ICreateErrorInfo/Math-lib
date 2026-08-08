# Moarx.Math.Tests – Checkliste offener Punkte

Quelle: Analyse von `Moarx.Math` vs. `Moarx.Math.Tests` (2026-08-08).

## 1. Fehlender Bug-Fix zuerst (blockiert saubere Tests) — ✅ erledigt (2026-08-08)

- [x] `Normal3D<T>`-Ctor normalisiert jetzt den Eingabevektor (`_vector = vector / T.Sqrt(vector.GetLengthSquared())`). Damit sind `GetLength()`/`GetLengthSquared() == 1` tatsächlich korrekt (pbrt-Konvention: Normal3D ist immer Einheitsvektor). `Normal3DTests.TestLength` prüft jetzt zusätzlich die normalisierten Komponentenwerte statt nur den (vorher gefakten) Längenwert.
- [x] `DirectionCone`-Ctor-Bug behoben: `W = w.Normalize();` (vorher wurde versehentlich nur der lokale Parameter `w` neu zugewiesen, das Feld `W` blieb immer `(0,0,0)`).
- [x] `DirectionCone.Union()`: unvollständiger Zweig wirft jetzt explizit `NotImplementedException` statt still `null` zurückzugeben (Rotation von `wr` um `thetaR` ist weiterhin nicht implementiert — bewusst offen gelassen, siehe Kommentar im Code).
- [x] `Vector3D<T>.IsNormalized()` nutzt jetzt einen Toleranzvergleich (`Math.Abs(GetLengthSquared() - 1) < 1e-6`) statt exakter Gleichheit.

Verifiziert: `Moarx.Math.Tests` (143/143 grün) und `Raytracing` bauen fehlerfrei nach den Änderungen.

## 2. Komplett fehlende Testdateien (neue Typen ohne jeden Test) — ✅ erledigt (2026-08-08)

Hinweis: Bounds2D<T> hat in Wirklichkeit eine viel kleinere API als Bounds3D<T> (nur Ctor, PMin/PMax,
ToRectangle, Diagonal, statisches Intersect, Indexer — kein Union/Overlaps/Inside/Expand/Volume/etc.),
daher war eine 1:1-Portierung von `Bound2DTests.cs` (Legacy) nicht möglich; stattdessen wurde ein
schlankerer, zur tatsächlichen API passender Testfile geschrieben.

- [x] `Bounds2D<T>` — `Bounds2DTests.cs` (11 Tests)
- [x] `MathmaticMethods.cs` — `MathmaticMethodsTests.cs` (25 Tests)
- [x] `DirectionCone` — `DirectionConeTests.cs` (15 Tests, inkl. `NotImplementedException`-Zweig von `Union()`)
- [x] `Ellipse2D<T>` — `Ellipse2DTests.cs`
- [x] `Line2D<T>` — `Line2DTests.cs`
- [x] `Triangle2D<T>` — `Triangle2DTests.cs`
- [x] `CubicBezierCurve2D<T>` — `CubicBezierCurve2DTests.cs`
- [x] `QuadBezierCurve2D<T>` — `QuadBezierCurve2DTests.cs`
- [x] `rng.cs` (PCG32) — `RngTests.cs` (Determinismus verifiziert: `rng` hat keine Seed-API, jede Instanz startet mit demselben Fixed-State — zwei frische Instanzen liefern identische Sequenzen)
- [x] `VectorExtensions.cs` — `VectorExtensionsTests.cs` (generischer Pfad + `float`-Fastpath gegeneinander geprüft; `Normalize()` eines Nullvektors wirft `ArgumentOutOfRangeException`, kein stilles NaN — durch den NaN-Guard im Vector-Ctor)
- [x] `BoundsExtensions.cs` — `BoundsExtensionsTests.cs`

**Bug gefunden und gefixt:** `Bounds3D<T>.DistanceSquared` (genutzt von `BoundsExtensions.Distance`) klammerte
die Achsen-Differenzen nicht mit `0`, bevor quadriert wurde — für Punkte **innerhalb** der Bounds lieferte
die Methode einen falschen, positiven Wert statt `0` (fehlendes `Max(0, ...)`, vgl. pbrt-Referenz). Fix in
`Bounds3D.cs`, direkte Tests dafür in `Bounds3DTests.cs` ergänzt (`TestDistanceSquaredForPointInsideIsZero`,
`...OnBoundary...`, `...OnOneAxis`, `...OnAllAxes`).

Verifiziert: `Moarx.Math.Tests` (238/238 grün) und `Raytracing` bauen fehlerfrei.

## 3. Bestehende Testdateien mit großen Lücken

### Vector3D
- [ ] `Reflect`
- [ ] `Refract<U>`
- [ ] `AngleBetween`
- [ ] `Permute`
- [ ] `MaxDimension`
- [ ] `Abs`
- [ ] `NearZero`
- [ ] `RandomInUnitSphere` / `Random`

### Vector2D
- [ ] `GetLength`/`Normalize` (via `VectorExtensions`, s.o.)

### Point2D
- [ ] `Min`
- [ ] `Max`

### Point3D
- [ ] `SmalestComponents` / `GreatestComponents`
- [ ] `Permute`
- [ ] `Round`

### Normal3D
- [ ] `FaceForward`
- [ ] echte `GetLength`/`GetLengthSquared`-Tests (nach Bugfix aus Punkt 1)

### Bounds3D
- [ ] `IntersectP(Ray, out, out)` — Ray-Intersection (wichtigste Methode des Typs, komplett ungetestet)
- [ ] `IntersectP(Ray, Vector3D, bool[])`
- [ ] `BoundingSphere()`
- [ ] `TestLerp` reaktivieren (aktuell auskommentiert in `Bounds3DTests.cs:188-200`) — Referenzwerte ggf. aus `Math-lib.Tests\Bound3DTests.cs` übernehmen
- [ ] NaN-Guard auf `PMin`/`PMax` verifizieren
- [ ] Degenerierte/invertierte Bounds (PMin > PMax, Volumen 0)

### SquareMatrix
- [ ] `Mul<T>(T[] v)` (Matrix-Vektor-Multiplikation) — Referenzwerte ggf. aus `Math-lib.Tests\MatrixTests.cs`
- [ ] `==` / `!=`
- [ ] `Inverse()` bei singulärer Matrix (Determinante 0 → `null`)

### Transform
- [ ] `Transpose()`
- [ ] `SwapHandness()`
- [ ] `Rotate(theta, axis)` (Rotation um beliebige Achse)
- [ ] `RotateFromTo`
- [ ] `LookAt`
- [ ] `Orthographic`
- [ ] `Perspective`
- [ ] Operator `*` mit `Vector3D`, `Normal3D`, `Ray`, `Bounds3D`, `Transform`
- [ ] `==` / `!=`
- [ ] `Inverse()`-Korrektheit für Rotate/Scale (nicht nur Ctor-Fall)
- [ ] `IsIdentity() == false`-Fall

### Rectangle2D
- [ ] `Intersect`
- [ ] `Union`
- [ ] `IntersectsWith`
- [ ] `Contains` (3 Overloads)
- [ ] `Inflate` (2 Overloads)
- [ ] `Offset` (2 Overloads)
- [ ] `IsEmpty` / `Empty`
- [ ] `ToString`
- [ ] statische `Rectangle2D.Create`-Overloads (3 Stück)

### Ray
- [ ] `TMax`/`Time` Default-Werte und Verwendung
- [ ] `ToString`

## 4. Edge Cases (typübergreifend)

- [ ] Zero-Vektoren bei `Normalize`/`GetLength` (Division durch 0 → NaN/Infinity)
- [ ] Degenerierte/invertierte `Bounds2D`/`Bounds3D`
- [ ] `Infinity`-Werte (z.B. `Ray.TMax`-Default, `Bounds3D`-Sentinel via `MinValue`/`MaxValue`)
- [ ] Singuläre Matrix bei `SquareMatrix.Inverse()`
- [ ] Indexer Out-of-Range (`IndexOutOfRangeException`) für alle Typen mit `this[int]`

## 5. Generics-Abdeckung (systemische Lücke)

- [ ] Mindestens einen generischen Testtyp pro Klasse zusätzlich mit `float` instanziieren (aktuell fast ausschließlich `double`, s. CLAUDE.md: „work with float/double interchangeably")
- [ ] `int`-Instanziierung für mind. eine weitere Klasse zusätzlich zu `Rectangle2D<int>` (z.B. Verhalten bei Integer-Division prüfen)

## 6. Portierbare Legacy-Tests (Math-lib.Tests → Moarx.Math.Tests)

- [ ] `Bound2DTests.cs` (20 Tests) → Basis für `Bounds2D<T>`-Suite (Punkt 2)
- [ ] `Bound3DTests.cs` — `IntersectP1`/`IntersectP2`, `Lerp`, `IsNaN` → Basis für Bounds3D-Lücken (Punkt 3)
- [ ] `Normal3DTests.cs` (32 Tests) — volle Vergleichsoperator-Matrix, `Cross`/`Dot` beidseitig → Basis für Normal3D-Erweiterung
- [ ] `Vector3DTests.cs`/`Vector2DTests.cs` — `Reflect`, `MaxDimension`, `Permute`, `RandomInUnitSphere` als Vorlage (Achtung: `Ceiling`/`Floor`/`Clamp`/`Sqrt` existieren auf neuem Typ nicht, nicht 1:1 portierbar)
- [ ] `MatrixTests.cs` — `Mul`-Test gegen `Point3D`/`Vector3D` als Vorlage für `SquareMatrix.Mul<T>`
