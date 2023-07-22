using System;

namespace FluidSimulation;

public struct BoundaryConditionData {
    public double? UVelocity;
    public double? VVelocity;
    public Func<double[,], int, int, double>? Pressure;
}
