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

### Vector3D — ✅ erledigt (2026-08-08)
- [x] `Reflect`
- [x] `Refract<U>` — **Bug gefunden und gefixt** (siehe unten)
- [x] `AngleBetween`
- [x] `Permute`
- [x] `MaxDimension`
- [x] `Abs`
- [x] `NearZero`
- [x] `RandomInUnitSphere` / `Random`
- [x] `IsNormalized` (Ergänzung, war in Punkt 1 nur gefixt, nicht getestet)
- [x] Indexer Out-of-Range

**Bug gefunden und gefixt:** `Vector3D<T>.Refract<U>` berechnete den gebrochenen Vektor falsch. Statt den
Skalarterm `(eta*cosi - sqrt(cost2))` mit dem Normalenvektor zu multiplizieren (Standard-Snellsche-Brechungsformel),
wurde `new Vector3D<U>(eta*cosi)` gebildet — ein Vektor mit allen drei Komponenten `= eta*cosi`, unabhängig von
der tatsächlichen Normalenrichtung. Beispiel: `v=(0,-1,0)`, `n=(0,1,0)`, `eta=1` (kein Brechungsindex-Unterschied,
sollte `v` unverändert lassen) lieferte `(1,-1,1)` statt `(0,-1,0)`. Der Bug bestand identisch bereits im
Legacy-`Math-lib\Vector3D.cs` (keine Migrationsregression) und ist **aktiv im Renderer**:
`Raytracing\Materials\Dielectric.cs` (Glas/Wasser-Material) ruft `Refract` direkt auf — betraf also sichtbar
die Glasbrechung beim Rendern. Fix nur in `Moarx.Math\Vector3D.cs` (aktiver Code), Legacy-`Math-lib` bewusst
unangetastet gelassen. Neue Tests: `TestRefractAtNormalIncidenceIsUnchanged`, `TestRefractBendsTowardNormalForDenserMedium`
(inkl. Unit-Length-Check), `TestRefractTotalInternalReflectionReturnsZero`.

### Vector2D — ✅ erledigt (2026-08-08)
- [x] `GetLength`/`Normalize` (via `VectorExtensions`, s.o.)
- [x] Explizite Casts (`(Vector2D<int>)`, `(Vector2D<double>)`)
- [x] Indexer Out-of-Range

Verifiziert: `Moarx.Math.Tests` (256/256 grün) und `Raytracing` bauen fehlerfrei.

### Point2D — ✅ erledigt (2026-08-08)
- [x] `Min`
- [x] `Max`
- [x] Explizite Casts (`(Point2D<int>)`, `(Point2D<double>)`)
- [x] Indexer Out-of-Range

### Point3D — ✅ erledigt (2026-08-08)
- [x] `SmalestComponents` / `GreatestComponents`
- [x] `Permute`
- [x] `Round`
- [x] Indexer Out-of-Range

Kein Bug gefunden. Verifiziert: `Moarx.Math.Tests` (266/266 grün).

### Normal3D — ✅ erledigt (2026-08-08)
- [x] `FaceForward`
- [x] echte `GetLength`/`GetLengthSquared`-Tests (bereits in Punkt 1 ergänzt, zusammen mit dem Ctor-Normalisierungs-Fix)

### Bounds3D — ✅ erledigt (2026-08-08)
- [x] `IntersectP(Ray, out, out)` — Hit- und Miss-Fall (Miss-Fall nutzt achsenparallelen Strahl mit 0-Richtungskomponente, deckt die Infinity-Handhabung ab)
- [x] `IntersectP(Ray, Vector3D, bool[])` — Hit- und Miss-Fall
- [x] `BoundingSphere()` — Normalfall + Degenerierte-Bounds-Fall (Default-Ctor: Center liegt nicht in den eigenen Sentinel-Bounds → Radius 0)
- [x] `TestLerp` reaktiviert (referenzierte zuvor eine nicht existierende `Mathe`-Klasse, jetzt `MathmaticMethods.Lerp`)
- [x] NaN-Guard auf `PMin`/`PMax` verifiziert
- [x] Degenerierte/invertierte Bounds — dokumentiert: Object-Initializer-Syntax umgeht die Sortier-Logik des Zwei-Punkt-Ctors, `Volume()` kann dadurch negativ werden (kein Bug-Fix beauftragt, nur als bestehendes Verhalten getestet)

