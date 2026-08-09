# Moarx.Math – Verbesserungsvorschläge

Quelle: Strukturbewertung von `Moarx.Math` im Vergleich mit etablierten Bibliotheken (pbrt-v4,
System.Numerics, GLM, Unity.Mathematics), 2026-08-08. Enthält keine Bugs (siehe dafür `BUGS.md` im
selben Ordner), sondern Konsistenz-, Performance- und Struktur-Verbesserungen. Nicht priorisiert nach
Dringlichkeit — eher als Backlog zu verstehen.

## 1. Werttyp/Referenztyp-Inkonsistenz

- [ ] `Point*`/`Vector*`/`Normal3D`/`Bounds*` sind `readonly record struct`, aber `Ray`, `Transform`,
  `SquareMatrix`, `DirectionCone`, `MathmaticMethods` sind `class`. In einem Raytracer, der pro
  Pixel/Bounce Rays und Transforms erzeugt, bedeutet das unnötige Heap-Allokationen + GC-Druck. pbrt
  (C++) hält diese Typen als Stack-Values. Falls Performance relevant wird (`BenchmarkTests`-Projekt
  existiert bereits), wäre eine Umstellung von `Ray`/`Transform`/`SquareMatrix`/`DirectionCone` auf
  `readonly struct` der erste Hebel.

## 2. Halbe Generizität

- [ ] `SquareMatrix` ist trotz des generischen `INumber<T>`-Stils im Rest der Lib hart auf
  `double[,]` verdrahtet. `Mul<T>(T[] v)` behilft sich mit `Convert.ChangeType` (Boxing + Reflection)
  pro Element, um das zu kaschieren — spürbar teurer als nötig und stilistisch ein Bruch.
- [ ] `Ray` ist ebenfalls nicht generisch (nur `double`), obwohl `Point3D<T>`/`Vector3D<T>` generisch
  sind, die es intern nutzt.
- [ ] Da `Transform`, `Ray` und `SquareMatrix` — also praktisch alles, was im Renderer aktiv genutzt
  wird — fest auf `double` fixiert sind, bringt die Generizität der reinen Datencontainer
  (`Point2D<T>`, `Bounds3D<T>` etc.) aktuell wenig praktischen Nutzen. Lohnt sich nur, falls
  tatsächlich mal mit `float` gerendert werden soll; andernfalls Doku-Kommentar ergänzen, der das als
  bewusste Designentscheidung festhält, statt dass es wie eine halbfertige Migration wirkt.

## 3. `Ray` bricht den Immutability-Stil

- [ ] `Ray.cs` — `public double TMax; public double Time;` sind public settable Felder ohne NaN-Guard,
  während sämtliche anderen Typen (`Point*`, `Vector*`, `Bounds*`) strikt immutable mit NaN-Guard in
  jedem Setter sind. Falls das ein bewusster pbrt-Kompromiss ist (pbrt's `Ray::tMax` ist ebenfalls
  mutable, wird während der Traversierung laufend verkürzt), zumindest als Kommentar festhalten, damit
  klar ist, dass die Inkonsistenz beabsichtigt ist.

## 4. Tote/falsche Usings

- [ ] `Transform.cs:1-2` — `using System.ComponentModel.Design;` und `using System.Reflection.Emit;`
  sind ungenutzt (vermutlich ein Auto-Import-Unfall wegen Namenskollision mit `Transform`/`Design`).
  Entfernen.

## 5. Uneinheitliche API-Breite zwischen 2D/3D-Typen

- [ ] `Bounds2D<T>` hat nur einen Bruchteil der Methoden von `Bounds3D<T>` (kein `Union`/`Overlaps`/
  `Inside`/`Expand`/`Volume`/`SurfaceArea`/`MaxDimension`/`Lerp`/`Offset`/`Corner`). Bereits in
  `Moarx.Math.Tests/Todos/TEST_COVERAGE_CHECKLIST.md` Punkt 2 als Fakt vermerkt — hier als
  API-Konsistenz-Aufgabe: entweder `Bounds2D<T>` auf das gleiche Funktionsniveau heben (falls
  gebraucht) oder bewusst dokumentieren, warum die 2D-Variante schlanker bleibt.

## 6. `MathmaticMethods` als Kitchen-Sink-Klasse

