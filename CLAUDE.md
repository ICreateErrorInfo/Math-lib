# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Repository overview

This is a personal graphics/math playground, not a single product. It contains an actively developed
math + rendering stack (`Moarx.*`) alongside a collection of older, largely dormant WPF demo apps
(raytracers, fluid/sand simulations, a neural network) that were early iterations of the same ideas.
When making changes, prefer touching the `Moarx.*` projects and `Raytracing` — that's where active
development happens (see recent git log). Treat `Math-lib`, `Projection`, `RasterizerTest`,
`RaytracingInOneWeek`, `Moarx.Rasterizer` as legacy/reference code unless asked to work on them directly.

There are two solutions:
- `Math-lib.sln` — the main solution, contains all active projects. Use this one.
- `Projection.sln` — a small legacy solution (`Projection`, `Math-lib`, `Math-lib.Tests` only).

## Common commands

Build and test (run from repo root):

```
dotnet build Math-lib.sln
dotnet test Math-lib.sln
```

Run tests for a single project:

```
dotnet test Moarx.Math.Tests/Moarx.Math.Tests.csproj
dotnet test Raytracer.Tests/Raytracing.Tests.csproj
```

Run a single test (NUnit, via the `--filter` flag, works with `dotnet test` on any test project):

```
dotnet test Moarx.Math.Tests/Moarx.Math.Tests.csproj --filter "FullyQualifiedName~Vector3DTests.Cross"
```

Test projects use NUnit (`Microsoft.NET.Test.Sdk` + `NUnit3TestAdapter`). Older test projects
(`Math-lib.Tests`) mirror newer ones (`Moarx.Math.Tests`) for the same types during the migration
described below — check both when changing math primitives if the legacy project is in scope.

Run benchmarks (BenchmarkDotNet):

```
dotnet run -c Release --project BenchmarkTests/BenchmarkTests.csproj
```

WPF demo apps (`Raytracing`, `FluidSimulation`, `SandSim`, `SPH_FluidSim`, `Neural_Network_WPF`, etc.)
are Windows-only (`net7.0-windows`, `UseWPF=true`) and must be built/run on Windows.

## Architecture

### Migration: `Math-lib`/legacy → `Moarx.Math`/`Moarx.Graphics`

The library was restructured from a single `Math-lib` project into a set of focused libraries under
the `Moarx.*` namespace. `Moarx.Math` and `Moarx.Graphics` are the current, maintained libraries;
`Math-lib` is the predecessor kept around for reference/compat and is not where new math code should
go. When adding or fixing a math primitive (vectors, points, bounds, rays, transforms), the canonical
location is `Moarx.Math`, not `Math-lib`.

- **`Moarx.Math`** — generic math primitives independent of rendering: `Vector2D`/`Vector3D`,
  `Point2D`/`Point3D`, `Normal3D`, `Ray`, `Bounds2D`/`Bounds3D`, `SquareMatrix`, `Transform`,
  `Rectangle2D`, `Ellipse2D`, curves (`CubicBezierCurve2D`, `QuadBezierCurve2D`), `DirectionCone`,
  and `rng.cs` (PCG-style RNG). Many types are generic over a numeric type (see `MathmaticMethods.cs`
  for shared numeric helpers) so they work with `float`/`double` interchangeably.
- **`Moarx.Graphics`** — imaging and color/spectral rendering built on top of `Moarx.Math`:
  - `Bitmap/` — `DirectBitmap` (a fast, directly-addressable pixel buffer; split into partials:
    `DirectBitmap.Sliced.cs`, `DirectBitmap.Source.cs`).
  - `Color/` — `DirectColor`, `RGB`, `XYZ`, `RGBColorSpace`, sRGB/DCI-P3 constants,
    `RGBSigmoidPolynomial`, `RGBToSpectrumTable`.
  - `Spectrum/` — spectral power distribution types: `SampledSpectrum`, `SampledWavelengths`,
    `DenselySampledSpectrum`, `PiecewiseLinearSpectrum`, `BlackbodySpectrum`, `RGBAlbedoSpectrum`,
    `RGBIlluminantSpectrum`, `ConstantSpectrum`, all behind `ISpectrum`.

  The spectral/color pipeline (`RGBSigmoidPolynomial`, `RGBToSpectrumTable`, densely-sampled spectra,
  Hero-wavelength-style `SampledWavelengths`) follows the spectral rendering approach from *Physically
  Based Rendering* (pbrt-v4) — recognizing that lineage helps when reasoning about why these types
  exist and how they interact.

### `Raytracing` (active WPF raytracer)

A pbrt-style physically based renderer, structured close to pbrt-v4's architecture:

- `Camera/` — `ICamera`, `ProjectiveCamera` base, `PerspectiveCamera`, `OrthographicCamera`,
  `CameraTransform`.
- `Shapes/` — `Shape` base plus `Sphere`, `MovingSphere`, `Triangle`, `Disk`, `Cylinder`, `Cone`, `AARect`.
- `Primitives/` — `Primitive`/`GeometricPrimitive` wrap a shape + material; `TriangleMesh`,
  `PrimitiveList`.
- `Accelerators/` — BVH acceleration structure (`BVHAccelerator` split across partials for build
  nodes, bucket info, Morton-code primitives, LBVH treelets, linear BVH nodes, sort, split method),
  plus `Aggregate` and a simpler `BVHNode`.
- `Materials/` — `Material` base, `lambertian`, `Metal`, `Dielectric`, `DiffuseLight`, `Texture`/
  `ImageTexture`, `Perlin` noise.
- `Integrators/` — `IIntegrator`, `RayIntegrator`, `ImageTileIntegrator`, `RandomWalkIntegrator`.
  (`Intigrators/` is a stale, empty leftover directory from a typo — ignore it, don't add files there.)
- `Scene.cs` / `Raytracer.cs` / `Film.cs` — scene assembly, the render loop, and the output image buffer.
- `MainWindow.xaml(.cs)` renders live preview output to the WPF window as the image is traced.

Rendering depends on `Moarx.Math` (geometry/transforms) and `Moarx.Graphics` (color/spectrum/bitmap
output) — check both when changing shared types like `Vector3D`, `Ray`, `Transform`, or `DirectColor`,
since changes ripple into the renderer.

### Other projects (legacy/standalone demos)

These are largely independent, older WPF applications not on the active development path:
`Projection` (software 3D projection/rasterizer), `RasterizerTest`, `Moarx.Rasterizer`,
`RaytracingInOneWeek` ("Ray Tracing in One Weekend" implementation, precursor to `Raytracing`),
`FluidSimulation` / `SPH_FluidSim` / `SandSim` (particle-based simulations), `NeuralNetwork` /
`Neural_Network_WPF` (from-scratch neural net + WPF visualizer), `Recognizer`. Each has its own
`.csproj`; most reference `Math-lib` rather than `Moarx.Math`.

## Code style

Formatting/style is enforced via `.editorconfig` at the repo root (applies to all `*.cs` files across
every project). Notable non-default conventions to match:
- 4-space indentation, CRLF line endings.
- Opening braces stay on the same line as the declaration (K&R style), not their own line.
- `var` used when the type is apparent or for built-in types, but not elsewhere.
- File-scoped namespaces (`block_scoped:silent` — new code in `Moarx.*` uses `namespace X;`; older
  code in `Math-lib`/`Raytracing` uses block-scoped `namespace X { }`) — match whatever the file
  you're editing already uses.
