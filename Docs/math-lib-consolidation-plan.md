# Math-lib → Moarx.Math Konsolidierung

## Context

Es gibt zwei Mathe-Bibliotheken im Repo, die dieselben geometrischen Grundtypen doppelt
implementieren:

- **Math-lib** (legacy): `Point2D/3D`, `Vector2D/3D`, `Normal3D`, `Bounds2D/3D`, `Ray`,
  `Matrix`/`Transform` — alle hart auf `double` verdrahtet, block-scoped Namespaces,
  Abhängigkeit auf WPF (`UseWPF`) und `System.Drawing.Common`.
- **Moarx.Math** (aktiv): dieselben Konzepte, aber generisch über `INumber<T>`, keine
  WPF/System.Drawing-Kopplung, file-scoped Namespaces, mit Tests abgedeckt (u.a.
  `Vector2D/3DTests`, `Point2D/3DTests`, `TransformTests`, `SquareMatrixTests`, `Bounds3DTests`).

Ziel: **vollständige Migration**, danach `Math-lib` komplett entfernen, statt die Doppelung
nur einzudämmen.

**Wichtiger Befund aus der Analyse:** Von den 7 Projekten, die `Math-lib.csproj` referenzieren,
haben die meisten **keine echte Codenutzung** mehr:

| Projekt | Referenziert Math-lib? | Nutzt tatsächlich Math-lib-Typen? |
|---|---|---|
| `SandSim` | ja (`using Math_lib;`) | **nein** — einzige Fundstelle ist auskommentierter Code |
| `RasterizerTest` | ja | **nein** — hat eine eigene, unabhängige lokale `DirectBitmap`-Klasse |
| `Neural_Network_WPF` | ja | **nein** |
| `NeuralNetwork` | ja | **nein** |
| `BenchmarkTests` | ja | nur `VertexBenchmarks.cs` (Vertex/VertexAttributes) |
| `Projection` | ja | **ja, stark** — 115+ Fundstellen über 14 Dateien, inkl. `Mesh`/`Vertex`/`VertexAttributes`/`DirectBitmap`/`Matrix` |
| `Math-lib.Tests` | ist Math-lib | testet Math-lib selbst |

Das heißt: der eigentliche Migrationsaufwand konzentriert sich fast vollständig auf
**`Projection`**. Alles andere ist Aufräumen von toten Referenzen.

Zusätzlich enthält Math-lib Typen ohne Moarx.Math-Äquivalent, die keine "Mathe"-Typen im
eigentlichen Sinn sind, sondern Teil von Projections Mesh/Render-Pipeline: `Mesh`, `Vertex`,
`VertexAttribute` (+ 3 Implementierungen), `DirectBitmap`. Sowie `BinaryTreeNode`
(`BinaryTree.cs`), das im gesamten Repo nirgendwo außerhalb seiner eigenen Datei benutzt wird
(toter Code).

## Plan

### Phase 1 — Tote Referenzen entfernen (risikofrei)

- [x] In `SandSim`, `RasterizerTest`, `Neural_Network_WPF`, `NeuralNetwork`: `<ProjectReference>`
      auf `Math-lib.csproj` entfernen.
- [x] In `SandSim/MainWindow.xaml.cs` das ungenutzte `using Math_lib;` entfernen.
- [x] Jedes der 4 Projekte einzeln bauen, um zu bestätigen, dass nichts bricht.

### Phase 2 — BenchmarkTests

- [ ] `BenchmarkTests/VertexBenchmarks.cs` löschen (benchmarkt `Math_lib.Vertex`/
      `VertexAttributes` — ein Konzept, das ausschließlich zu `Projection` gehört und in
      Moarx.Math kein Äquivalent hat/bekommt, siehe Phase 4).
- [ ] `ProjectReference` auf `Math-lib.csproj` aus `BenchmarkTests.csproj` entfernen
      (kein anderer Benchmark referenziert Math-lib).

### Phase 3 — Projection: Geometrie-Typen migrieren

- [ ] In den 14 betroffenen Dateien (`Importer.cs`, `PubeScreenTransformer.cs`, `Clipping.cs`,
      `MainWindow.xaml.cs`, `Pipeline.cs`, `Primitives/Cube.cs`, `Primitives/Sphere.cs`,
      `Primitives/Plane.cs`, `Effects/*.cs`) `Point2D`/`Point3D`/`Vector2D`/`Vector3D`/
      `Normal3D`/`Bounds2D`/`Bounds3D`/`Ray` durch die generischen Moarx.Math-Typen mit
      `double` ersetzen (`Point3D<double>`, `Vector3D<double>`, …).