- [ ] RNG-Wrapper, Sampling (`SampleUniformDiskConcentric`), robuste Arithmetik (`FMA`,
  `DifferenceOfProducts`, `InnerProduct`), Quadratik-Solver, `Partition<T>` (BVH-Hilfsfunktion) und
  `ParallelFor2D` (Renderer-Tiling) sitzen alle in einer einzigen statischen Klasse. pbrt trennt das
  über mehrere Header (`sampling.h`, `math.h`, `parallel.h`). Aufteilen in z.B. `NumericHelpers`
  (FMA/DifferenceOfProducts/InnerProduct/SolveQuadratic/Safe*), `Sampling`
  (SampleUniformDiskConcentric/Random*) und `Parallel`/`Partitioning` (ParallelFor2D/Partition) würde
  die Auffindbarkeit deutlich verbessern. Reine Organisationsfrage, kein Verhaltensfix.

## 7. NaN-Guard in jedem Property-Setter — Kandidat für Debug-only

- [ ] `Point2D`/`Point3D`/`Vector2D`/`Vector3D`/`Bounds2D`/`Bounds3D` werfen bei jedem
  Komponenten-Set (`X`/`Y`/`Z`/`PMin`/`PMax`) eine `ArgumentOutOfRangeException` bei NaN. Das ist pro
  Konstruktion/`with`-Ausdruck ein zusätzlicher Check pro Komponente — in einem Pfadverfolger, der
  massenhaft Vektoren pro Frame konstruiert, spürbarer Overhead. Etablierte Libraries
  (`System.Numerics.Vector3`, GLM, Unity.Mathematics) machen das bewusst **nicht** im Release-Pfad,
  sondern verlassen sich auf IEEE-NaN-Propagation + `Debug.Assert`/`[Conditional("DEBUG")]`. Da die
  Checks in der Vergangenheit aber tatsächlich mehrfach beim Bug-Finden geholfen haben (siehe
  `TEST_COVERAGE_CHECKLIST.md`), Trade-off gegen Messung abwägen: Falls Profiling das je als
  Bottleneck zeigt, auf `[Conditional("DEBUG")]`-Guards umstellen statt ersatzlos streichen.

## 8. `Partition<T>` ist im BVH-Hot-Path ineffizient

- [ ] `MathmaticMethods.cs:118` — `Partition<T>` ist über LINQ `GroupBy` + vier separate
  `List<T>`-Allokationen implementiert, wird laut `TEST_COVERAGE_CHECKLIST.md` Punkt 7 aber aktiv und
  rekursiv aus `Raytracing/Accelerators/BVHAccelerator.cs` (Split-Methoden `Middle`/`SAH`) aufgerufen.
  Ein klassisches In-Place-`std::partition`-Äquivalent (zwei Zeiger, swappen, keine Allokation) würde
  hier deutlich besser passen — aktuell vermutlich der teuerste Einzelschritt beim Szenenaufbau.

## 9. Fehlende Funktionalität ggü. etablierten Bezier-/Kurven-APIs

- [ ] `CubicBezierCurve2D<T>`/`QuadBezierCurve2D<T>` sind reine Datencontainer (Ctor + Indexer) ohne
  `Evaluate(t)`, `Split`, `GetBoundingBox` oder Tangenten-Berechnung. Jede etablierte 2D-Kurvenbibliothek
  hätte mindestens eine Auswertungsfunktion — aktuell nicht praktisch nutzbar, nur Datenhaltung.

## 10. Kein SIMD/Hardware-Beschleunigung

- [ ] Im Gegensatz zu `System.Numerics.Vector3` (JIT-Intrinsics) oder Unity.Mathematics läuft hier
  alles über generische Skalar-Arithmetik. Für ein Lernprojekt/Playground unkritisch, aber falls der
  Raytracer mal an Performance-Grenzen stößt, ist das der strukturelle Deckel nach oben.

## 11. `DirectionCone.Union()` unvollständig

- [ ] `DirectionCone.cs:69` — wirft bewusst `NotImplementedException` (Rotation von `wr` um `thetaR`
  fehlt), bereits in `TEST_COVERAGE_CHECKLIST.md` Punkt 1 dokumentiert. Nur relevant, falls
  Light-Sampling mit Cone-Bounds gebraucht wird — als offener Punkt hier mit aufgeführt, damit er nicht
  aus dem Blick gerät.
