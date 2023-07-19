using System;

namespace FluidSimulation;

public struct FluidInformation {
    public double[,] uVectorField;
    public double[,] vVectorField;
    public double[,] pressureGradient;
}

public class NavierStokesSim {

    int _cellsX;
    int _cellsY;
    int _numberTimesteps;
    int _numberIterations;
    double _timestep;
    double _viscosityFactor;
    double _rho;

    double dx;
    double dy;

    public NavierStokesSim(int cellsX, int cellsY, double timestep, int numberTimesteps, int numberiterations, double viscosityFactor, double rho) {
        _cellsX = cellsX;
        _cellsY = cellsY;
        _timestep = timestep;
        _numberTimesteps = numberTimesteps;
        _numberIterations = numberiterations;
        _viscosityFactor = viscosityFactor;
        _rho = rho;
    }

    public FluidInformation Solve() {

        dx = (double)2 / (_cellsX - 1);
        dy = (double)2 / (_cellsY - 1);

        double[,] u = new double[_cellsX,_cellsY];
        double[,] v = new double[_cellsX,_cellsY];
        double[,] p = new double[_cellsX,_cellsY];

        double[,] b = new double[_cellsX,_cellsY];

        //Calculation
        for (int it = 0; it < _numberTimesteps; it++) {

            //RHS poisson equation calculation
            for (int i = 1; i < _cellsX - 1; i++) {
                for (int j = 1; j < _cellsY - 1; j++) {
                    b[i, j] =
                        _rho * ((u[i + 1, j] - u[i - 1, j]) / 2 / dx + (v[i, j + 1] - v[i, j - 1]) / 2 / dy) / _timestep + Math.Pow(((u[i + 1, j] - u[i - 1, j]) / 2 / dx), 2) +
                        2 * (u[i, j + 1] - u[i, j - 1]) / 2 / dy * (v[i + 1, j] - v[i, j - 1]) / 2 / dx + Math.Pow(((v[i, j + 1] - v[i, j - 1]) / 2 / dy), 2);
                }
            }

            p = SolvePressurePoisson(p, b);

            //Update U and V values
            double[,] un = u.Clone() as double[,];
            double[,] vn = v.Clone() as double[,];

            for (int i = 1; i < _cellsX - 1; i++) {
                for (int j = 1; j < _cellsY - 1; j++) {

                    u[i, j] = un[i, j] - un[i, j] * _timestep / dx * (un[i, j] - un[i - 1, j]) - vn[i, j] * _timestep / dy * (un[i, j] - un[i, j - 1]) -
                              1 / _rho * (p[i + 1, j] - p[i - 1, j]) * _timestep / 2 / dx + _viscosityFactor * _timestep / (dx * dx) * (un[i + 1, j] - 2 * un[i, j] + un[i - 1, j])
                              + _viscosityFactor * _timestep / (dy * dy) * (un[i, j + 1] - 2 * un[i, j] + un[i, j - 1]);

                    v[i, j] = vn[i, j] - un[i, j] * _timestep / dx * (vn[i, j] - vn[i - 1, j]) - vn[i, j] * _timestep / dy * (vn[i, j] - vn[i, j - 1]) -
                              1 / _rho * (p[i, j + 1] - p[i, j - 1]) * _timestep / 2 / dy + _viscosityFactor * _timestep / (dx * dx) * (vn[i + 1, j] - 2 * vn[i, j] + vn[i - 1, j])
                              + _viscosityFactor * _timestep / (dy * dy) * (vn[i, j + 1] - 2 * vn[i, j] + vn[i, j - 1]);
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

    double[,] SolvePressurePoisson(double[,] p, double[,] b) {
        for (int iit = 0; iit < _numberIterations; iit++) {
            double[,] pn = p.Clone() as double[,];

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
                //p[i, ny - 1] = p[i, ny - 2];
                p[i, _cellsY - 1] = 0;
            }
        }

        return p;
    }

}