- [ ] `Matrix`-Nutzung (nur in `MainWindow.xaml.cs`, `Primitives/Sphere.cs`,
      `PubeScreenTransformer.cs` gefunden) auf `Transform` ummünzen:
  - `Matrix.RotateXMarix/RotateYMarix/RotateZMarix(angle)` → `Transform.RotateX/Y/Z(angle)`
  - `Matrix.Projection(width, height, fov, zNear, zFar)` → `Transform.Perspective(fov, near, far)`
    — Parametrierung unterscheidet sich (kein width/height in Moarx' Perspective), hier genau
    prüfen, wie Projection sein Screen-Mapping bisher kombiniert hat, damit die projizierte
    Ausgabe gleich bleibt.
  - `Matrix * Point3D` / `* Vector3D` Operator-Aufrufe → `Transform * Point3D<double>` etc.
    (Moarx' `Transform` überlädt dieselben Operatoren).
- [ ] Datei für Datei vorgehen, nach jeder Datei bauen.
- [ ] Nach Abschluss die App starten und die Cube/Sphere/Plane-Primitives mit den vorhandenen
      Effects visuell gegenprüfen — Math-lib's `Matrix` und Moarx.Math's `Transform` sind keine
      bit-identischen Implementierungen.

### Phase 4 — Mesh/Vertex/DirectBitmap/BinaryTree

- [ ] `Mesh`, `Vertex`, `VertexAttribute` + `ColorVertexAttribute`/`NormalVertexAttribute`/
      `TextureCoordinateVertexAttribute`: Dateien nach `Projection` verschieben (z.B.
      `Projection/Mesh/`), Namespace von `Math_lib`/`Math_lib.VertexAttributes` auf `Projection`
      (oder Unternamespace) ändern. Das ist Projections eigene Mesh-Pipeline, kein
      Allzweck-Mathetyp — gehört zum einzigen Nutzer, nicht in eine geteilte Lib.
- [ ] `DirectBitmap`: nach `Projection` verschieben, unverändert (System.Drawing.Color-basiert,
      inkl. `ToImageSource`/`FloodFill`).
  - Alternative wäre `Moarx.Graphics.DirectBitmap` (bereits von `Raytracing` genutzt), aber die
    nutzt `DirectColor` statt `System.Drawing.Color` und hat weder `FloodFill` noch
    `ToImageSource` — das würde alle `Effects/*.cs`-Dateien anfassen, die auf
    `System.Drawing.Color` aufbauen. Für diesen Schritt nicht den Aufwand wert, da `Projection`
    laut CLAUDE.md Legacy/Referenzcode ist. Möglicher Folgeschritt, falls `Projection` mal
    wieder aktiv weiterentwickelt wird.
- [ ] `BinaryTreeNode` (`BinaryTree.cs`): nirgends im Solution genutzt außer in der eigenen
      Datei — ersatzlos löschen, nicht migrieren.

### Phase 5 — Math-lib entfernen

- [ ] `Math-lib`-Ordner, `Math-lib.csproj` und `Math-lib.Tests` aus `Math-lib.sln` **und**
      `Projection.sln` entfernen.
- [ ] `Projection.sln` referenziert aktuell explizit `Math-lib` — nach der Migration braucht
      `Projection` stattdessen `Moarx.Math` als ProjectReference.
- [ ] `CLAUDE.md` aktualisieren: den Abschnitt "Migration: Math-lib/legacy → Moarx.Math/
      Moarx.Graphics" entfernen bzw. auf "abgeschlossen" setzen, die Projektliste
      (`Projection.sln`-Beschreibung, "Math-lib.Tests mirrors Moarx.Math.Tests"-Hinweis)
      entsprechend anpassen, da Math-lib nicht mehr existiert.

### Verifikation

- [ ] Nach jeder Phase: `dotnet build Math-lib.sln`.
- [ ] `dotnet test Math-lib.sln` (Moarx.Math.Tests deckt ab, was vorher Math-lib.Tests testete).
- [ ] Nach Phase 3/4: `Projection` als WPF-App manuell starten und die 3D-Ausgabe
      (Cube/Sphere/Plane, Effects) visuell mit dem Stand vor der Migration vergleichen — das
      ist die einzige Stelle mit echtem Verhaltensrisiko.
