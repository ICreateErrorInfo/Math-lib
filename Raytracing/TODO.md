# Raytracing – TODO / Known Issues

Ergebnis einer Code-Analyse des `Raytracing`-Projekts (Stand 2026-08-08). Priorisiert nach Schwere.

## Kritisch (Korrektheit)

- [ ] **`Shapes/Cylinder.cs:53`** – `tMax = 0;` statt `tMax = t0;`. Da `Ray` mutable ist und `GeometricPrimitive.Intersect`
      diesen Wert in `r.TMax` übernimmt, kollabiert nach jedem Zylindertreffer der gültige Suchbereich für den Rest der
      BVH-Traversierung auf diesem Ray (`Bounds3D.IntersectP` prüft `tMin < ray.TMax`). Folge: nach einem Zylindertreffer
      werden alle weiteren, auch näherliegenden Objekte im selben Traversierungslauf ignoriert.
- [ ] **`Integrators/RandomWalkIntegrator.cs:43`** – `LiRandomWalk(interaction.ScatteredRay, lambda, depth++)` nutzt
      Post-Inkrement als Argument; der rekursive Aufruf bekommt immer den alten `depth`-Wert. `MaxDepth` greift dadurch
      nie. Fix: `depth + 1`. Risiko: Stack-Overflow in geschlossenen Szenen (z. B. Cornell Box).
- [ ] **`Moarx.Math/Vector3D.cs:89`** (`Refract`) – `new Vector3D<U>(eta * cosi)` nutzt den Ein-Parameter-Ctor und
      erzeugt den uniformen Vektor `(eta·cosi, eta·cosi, eta·cosi)` statt `eta·cosi · v1` (Normalenvektor). Betrifft
      `Dielectric.Scatter` direkt – Glas-Rendering ist physikalisch falsch, sobald die Normale nicht zufällig
      achsenparallel zu (1,1,1) ist.
- [ ] **`Primitives/TriangleMesh.cs:27-32`** – setzt `Normal = null`, ruft dann `Normal.Add(...)` in der Schleife (NRE)
      und liest zusätzlich das Feld `Normal[i]` statt des Parameters `normal[i]`. Aktuell "unsichtbar", weil
      `Importer.Obj` keine `vn`-Zeilen parst – stürzt sofort ab, sobald Vertexnormalen importiert werden.
- [ ] **`Shapes/Sphere.cs:61-66`** – Retry-Logik für geclippte Kugeln (zMin/zMax/phiMax) ist wirkungslos: die genutzte
      `SolveQuadratic`-Überladung liefert nur eine Nullstelle; wird sie verworfen, wird `t0 = ray.TMax` gesetzt, aber
      `pHit`/`phi` nie neu berechnet – die zweite Prüfung ist identisch zur ersten und liefert immer `false`. Geclippte
      Kugeln (zweiter Ctor) sind für den entfernten Schnittpunkt faktisch nicht renderbar.
- [ ] **`Shapes/Disk.cs:49`** – Normale ist der Trefferpunkt selbst statt der konstanten Flächennormale `(0,0,±1)`.
- [ ] **`Shapes/Cone.cs:56-58`** – Normalenformel falsch: X/Y-Komponente werden immer identisch gesetzt, unabhängig vom
      tatsächlichen Trefferpunkt; korrekter Gradient wäre `(2x, 2y, -2k²(z-h))`.

## Mittel

- [ ] Toten Code entfernen oder klar als experimentell markieren:
  - `Raytracer.cs:198` `GetRayColor` (dupliziert `RandomWalkIntegrator.LiRandomWalk`, nie aufgerufen)
  - `Accelerators/BVHNode.cs` (komplett unbenutztes zweites BVH, eigene Sortier-Bugs)
  - `Mathmatic/SurfaceInteraction2.cs` / `Interaction.cs` (abgebrochener Versuch einer pbrt-näheren Interaction, nirgends verdrahtet)
  - auskommentierter LBVH/Morton-Code am Ende von `Accelerators/BVHAccelerator.cs:238-353`
  - auskommentierte serielle Renderschleife in `Raytracer.cs:86-115`
- [ ] `Accelerators/BVHAccelerator.Sort.cs` – Comparator liefert bei Gleichheit nie `0` (verletzt Introsort-Contract),
      Risiko einer `InvalidOperationException` bei Primitiven mit identischer Centroid-Koordinate.
- [ ] Stray Usings entfernen: `Microsoft.VisualBasic`, `System.Windows.Documents` in `RandomWalkIntegrator.cs:1,8`;
      `System.Net.Http.Headers` in `ImageTileIntegrator.cs:7`.
- [ ] "Earth"-Szene in `MainWindow.xaml.cs:273` ist nicht lauffähig (`new ImageTexture("", colorSpace)` – leerer Pfad,
      wirft beim Rendern). Entweder echten Texturpfad hinterlegen oder aus dem Szenen-Dropdown entfernen.
- [ ] Tests ergänzen für die am wenigsten abgedeckten, aber am fehleranfälligsten Bereiche:
  - Materials (v. a. `Dielectric`-Refraction gegen Referenzwerte)
  - Integrator-Tiefenbegrenzung (`MaxDepth` wird eingehalten)
  - `TriangleMesh`-Normalenimport
  - `BVHAccelerator.Intersect()` (aktuell nur der Ctor getestet, keine Traversierungs-Assertions)
- [ ] `Shapes/Sphere.cs` `GetSphereUV` nutzt Y-Achsen-Polkoordinaten, während das Clipping (`_zMin`/`_zMax`/`_phiMax`)
      auf der Z-Achse arbeitet – nach Fix der Clipping-Logik (siehe oben) würden UV-Koordinaten nicht zur sichtbaren
      Kappe passen.
- [ ] `Materials/DiffuseLight.cs` `Emitted` ignoriert `FrontFace` – Fläche emittiert beidseitig; ggf. bewusst so
      belassen oder dokumentieren.

## Nice-to-have

- [ ] `Accelerators/BVHAccelerator.cs:369` – `nodesToVisit`-Array wird pro Ray neu alloziert; poolen/wiederverwenden
      statt GC-Druck pro Sample.
- [ ] `Materials/Metal.cs` – `_fuzz` auch nach unten clampen (`Math.Clamp(fuzz, 0, 1)` statt nur `< 1 ? fuzz : 1`).
- [ ] `Moarx.Math/MathmaticMethods.cs:225` `ParallelFor2D` – Kachelanzahl über `extent.PMax.X/Y` statt
      `extent.Diagonal()` berechnet; für aktuelle Aufrufer (`PMin=(0,0)`) folgenlos, aber nicht generisch korrekt.