Kein Bug gefunden. Verifiziert: `Moarx.Math.Tests` (284/284 grün) und `Raytracing` bauen fehlerfrei.

### SquareMatrix — ✅ erledigt (2026-08-08)
- [x] `Mul<T>(T[] v)` (Matrix-Vektor-Multiplikation, inkl. Identitätsmatrix-Fall)
- [x] `==` / `!=`
- [x] `Inverse()` bei singulärer Matrix für 3x3, 4x4 und NxN (5x5) → `null`

Kein Bug gefunden.

### Transform — ✅ erledigt (2026-08-08)
- [x] `Transpose()`
- [x] `SwapHandness()`
- [x] `Rotate(theta, axis)` (Rotation um beliebige Achse) — **Bug gefunden und gefixt** (siehe unten)
- [x] `RotateFromTo` — **derselbe Bug gefunden und gefixt**
- [x] `LookAt`
- [x] `Orthographic`
- [x] `Perspective`
- [x] Operator `*` mit `Vector3D`, `Normal3D`, `Ray`, `Bounds3D`, `Transform`
- [x] `==` / `!=`
- [x] `Inverse()`-Korrektheit für Rotate/Scale (Round-Trip-Test, nicht nur Ctor-Fall)
- [x] `IsIdentity() == false`-Fall

**Bug gefunden und gefixt:** `Transform.Rotate(sinTheta,cosTheta,axis)` (und die `Rotate(theta,axis)`-Überladung,
die diese aufruft) sowie `Transform.RotateFromTo` bauen ihre 4x4-Matrix aus einem frischen `double[4,4]`
(alle Einträge starten bei `0`) und füllen nur die obere-linke 3x3-Rotationsmatrix — die letzte Zeile
(`m[3,0..3]`) wird nie beschrieben und bleibt `[0,0,0,0]` statt der für eine affine Transformation
erforderlichen `[0,0,0,1]`. Da der `Point3D`-Transformoperator durch die homogene Koordinate `wp = m[3,*]·p + m[3,3]`
dividiert, war `wp` dadurch immer `0` → jede Punkt-Transformation mit diesen beiden Methoden warf eine
`DivideByZeroException`. `Vector3D`/`Normal3D`-Transformationen waren nicht betroffen (deren Operatoren
greifen nie auf Zeile/Spalte 3 zu). Beide Methoden waren zum Fundzeitpunkt nirgends im Code aufgerufen
(kein aktiver Rendering-Bug, aber ein sofortiger Crash beim ersten Gebrauch). Fix: `m[3,3] = 1` (und zur
Klarheit `m[3,0..2] = 0`) in `Rotate(...)` ergänzt, `r[3,3] = 1` in `RotateFromTo` ergänzt. Neue Tests
(`TestRotateAroundZAxisMatchesRotateZ`, `TestRotateInverseRoundTrips`, `TestRotateFromToMapsFromOntoTo`,
`TestRotateFromToWithOppositeAxis`) transformieren `Point3D`-Werte über diese Methoden und hätten vor dem
Fix mit `DivideByZeroException` fehlgeschlagen.

Verifiziert: `Moarx.Math.Tests` (304/304 grün) und `Raytracing` bauen fehlerfrei.

### Rectangle2D — ✅ erledigt (2026-08-08)
- [x] `Intersect`
- [x] `Union`
- [x] `IntersectsWith`
- [x] `Contains` (3 Overloads) — **Bug gefunden und gefixt** (siehe unten)
- [x] `Inflate` (2 Overloads)
- [x] `Offset` (2 Overloads) — **Bug gefunden und gefixt** (siehe unten)
- [x] `IsEmpty` / `Empty`
- [x] `ToString`
- [x] statische `Rectangle2D.Create`-Overloads (3 Stück)

**Bug gefunden und gefixt (1/2):** `Contains(Rectangle2D<T> rect)` verglich im letzten Check `rect.Bottom`
(Y-Achse) fälschlich gegen `Right` (X-Achse) statt gegen `Bottom` — Kopierfehler. Beispiel: äußeres Rechteck
`(X=0,Y=0,W=10,H=3)` (`Right=10, Bottom=3`), inneres Rechteck `(X=2,Y=2,W=2,H=2)` (`Bottom=4`, ragt über den
unteren Rand hinaus) — `Contains` lieferte fälschlich `true`, weil `4 <= Right(10)` zufällig zutraf, statt
korrekt gegen `Bottom(3)` zu prüfen. Fix: `rect.Bottom <= Right` → `rect.Bottom <= Bottom`. Regressionstest:
`TestContainsRectangleExtendingBeyondBottomIsNotContained`.

