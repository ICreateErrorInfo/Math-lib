using Moarx.Math;
using Moarx.Rasterizer;
using System;
using System.Collections.Generic;

namespace FluidSimulation;

public enum FluidObjectType {
    Domain,
    Inflow,
    Outflow,
    Collision
}
public struct FluidObject {
    public List<Rectangle2D<double>> Geometry;
    public FluidObjectType Type;
}

public class MeshGenerator {
    List<FluidObject> _fluidObjects;
    readonly int _cellsX;
    readonly int _cellsY;

    public MeshGenerator(FluidObject domain, int cellsX, int cellsY) {
        if (domain.Type != FluidObjectType.Domain) {
            throw new Exception("Domain required");
        }

        _fluidObjects = new List<FluidObject>();
        _fluidObjects.Add(domain);

        _cellsX = cellsX;
        _cellsY = cellsY;
    }

    public void AddObject(FluidObject fluidObject) {
        _fluidObjects.Add(fluidObject);
    }

    public FluidObject GetDomain() {
        foreach (FluidObject fluidObject in _fluidObjects) {
            if (fluidObject.Type == FluidObjectType.Domain) { return fluidObject; }
        }
        throw new Exception("There is no domain");
    }

    public BoundaryConditionData[,] GetBoundaryConditions() {
        BoundaryConditionData[,] boundaryConditions = new BoundaryConditionData[_cellsX, _cellsY];

        FluidObject domain = GetDomain();

        double domainX = 0;
        double domainY = 0;

        foreach (var rectangle in domain.Geometry) {
            domainX = rectangle.Width;
            domainY = rectangle.Height;
        }

        double PixelsPerUnitX = (_cellsX - 1) / domainX;
        double PixelsPerUnitY = (_cellsY - 1) / domainY;


        DirectBitmap bitmap = DirectBitmap.Create(_cellsX, _cellsY, 3);
        DirectGraphics directGraphics = DirectGraphics.Create(bitmap);

        foreach (var fluidObject in _fluidObjects) {
            foreach (var rectangle in fluidObject.Geometry) {
                Point2D<int> p1 = new Point2D<int>((int)(PixelsPerUnitX * rectangle.TopLeft.X), (int)(PixelsPerUnitY * rectangle.TopLeft.Y));
                Point2D<int> p2 = new Point2D<int>((int)(PixelsPerUnitX * rectangle.BottomRight.X), (int)(PixelsPerUnitY * rectangle.BottomRight.Y));

                Rectangle2D<int> rect = new Rectangle2D<int>(p1, p2);

                if (fluidObject.Type == FluidObjectType.Collision) {
                    DirectAttributes attributes = new DirectAttributes(DirectColor.FromRgb(0, 0, 255), 1, DirectColor.FromRgb(0, 0, 255));
                    if(p1.X == p2.X || p1.Y == p2.Y) {
                        directGraphics.DrawLine(new(p1, p2), attributes); //TODO bad
                    } else {
                        directGraphics.DrawRectangle(rect, attributes);
                    }
                }
                if(fluidObject.Type == FluidObjectType.Inflow) {
                    DirectAttributes attributes = new DirectAttributes(DirectColor.FromRgb(255, 0, 0), 1, DirectColor.FromRgb(255, 0, 0));
                    if (p1.X == p2.X || p1.Y == p2.Y) {
                        directGraphics.DrawLine(new(p1, p2), attributes);
                    } else {
                        directGraphics.DrawRectangle(rect, attributes);
                    }
                }
                if (fluidObject.Type == FluidObjectType.Outflow) {
                    DirectAttributes attributes = new DirectAttributes(DirectColor.FromRgb(0, 255, 0), 1, DirectColor.FromRgb(0, 255, 0));
                    if (p1.X == p2.X || p1.Y == p2.Y) {
                        directGraphics.DrawLine(new(p1, p2), attributes);
                    } else {
                        directGraphics.DrawRectangle(rect, attributes);
                    }
                }
            }
        }

        for(int i = 0; i < _cellsX; i++) {
            for(int j  = 0; j < _cellsY; j++) {
                if(bitmap.GetPixel(i, j) == DirectColor.FromRgb(0, 0, 255)) {
                    boundaryConditions[i, j] = new BoundaryConditionData() { 
                        UVelocity = 0,
                        VVelocity = 0,
                        Pressure = (p, i, j) => { return 0; } 
                    };
                }
                if (bitmap.GetPixel(i, j) == DirectColor.FromRgb(255, 0, 0)) {
                    boundaryConditions[i, j] = new BoundaryConditionData() {
                        Pressure = (p, i, j) => { return 1; }
                    };
                }
                if (bitmap.GetPixel(i, j) == DirectColor.FromRgb(0, 255, 0)) {
                    boundaryConditions[i, j] = new BoundaryConditionData() {
                        Pressure = (p, i, j) => { return -1; }
                    };
                }
            }
        }

        return boundaryConditions;
    }
}
