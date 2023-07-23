using System;

namespace FluidSimulation;

public struct FluidInformation {
    public double[,] uVectorField;
    public double[,] vVectorField;
    public double[,] pressureGradient;
}
public struct FluidSimulationData {
    public int _cellsX;
    public int _cellsY;
    public int _numberTimesteps;
    public int _numberIterations;
    public double _timestep;
    public double _viscosityFactor;
    public double _rho;

    public BoundaryConditionData[,] boundaryConditions;
}

public class NavierStokesSim { 
    FluidSimulationData _data;

    readonly double dx;
    readonly double dy;

    public NavierStokesSim(FluidSimulationData data) {
        _data = data;

        dx = (double)2 / (_data._cellsX - 1); //TODO Bug 2 has to be the domain legth 
        dy = (double)2 / (_data._cellsY - 1);
    }

    public FluidInformation Solve() {
        double[,] u = new double[_data._cellsX,_data._cellsY];
        double[,] v = new double[_data._cellsX,_data._cellsY];
        double[,] p = new double[_data._cellsX,_data._cellsY];

        double[,] b = new double[_data._cellsX,_data._cellsY];

        //Calculation
        for (int it = 0; it < _data._numberTimesteps; it++) {

            //RHS poisson equation calculation
            for (int i = 1; i < _data._cellsX - 1; i++) {
                for (int j = 1; j < _data._cellsY - 1; j++) {

                    b[i, j] = CalculateB(
                        uIp1J: u[i+1, j],
                        uIm1J: u[i-1, j],
                        uIJp1: u[i, j+1],
                        uIJm1: u[i, j-1],
                        vIp1J: v[i+1, j],
                        vIJp1: v[i, j+1],
                        vIJm1: v[i, j-1]);
                }
            }

            p = SolvePressurePoisson(p, b);

            //solve for u and v
            for (int i = 1; i < _data._cellsX - 1; i++) {
                for (int j = 1; j < _data._cellsY - 1; j++) {
                    u[i, j] = CalculateU(
                        uIJ: u[i, j],
                        uIm1J: u[i - 1, j],
                        uIp1J: u[i + 1, j],
                        uIJm1: u[i, j - 1],
                        uIJp1: u[i, j + 1],
                        vIJ:   v[i, j],
                        pIp1J: p[i + 1, j],
                        pIm1J: p[i - 1, j]);

                    v[i, j] = CalculateV(
                        vIJ:   v[i, j],
                        vIm1J: v[i - 1, j],
                        vIp1J: v[i + 1, j],
                        vIJm1: v[i, j - 1],
                        vIJp1: v[i, j + 1],
                        uIJ:   u[i, j],
                        pIJp1: p[i, j + 1],
                        pIJm1: p[i, j - 1]);
                }
            }

            for (int i = 0; i < _data._cellsX; i++) {
                for (int j = 0; j < _data._cellsY; j++) {
                    if(_data.boundaryConditions[i, j].UVelocity != null) {
                        u[i, j] = (double)_data.boundaryConditions[i, j].UVelocity;
                    }
                    if (_data.boundaryConditions[i, j].VVelocity != null) {
                        v[i, j] = (double)_data.boundaryConditions[i, j].VVelocity;
                    }
                }
            }
        }

        return new FluidInformation {  uVectorField = u, vVectorField = v, pressureGradient = p};
    }

    double CalculateB(double uIp1J, double uIm1J, double uIJp1, double uIJm1, double vIp1J, double vIJp1, double vIJm1) {
        return  _data._rho * ((uIp1J - uIm1J) / 2 / dx + (vIJp1 - vIJm1) / 2 / dy) / _data._timestep + Math.Pow(((uIp1J - uIm1J) / 2 / dx), 2) +
                2 * (uIJp1 - uIJm1) / 2 / dy * (vIp1J - vIJm1) / 2 / dx + Math.Pow(((vIJp1 - vIJm1) / 2 / dy), 2);
    }

    double CalculateU(double uIJ, double uIm1J, double uIp1J, double uIJm1, double uIJp1, double vIJ, double pIp1J, double pIm1J) {

        return uIJ - uIJ * _data._timestep / dx * (uIJ - uIm1J) - vIJ * _data._timestep / dy * (uIJ - uIJm1) -
               1 / _data._rho * (pIp1J - pIm1J) * _data._timestep / 2 / dx + _data._viscosityFactor * _data._timestep / (dx * dx) * (uIp1J - 2 * uIJ + uIm1J)
               + _data._viscosityFactor * _data._timestep / (dy * dy) * (uIJp1 - 2 * uIJ + uIJm1);
    }
    double CalculateV(double vIJ, double vIm1J, double vIp1J, double vIJm1, double vIJp1, double uIJ, double pIJp1, double pIJm1) {

        return vIJ - uIJ * _data._timestep / dx * (vIJ - vIm1J) - vIJ * _data._timestep / dy * (vIJ - vIJm1) -
               1 / _data._rho * (pIJp1 - pIJm1) * _data._timestep / 2 / dy + _data._viscosityFactor * _data._timestep / (dx * dx) * (vIp1J - 2 * vIJ + vIm1J)
               + _data._viscosityFactor * _data._timestep / (dy * dy) * (vIJp1 - 2 * vIJ + vIJm1);
    }

    double[,] SolvePressurePoisson(double[,] p, double[,] b) {
        for (int iit = 0; iit < _data._numberIterations; iit++) {
            double[,] pn = (double[,])p.Clone();

            for (int i = 1; i < _data._cellsX - 1; i++) {
                for (int j = 1; j < _data._cellsY - 1; j++) {
                    p[i, j] = ((pn[i + 1, j] + pn[i - 1, j]) * (dy * dy) + (pn[i, j + 1] + pn[i, j - 1]) * (dx * dx) - b[i, j] * (dx * dx * dy * dy)) / (dx * dx + dy * dy) / 2;
                }
            }

            for (int i = 0; i < _data._cellsX; i++) {
                for (int j = 0; j < _data._cellsY; j++) {
                    if (_data.boundaryConditions[i, j].Pressure != null) {
                        p[i, j] = _data.boundaryConditions[i, j].Pressure(p, i, j);
                    }
                }
            }
        }

        return p;
    }

}
