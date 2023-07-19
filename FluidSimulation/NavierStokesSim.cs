using System;

namespace FluidSimulation;

public struct FluidInformation {
    public double[,] uVectorField;
    public double[,] vVectorField;
    public double[,] pressureGradient;
}

public class NavierStokesSim {

    readonly int _cellsX;
    readonly int _cellsY;
    readonly int _numberTimesteps;
    readonly int _numberIterations;
    readonly double _timestep;
    readonly double _viscosityFactor;
    readonly double _rho;

    readonly double dx;
    readonly double dy;

    public NavierStokesSim(int cellsX, int cellsY, double timestep, int numberTimesteps, int numberiterations, double viscosityFactor, double rho) {
        _cellsX = cellsX;
        _cellsY = cellsY;
        _timestep = timestep;
        _numberTimesteps = numberTimesteps;
        _numberIterations = numberiterations;
        _viscosityFactor = viscosityFactor;
        _rho = rho;

        dx = (double)2 / (_cellsX - 1);
        dy = (double)2 / (_cellsY - 1);
    }

    public FluidInformation Solve() {
        double[,] u = new double[_cellsX,_cellsY];
        double[,] v = new double[_cellsX,_cellsY];
        double[,] p = new double[_cellsX,_cellsY];

        double[,] b = new double[_cellsX,_cellsY];

        //Calculation
        for (int it = 0; it < _numberTimesteps; it++) {

            //RHS poisson equation calculation
            for (int i = 1; i < _cellsX - 1; i++) {
                for (int j = 1; j < _cellsY - 1; j++) {

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
            for (int i = 1; i < _cellsX - 1; i++) {
                for (int j = 1; j < _cellsY - 1; j++) {
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

            //Boundary Conditions
            for (int j = 0; j < _cellsY; j++) {
                u[0, j] = 0;
                v[0, j] = 0;

                u[_cellsX - 1, j] = 0;
                v[_cellsX - 1, j] = 0;
            }
            for (int i = 0; i < _cellsX; i++) {
                u[i, 0] = 0;
                v[i, 0] = 0;

                u[i, _cellsY - 1] = 1;
                v[i, _cellsY - 1] = 0;
            }
        }

        return new FluidInformation {  uVectorField = u, vVectorField = v, pressureGradient = p};
    }

    double CalculateB(double uIp1J, double uIm1J, double uIJp1, double uIJm1, double vIp1J, double vIJp1, double vIJm1) {
        return  _rho * ((uIp1J - uIm1J) / 2 / dx + (vIJp1 - vIJm1) / 2 / dy) / _timestep + Math.Pow(((uIp1J - uIm1J) / 2 / dx), 2) +
                2 * (uIJp1 - uIJm1) / 2 / dy * (vIp1J - vIJm1) / 2 / dx + Math.Pow(((vIJp1 - vIJm1) / 2 / dy), 2);
    }

    double CalculateU(double uIJ, double uIm1J, double uIp1J, double uIJm1, double uIJp1, double vIJ, double pIp1J, double pIm1J) {

        return uIJ - uIJ * _timestep / dx * (uIJ - uIm1J) - vIJ * _timestep / dy * (uIJ - uIJm1) -
               1 / _rho * (pIp1J - pIm1J) * _timestep / 2 / dx + _viscosityFactor * _timestep / (dx * dx) * (uIp1J - 2 * uIJ + uIm1J)
               + _viscosityFactor * _timestep / (dy * dy) * (uIJp1 - 2 * uIJ + uIJm1);
    }
    double CalculateV(double vIJ, double vIm1J, double vIp1J, double vIJm1, double vIJp1, double uIJ, double pIJp1, double pIJm1) {

        return vIJ - uIJ * _timestep / dx * (vIJ - vIm1J) - vIJ * _timestep / dy * (vIJ - vIJm1) -
               1 / _rho * (pIJp1 - pIJm1) * _timestep / 2 / dy + _viscosityFactor * _timestep / (dx * dx) * (vIp1J - 2 * vIJ + vIm1J)
               + _viscosityFactor * _timestep / (dy * dy) * (vIJp1 - 2 * vIJ + vIJm1);
    }

    double[,] SolvePressurePoisson(double[,] p, double[,] b) {
        for (int iit = 0; iit < _numberIterations; iit++) {
            double[,] pn = (double[,])p.Clone();

            for (int i = 1; i < _cellsX - 1; i++) {
                for (int j = 1; j < _cellsY - 1; j++) {
                    p[i, j] = ((pn[i + 1, j] + pn[i - 1, j]) * (dy * dy) + (pn[i, j + 1] + pn[i, j - 1]) * (dx * dx) - b[i, j] * (dx * dx * dy * dy)) / (dx * dx + dy * dy) / 2;
                }
            }

            //Boundary Conditions
            for (int j = 0; j < _cellsY; j++) {
                p[0, j] = p[1, j];
                p[_cellsX - 1, j] = p[_cellsX - 2, j];
            }
            for (int i = 0; i < _cellsX; i++) {
                p[i, 0] = p[i, 1];
                p[i, _cellsY - 1] = 0;
            }
        }

        return p;
    }

}