**Bug gefunden und gefixt (2/2):** `Offset(Point2D<T> pos)` war als `void` deklariert und rief intern
`Offset(x, y)` auf, das ein neues (verschobenes) `Rectangle2D<T>` **zurückgibt** — auf dem unveränderlichen
`readonly record struct` wurde dieser Rückgabewert einfach verworfen, wodurch der Aufruf ein kompletter,
stiller No-Op war. Fix: Signatur zu `public Rectangle2D<T> Offset(Point2D<T> pos) => Offset(pos.X, pos.Y);`
geändert. Regressionstest: `TestOffsetByPoint`. Beide Bugs waren zum Fundzeitpunkt nirgends im Code
aufgerufen (kein aktiver Rendering-Bug).

Verifiziert: `Moarx.Math.Tests` (326/326 grün) und `Raytracing` bauen fehlerfrei.

### Ray — ✅ erledigt (2026-08-08)
- [x] `TMax`/`Time` Default-Werte und Verwendung
- [x] `ToString`

Kein Bug gefunden.

## 4. Edge Cases (typübergreifend) — ✅ erledigt (2026-08-08)

- [x] Zero-Vektoren bei `Normalize`/`GetLength` (bereits in Punkt 2 erledigt — wirft `ArgumentOutOfRangeException` statt stillem NaN, s. `VectorExtensionsTests`)
- [x] Degenerierte/invertierte `Bounds2D`/`Bounds3D` (`Bounds3D` bereits in Punkt 3 erledigt; `Bounds2D` jetzt ergänzt: `TestInvertedBoundsDiagonalIsNegative`)
- [x] `Infinity`-Werte — `Ray.TMax`-Default bereits in Punkt 3 erledigt (`RayTests`), `Bounds2D`/`Bounds3D`-Sentinel via `MinValue`/`MaxValue` bereits abgedeckt (`TestEmptyBounds2D`/`TestEmptyBound3D`)
- [x] Singuläre Matrix bei `SquareMatrix.Inverse()` (bereits in Punkt 3 erledigt)
- [x] Indexer Out-of-Range für alle Typen mit `this[int]` — Audit aller Indexer-Typen ergab drei bislang ungetestete: `Bounds3D`, `Normal3D`, `SquareMatrix` (2D-Indexer `this[int,int]`, ungeprüfter Zugriff auf das darunterliegende Array). Alle drei jetzt ergänzt (`Vector2D`/`Vector3D`/`Point2D`/`Point3D`/`Bounds2D`/`CubicBezierCurve2D`/`QuadBezierCurve2D` waren bereits in früheren Punkten abgedeckt).

Kein neuer Bug gefunden. Verifiziert: `Moarx.Math.Tests` (330/330 grün).

## 5. Generics-Abdeckung (systemische Lücke)

- [ ] Mindestens einen generischen Testtyp pro Klasse zusätzlich mit `float` instanziieren (aktuell fast ausschließlich `double`, s. CLAUDE.md: „work with float/double interchangeably")
- [ ] `int`-Instanziierung für mind. eine weitere Klasse zusätzlich zu `Rectangle2D<int>` (z.B. Verhalten bei Integer-Division prüfen)

## 6. Portierbare Legacy-Tests (Math-lib.Tests → Moarx.Math.Tests)

- [ ] `Bound2DTests.cs` (20 Tests) → Basis für `Bounds2D<T>`-Suite (Punkt 2)
- [ ] `Bound3DTests.cs` — `IntersectP1`/`IntersectP2`, `Lerp`, `IsNaN` → Basis für Bounds3D-Lücken (Punkt 3)
- [ ] `Normal3DTests.cs` (32 Tests) — volle Vergleichsoperator-Matrix, `Cross`/`Dot` beidseitig → Basis für Normal3D-Erweiterung
- [ ] `Vector3DTests.cs`/`Vector2DTests.cs` — `Reflect`, `MaxDimension`, `Permute`, `RandomInUnitSphere` als Vorlage (Achtung: `Ceiling`/`Floor`/`Clamp`/`Sqrt` existieren auf neuem Typ nicht, nicht 1:1 portierbar)
- [ ] `MatrixTests.cs` — `Mul`-Test gegen `Point3D`/`Vector3D` als Vorlage für `SquareMatrix.Mul<T>`
