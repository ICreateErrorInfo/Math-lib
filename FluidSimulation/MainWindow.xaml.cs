using Moarx.Math;
using Moarx.Rasterizer;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FluidSimulation;
public partial class MainWindow: Window {
    public MainWindow() {
        InitializeComponent();

        FluidSimulationData data = new FluidSimulationData() { 
            _cellsX = 20,
            _cellsY = 20,
            _numberIterations = 100,
            _numberTimesteps = 500,
            _rho = 1,
            _viscosityFactor = .1,
            _timestep = 0.01
        };

        data.boundaryConditions = new BoundaryConditionData[data._cellsX, data._cellsY];

        Rectangle2D<double> rectangleDomain = new Rectangle2D<double>(new(0, 0), new(2, 2));
        Rectangle2D<double> rectangleLeftWall = new Rectangle2D<double>(new(0, 0), new(0, 2));
        Rectangle2D<double> rectangleBottomWall = new Rectangle2D<double>(new(0, 2), new(2, 2));
        Rectangle2D<double> rectangleRightWall = new Rectangle2D<double>(new(2, 0), new(2, 2));
        Rectangle2D<double> rectangleTopWall = new Rectangle2D<double>(new(0, 0), new(2, 0));

        FluidObject domain = new FluidObject() {Geometry = new() { rectangleDomain}, type = FluidObjectType.Domain};
        FluidObject leftWall = new FluidObject() {Geometry = new() { rectangleLeftWall}, type = FluidObjectType.Collision};
        FluidObject bottomWall = new FluidObject() {Geometry = new() { rectangleBottomWall}, type = FluidObjectType.Collision};
        FluidObject rightWall = new FluidObject() {Geometry = new() { rectangleRightWall}, type = FluidObjectType.Collision};
        FluidObject topWall = new FluidObject() {Geometry = new() { rectangleTopWall}, type = FluidObjectType.Collision};

        MeshGenerator generator = new MeshGenerator(domain, data._cellsX, data._cellsY);
        generator.AddObject(leftWall);
        generator.AddObject(bottomWall);
        generator.AddObject(rightWall);
        generator.AddObject(topWall);

        data.boundaryConditions = generator.GetBoundaryConditions();

        //Boundary Conditions
        for (int i = 0; i < data._cellsX; i++) {
            data.boundaryConditions[i, data._cellsY - 1].UVelocity = 1;
        }

        //Boundary Conditions Pressure
        for (int j = 0; j < data._cellsY; j++) {
            data.boundaryConditions[0, j].Pressure = (p, i, j) => {
                return p[1, j];
            };
            data.boundaryConditions[data._cellsX - 1, j].Pressure = (p, i, j) => {
                return p[data._cellsX - 2, j];
            };
        }
        for (int i = 0; i < data._cellsX; i++) {
            data.boundaryConditions[i, 0].Pressure = (p, i, j) => {
                return p[i, 1];
            };
        }

        NavierStokesSim simulation = new NavierStokesSim(data);

        FluidInformation information = simulation.Solve();

        DrawVectorField(information.uVectorField, information.vVectorField);
        CreatePictureFromNegativArray(information.pressureGradient);
    }

    //Navier Stoke 2D
    //Channel flow
    public void NavierStokeChannelFlow2() {
        //InitialConditions
        int nx = 20, ny = 20;
        int nt = 100, nit = 100;
        double dt = 0.01, vis = 0.1, rho = 1;
        double F = 0.5;

        double dx = (double)2 / (nx - 1), dy = (double)2 / (ny - 1);
        double[,] u = new double[nx, ny];
        double[,] v = new double[nx, ny];
        double[,] p = new double[nx, ny];

        double[,] b = new double[nx, ny];
        //Calculation
        for (int it = 0; it < nt; it++) {
            //RHS poisson equation calculation
            for (int i = 1; i < nx - 1; i++) {
                for (int j = 1; j < ny - 1; j++) {
                    b[i, j] =
                        rho * ((u[i + 1, j] - u[i - 1, j]) / 2 / dx + (v[i, j + 1] - v[i, j - 1]) / 2 / dy) / dt + Math.Pow(((u[i + 1, j] - u[i - 1, j]) / 2 / dx), 2) +
                        2 * (u[i, j + 1] - u[i, j - 1]) / 2 / dy * (v[i + 1, j] - v[i, j - 1]) / 2 / dx + Math.Pow(((v[i, j + 1] - v[i, j - 1]) / 2 / dy), 2);
                }
            }

            int numRows = b.GetLength(0);
            int numCols = b.GetLength(1);

            // Periodic BC Pressure @ x = 2
            for (int j = 1; j < ny - 1; j++) {
                int i = nx - 1;

                b[nx - 1, j] =
                    rho * ((u[0, j] - u[i - 1, j]) / 2 / dx + (v[i, j + 1] - v[i, j - 1]) / 2 / dy) / dt + Math.Pow(((u[0, j] - u[i - 1, j]) / 2 / dx), 2) +
                    2 * (u[i, j + 1] - u[i, j - 1]) / 2 / dy * (v[0, j] - v[i, j - 1]) / 2 / dx + Math.Pow(((v[i, j + 1] - v[i, j - 1]) / 2 / dy), 2);
            }
            // Periodic BC Pressure @ x = 0
            for (int j = 1; j < ny - 1; j++) {
                int i = 0;

                b[0, j] =
                    rho * ((u[i + 1, j] - u[nx - 1, j]) / 2 / dx + (v[i, j + 1] - v[i, j - 1]) / 2 / dy) / dt + Math.Pow(((u[i + 1, j] - u[nx - 1, j]) / 2 / dx), 2) +
                    2 * (u[i, j + 1] - u[i, j - 1]) / 2 / dy * (v[i + 1, j] - v[i, j - 1]) / 2 / dx + Math.Pow(((v[i, j + 1] - v[i, j - 1]) / 2 / dy), 2);
            }


            //solving the poisson equation
            for (int iit = 0; iit < nit; iit++) {
                double[,] pn = p.Clone() as double[,];

                for (int i = 1; i < nx - 1; i++) {
                    for (int j = 1; j < ny - 1; j++) {
                        p[i, j] = ((pn[i + 1, j] + pn[i - 1, j]) * (dy * dy) + (pn[i, j + 1] + pn[i, j - 1]) * (dx * dx) - b[i, j] * (dx * dx * dy * dy)) / (dx * dx + dy * dy) / 2;
                    }
                }

                // Periodic BC Pressure @ x = 2
                for (int j = 1; j < ny - 1; j++) {
                    int i = nx - 1;

                    p[nx - 1, j] = ((pn[0, j] + pn[i - 1, j]) * (dy * dy) + (pn[i, j + 1] + pn[i, j - 1]) * (dx * dx) - b[i, j] * (dx * dx * dy * dy)) / (dx * dx + dy * dy) / 2;
                }
                // Periodic BC Pressure @ x = 0
                for (int j = 1; j < ny - 1; j++) {
                    int i = 0;

                    p[0, j] = ((pn[i + 1, j] + pn[nx - 1, j]) * (dy * dy) + (pn[i, j + 1] + pn[i, j - 1]) * (dx * dx) - b[i, j] * (dx * dx * dy * dy)) / (dx * dx + dy * dy) / 2;
                }

                //Boundary Conditions
                for (int i = 0; i < nx; i++) {
                    p[i, 0] = p[i, 1];
                    p[i, ny - 1] = p[i, ny - 2];
                }
            }

            //Update U and V values
            double[,] un = u.Clone() as double[,];
            double[,] vn = v.Clone() as double[,];

            for (int i = 1; i < nx - 1; i++) {
                for (int j = 1; j < ny - 1; j++) {
                    u[i, j] = un[i, j] - un[i, j] * dt / dx * (un[i, j] - un[i - 1, j]) - vn[i, j] * dt / dy * (un[i, j] - un[i, j - 1]) -
                              1 / rho * (p[i + 1, j] - p[i - 1, j]) * dt / 2 / dx + vis * dt / (dx * dx) * (un[i + 1, j] - 2 * un[i, j] + un[i - 1, j])
                              + vis * dt / (dy * dy) * (un[i, j + 1] - 2 * un[i, j] + un[i, j - 1]) + dt * F;

                    v[i, j] = vn[i, j] - un[i, j] * dt / dx * (vn[i, j] - vn[i - 1, j]) - vn[i, j] * dt / dy * (vn[i, j] - vn[i, j - 1]) -
                              1 / rho * (p[i, j + 1] - p[i, j - 1]) * dt / 2 / dy + vis * dt / (dx * dx) * (vn[i + 1, j] - 2 * vn[i, j] + vn[i - 1, j])
                              + vis * dt / (dy * dy) * (vn[i, j + 1] - 2 * vn[i, j] + vn[i, j - 1]);
                }
            }

            // Periodic BC u @ x = 2
            for (int j = 1; j < ny - 1; j++) {
                int i = nx - 1;

                u[nx - 1, j] =
                    un[i, j] - un[i, j] * dt / dx * (un[i, j] - un[i - 1, j]) - vn[i, j] * dt / dy * (un[i, j] - un[i, j - 1]) -
                    1 / rho * (p[0, j] - p[i - 1, j]) * dt / 2 / dx + vis * dt / (dx * dx) * (un[0, j] - 2 * un[i, j] + un[i - 1, j])
                    + vis * dt / (dy * dy) * (un[i, j + 1] - 2 * un[i, j] + un[i, j - 1]) + dt * F;
            }
            // Periodic BC u @ x = 0
            for (int j = 1; j < ny - 1; j++) {
                int i = 0;

                u[0, j] =
                    un[i, j] - un[i, j] * dt / dx * (un[i, j] - un[nx - 1, j]) - vn[i, j] * dt / dy * (un[i, j] - un[i, j - 1]) -
                    1 / rho * (p[i + 1, j] - p[nx - 1, j]) * dt / 2 / dx + vis * dt / (dx * dx) * (un[i + 1, j] - 2 * un[i, j] + un[nx - 1, j])
                    + vis * dt / (dy * dy) * (un[i, j + 1] - 2 * un[i, j] + un[i, j - 1]) + dt * F;
            }

            // Periodic BC v @ x = 2
            for (int j = 1; j < ny - 1; j++) {
                int i = nx - 1;

                v[nx - 1, j] =
                    vn[i, j] - un[i, j] * dt / dx * (vn[i, j] - vn[i - 1, j]) - vn[i, j] * dt / dy * (vn[i, j] - vn[i, j - 1]) -
                    1 / rho * (p[i, j + 1] - p[i, j - 1]) * dt / 2 / dy + vis * dt / (dx * dx) * (vn[0, j] - 2 * vn[i, j] + vn[i - 1, j])
                    + vis * dt / (dy * dy) * (vn[i, j + 1] - 2 * vn[i, j] + vn[i, j - 1]);
            }
            // Periodic BC v @ x = 0
            for (int j = 1; j < ny - 1; j++) {
                int i = 0;

                v[0, j] =
                    vn[i, j] - un[i, j] * dt / dx * (vn[i, j] - vn[nx - 1, j]) - vn[i, j] * dt / dy * (vn[i, j] - vn[i, j - 1]) -
                    1 / rho * (p[i, j + 1] - p[i, j - 1]) * dt / 2 / dy + vis * dt / (dx * dx) * (vn[i + 1, j] - 2 * vn[i, j] + vn[nx - 1, j])
                    + vis * dt / (dy * dy) * (vn[i, j + 1] - 2 * vn[i, j] + vn[i, j - 1]);
            }

            //Boundary Conditions
            for (int i = 0; i < nx; i++) {
                u[i, 0] = 0;
                v[i, 0] = 0;

                u[i, ny - 1] = 0;
                v[i, ny - 1] = 0;
            }

            u[9, 9] = 0;
            v[9, 9] = 0;

            u[10, 10] = 0;
            v[10, 10] = 0;

            u[11, 11] = 0;
            v[11, 11] = 0;

            u[12, 12] = 0;
            v[12, 12] = 0;
        }

        DrawVectorField(u, v);
        //CreatePictureFromNegativArray(u);
        CreatePictureFromNegativArray(p);
    }
    //Cavity flow
    public void NavierStokeCavityFlow2() {
        //InitialConditions
        int nx = 20, ny = 20;
        int nt = 500, nit = 100;
        double dt = 0.01, vis = .1, rho = 1;

        double dx = (double)2 / (nx - 1), dy = (double)2 / (ny - 1);
        double[,] u = new double[nx, ny];
        double[,] v = new double[nx, ny];
        double[,] p = new double[nx, ny];

        double[,] b = new double[nx, ny];
        //Calculation
        for (int it = 0; it < nt; it++) {
            //RHS poisson equation calculation
            for (int i = 1; i < nx - 1; i++) {
                for (int j = 1; j < ny - 1; j++) {
                    b[i, j] =
                        rho * ((u[i + 1, j] - u[i - 1, j]) / 2 / dx + (v[i, j + 1] - v[i, j - 1]) / 2 / dy) / dt + Math.Pow(((u[i + 1, j] - u[i - 1, j]) / 2 / dx), 2) +
                        2 * (u[i, j + 1] - u[i, j - 1]) / 2 / dy * (v[i + 1, j] - v[i, j - 1]) / 2 / dx + Math.Pow(((v[i, j + 1] - v[i, j - 1]) / 2 / dy), 2);
                }
            }

            //solving the poisson equation
            for (int iit = 0; iit < nit; iit++) {
                double[,] pn = p.Clone() as double[,];

                for (int i = 1; i < nx - 1; i++) {
                    for (int j = 1; j < ny - 1; j++) {
                        p[i, j] = ((pn[i + 1, j] + pn[i - 1, j]) * (dy * dy) + (pn[i, j + 1] + pn[i, j - 1]) * (dx * dx) - b[i, j] * (dx * dx * dy * dy)) / (dx * dx + dy * dy) / 2;
                    }
                }

                //Boundary Conditions
                for (int j = 0; j < ny; j++) {
                    p[0, j] = p[1, j];
                    p[nx - 1, j] = p[nx - 2, j];
                }
                for (int i = 0; i < nx; i++) {
                    p[i, 0] = 0;
                    p[i, ny - 1] = 0;
                }
            }

            //Update U and V values
            double[,] un = u.Clone() as double[,];
            double[,] vn = v.Clone() as double[,];

            for (int i = 1; i < nx - 1; i++) {
                for (int j = 1; j < ny - 1; j++) {
                    u[i, j] = un[i, j] - un[i, j] * dt / dx * (un[i, j] - un[i - 1, j]) - vn[i, j] * dt / dy * (un[i, j] - un[i, j - 1]) -
                              1 / rho * (p[i + 1, j] - p[i - 1, j]) * dt / 2 / dx + vis * dt / (dx * dx) * (un[i + 1, j] - 2 * un[i, j] + un[i - 1, j])
                              + vis * dt / (dy * dy) * (un[i, j + 1] - 2 * un[i, j] + un[i, j - 1]);

                    v[i, j] = vn[i, j] - un[i, j] * dt / dx * (vn[i, j] - vn[i - 1, j]) - vn[i, j] * dt / dy * (vn[i, j] - vn[i, j - 1]) -
                              1 / rho * (p[i, j + 1] - p[i, j - 1]) * dt / 2 / dy + vis * dt / (dx * dx) * (vn[i + 1, j] - 2 * vn[i, j] + vn[i - 1, j])
                              + vis * dt / (dy * dy) * (vn[i, j + 1] - 2 * vn[i, j] + vn[i, j - 1]);
                }
            }

            //Boundary Conditions
            for (int j = 0; j < ny; j++) {
                u[0, j] = 0;
                v[0, j] = 0;

                u[nx - 1, j] = 0;
                v[nx - 1, j] = 0;
            }
            for (int i = 0; i < nx; i++) {
                u[i, 0] = 1;
                v[i, 0] = 0;

                u[i, ny - 1] = 1;
                v[i, ny - 1] = 0;
            }

            u[10, 9] = 0;
            u[10, 10] = 0;
            u[10, 11] = 0;

            v[10, 9] = 0;
            v[10, 10] = 0;
            v[10, 11] = 0;
        }

        DrawVectorField(u, v);
        CreatePictureFromNegativArray(p);
    }
    //Channel flow
    public void NavierStokeChannelFlow() {
        //InitialConditions
        int nx = 20, ny = 20;
        int nt = 100, nit = 100;
        double dt = 0.01, vis = 0.1, rho = 1;
        double F = 1;

        double dx = (double)2 / (nx - 1), dy = (double)2 / (ny - 1);
        double[,] u = new double[nx, ny];
        double[,] v = new double[nx, ny];
        double[,] p = new double[nx, ny];

        double[,] b = new double[nx, ny];
        //Calculation
        for (int it = 0; it < nt; it++) {
            //RHS poisson equation calculation
            for (int i = 1; i < nx - 1; i++) {
                for (int j = 1; j < ny - 1; j++) {
                    b[i, j] =
                        rho * ((u[i + 1, j] - u[i - 1, j]) / 2 / dx + (v[i, j + 1] - v[i, j - 1]) / 2 / dy) / dt + Math.Pow(((u[i + 1, j] - u[i - 1, j]) / 2 / dx), 2) +
                        2 * (u[i, j + 1] - u[i, j - 1]) / 2 / dy * (v[i + 1, j] - v[i, j - 1]) / 2 / dx + Math.Pow(((v[i, j + 1] - v[i, j - 1]) / 2 / dy), 2);
                }
            }

            int numRows = b.GetLength(0);
            int numCols = b.GetLength(1);

            // Periodic BC Pressure @ x = 2
            for (int j = 1; j < ny - 1; j++) {
                int i = nx - 1;

                b[nx - 1, j] =
                    rho * ((u[0, j] - u[i - 1, j]) / 2 / dx + (v[i, j + 1] - v[i, j - 1]) / 2 / dy) / dt + Math.Pow(((u[0, j] - u[i - 1, j]) / 2 / dx), 2) +
                    2 * (u[i, j + 1] - u[i, j - 1]) / 2 / dy * (v[0, j] - v[i, j - 1]) / 2 / dx + Math.Pow(((v[i, j + 1] - v[i, j - 1]) / 2 / dy), 2);
            }
            // Periodic BC Pressure @ x = 0
            for (int j = 1; j < ny - 1; j++) {
                int i = 0;

                b[0, j] =
                    rho * ((u[i + 1, j] - u[nx - 1, j]) / 2 / dx + (v[i, j + 1] - v[i, j - 1]) / 2 / dy) / dt + Math.Pow(((u[i + 1, j] - u[nx - 1, j]) / 2 / dx), 2) +
                    2 * (u[i, j + 1] - u[i, j - 1]) / 2 / dy * (v[i + 1, j] - v[i, j - 1]) / 2 / dx + Math.Pow(((v[i, j + 1] - v[i, j - 1]) / 2 / dy), 2);
            }


            //solving the poisson equation
            for (int iit = 0; iit < nit; iit++) {
                double[,] pn = p.Clone() as double[,];

                for (int i = 1; i < nx - 1; i++) {
                    for (int j = 1; j < ny - 1; j++) {
                        p[i, j] = ((pn[i + 1, j] + pn[i - 1, j]) * (dy * dy) + (pn[i, j + 1] + pn[i, j - 1]) * (dx * dx) - b[i, j] * (dx * dx * dy * dy)) / (dx * dx + dy * dy) / 2;
                    }
                }

                // Periodic BC Pressure @ x = 2
                for (int j = 1; j < ny - 1; j++) {
                    int i = nx - 1;

                    p[nx - 1, j] = ((pn[0, j] + pn[i - 1, j]) * (dy * dy) + (pn[i, j + 1] + pn[i, j - 1]) * (dx * dx) - b[i, j] * (dx * dx * dy * dy)) / (dx * dx + dy * dy) / 2;
                }
                // Periodic BC Pressure @ x = 0
                for (int j = 1; j < ny - 1; j++) {
                    int i = 0;

                    p[0, j] = ((pn[i + 1, j] + pn[nx - 1, j]) * (dy * dy) + (pn[i, j + 1] + pn[i, j - 1]) * (dx * dx) - b[i, j] * (dx * dx * dy * dy)) / (dx * dx + dy * dy) / 2;
                }

                //Boundary Conditions
                for (int i = 0; i < nx; i++) {
                    p[i, 0] = p[i, 1];
                    p[i, ny - 1] = p[i, ny - 2];
                }
            }

            //Update U and V values
            double[,] un = u.Clone() as double[,];
            double[,] vn = v.Clone() as double[,];

            for (int i = 1; i < nx - 1; i++) {
                for (int j = 1; j < ny - 1; j++) {
                    u[i, j] = un[i, j] - un[i, j] * dt / dx * (un[i, j] - un[i - 1, j]) - vn[i, j] * dt / dy * (un[i, j] - un[i, j - 1]) -
                              1 / rho * (p[i + 1, j] - p[i - 1, j]) * dt / 2 / dx + vis * dt / (dx * dx) * (un[i + 1, j] - 2 * un[i, j] + un[i - 1, j])
                              + vis * dt / (dy * dy) * (un[i, j + 1] - 2 * un[i, j] + un[i, j - 1]) + dt * F;

                    v[i, j] = vn[i, j] - un[i, j] * dt / dx * (vn[i, j] - vn[i - 1, j]) - vn[i, j] * dt / dy * (vn[i, j] - vn[i, j - 1]) -
                              1 / rho * (p[i, j + 1] - p[i, j - 1]) * dt / 2 / dy + vis * dt / (dx * dx) * (vn[i + 1, j] - 2 * vn[i, j] + vn[i - 1, j])
                              + vis * dt / (dy * dy) * (vn[i, j + 1] - 2 * vn[i, j] + vn[i, j - 1]);
                }
            }

            // Periodic BC u @ x = 2
            for (int j = 1; j < ny - 1; j++) {
                int i = nx - 1;

                u[nx - 1, j] =
                    un[i, j] - un[i, j] * dt / dx * (un[i, j] - un[i - 1, j]) - vn[i, j] * dt / dy * (un[i, j] - un[i, j - 1]) -
                    1 / rho * (p[0, j] - p[i - 1, j]) * dt / 2 / dx + vis * dt / (dx * dx) * (un[0, j] - 2 * un[i, j] + un[i - 1, j])
                    + vis * dt / (dy * dy) * (un[i, j + 1] - 2 * un[i, j] + un[i, j - 1]) + dt * F;
            }
            // Periodic BC u @ x = 0
            for (int j = 1; j < ny - 1; j++) {
                int i = 0;

                u[0, j] =
                    un[i, j] - un[i, j] * dt / dx * (un[i, j] - un[nx - 1, j]) - vn[i, j] * dt / dy * (un[i, j] - un[i, j - 1]) -
                    1 / rho * (p[i + 1, j] - p[nx - 1, j]) * dt / 2 / dx + vis * dt / (dx * dx) * (un[i + 1, j] - 2 * un[i, j] + un[nx - 1, j])
                    + vis * dt / (dy * dy) * (un[i, j + 1] - 2 * un[i, j] + un[i, j - 1]) + dt * F;
            }

            // Periodic BC v @ x = 2
            for (int j = 1; j < ny - 1; j++) {
                int i = nx - 1;

                v[nx - 1, j] =
                    vn[i, j] - un[i, j] * dt / dx * (vn[i, j] - vn[i - 1, j]) - vn[i, j] * dt / dy * (vn[i, j] - vn[i, j - 1]) -
                    1 / rho * (p[i, j + 1] - p[i, j - 1]) * dt / 2 / dy + vis * dt / (dx * dx) * (vn[0, j] - 2 * vn[i, j] + vn[i - 1, j])
                    + vis * dt / (dy * dy) * (vn[i, j + 1] - 2 * vn[i, j] + vn[i, j - 1]);
            }
            // Periodic BC v @ x = 0
            for (int j = 1; j < ny - 1; j++) {
                int i = 0;

                v[0, j] =
                    vn[i, j] - un[i, j] * dt / dx * (vn[i, j] - vn[nx - 1, j]) - vn[i, j] * dt / dy * (vn[i, j] - vn[i, j - 1]) -
                    1 / rho * (p[i, j + 1] - p[i, j - 1]) * dt / 2 / dy + vis * dt / (dx * dx) * (vn[i + 1, j] - 2 * vn[i, j] + vn[nx - 1, j])
                    + vis * dt / (dy * dy) * (vn[i, j + 1] - 2 * vn[i, j] + vn[i, j - 1]);
            }

            //Boundary Conditions
            for (int i = 0; i < nx; i++) {
                u[i, 0] = 0;
                v[i, 0] = 0;

                u[i, ny - 1] = 0;
                v[i, ny - 1] = 0;
            }
        }

        DrawVectorField(u, v);
        //CreatePictureFromNegativArray(u);
        //CreatePictureFromNegativArray(p);
    }
    //Cavity flow
    public void NavierStokeCavityFlow() {
        //InitialConditions
        int nx = 20, ny = 20;
        int nt = 500, nit = 100;
        double dt = 0.01, vis = .1, rho = 1;

        double dx = (double)2/(nx - 1), dy = (double)2 /(ny - 1);
        double[,] u = new double[nx,ny];
        double[,] v = new double[nx,ny];
        double[,] p = new double[nx,ny];

        double[,] b = new double[nx,ny];
        //Calculation
        for (int it = 0; it < nt; it++) {
            //RHS poisson equation calculation
            for (int i = 1; i < nx - 1; i++) {
                for (int j = 1; j < ny - 1; j++) {
                    b[i, j] =
                        rho * ((u[i + 1, j] - u[i - 1, j]) / 2 / dx + (v[i, j + 1] - v[i, j - 1]) / 2 / dy) / dt + Math.Pow(((u[i + 1, j] - u[i - 1, j]) / 2 / dx), 2) +
                        2 * (u[i, j + 1] - u[i, j - 1]) / 2 / dy * (v[i + 1, j] - v[i, j - 1]) / 2 / dx + Math.Pow(((v[i, j + 1] - v[i, j - 1]) / 2 / dy), 2);
                }
            }

            //solving the poisson equation
            for (int iit = 0; iit < nit; iit++) {
                double[,] pn = p.Clone() as double[,];

                for (int i = 1; i < nx - 1; i++) {
                    for (int j = 1; j < ny - 1; j++) {
                        p[i, j] = ((pn[i + 1, j] + pn[i - 1, j]) * (dy * dy) + (pn[i, j + 1] + pn[i, j - 1]) * (dx * dx) - b[i, j] * (dx * dx * dy * dy)) / (dx * dx + dy * dy) / 2;
                    }
                }

                //Boundary Conditions
                for (int j = 0; j < ny; j++) {
                    p[0, j] = p[1, j];
                    p[nx - 1, j] = p[nx - 2, j];
                }
                for (int i = 0; i < nx; i++) {
                    p[i, 0] = p[i, 1];
                    //p[i, ny - 1] = p[i, ny - 2];
                    p[i, ny - 1] = 0;
                }
            }

            //Update U and V values
            double[,] un = u.Clone() as double[,];
            double[,] vn = v.Clone() as double[,];

            for (int i = 1; i < nx - 1; i++) {
                for (int j = 1; j < ny - 1; j++) {
                    u[i, j] = un[i, j] - un[i, j] * dt / dx * (un[i, j] - un[i - 1, j]) - vn[i, j] * dt / dy * (un[i, j] - un[i, j - 1]) -
                              1 / rho * (p[i + 1, j] - p[i - 1, j]) * dt / 2 / dx + vis * dt / (dx * dx) * (un[i + 1, j] - 2 * un[i, j] + un[i - 1, j])
                              + vis * dt / (dy * dy) * (un[i, j + 1] - 2 * un[i, j] + un[i, j - 1]);

                    v[i, j] = vn[i, j] - un[i, j] * dt / dx * (vn[i, j] - vn[i - 1, j]) - vn[i, j] * dt / dy * (vn[i, j] - vn[i, j - 1]) -
                              1 / rho * (p[i, j + 1] - p[i, j - 1]) * dt / 2 / dy + vis * dt / (dx * dx) * (vn[i + 1, j] - 2 * vn[i, j] + vn[i - 1, j])
                              + vis * dt / (dy * dy) * (vn[i, j + 1] - 2 * vn[i, j] + vn[i, j - 1]);
                }
            }

            //Boundary Conditions
            for (int j = 0; j < ny; j++) {
                u[0, j] = 0;
                v[0, j] = 0;

                u[nx - 1, j] = 0;
                v[nx - 1, j] = 0;
            }
            for (int i = 0; i < nx; i++) {
                u[i, 0] = 0;
                v[i, 0] = 0;

                u[i, ny - 1] = 1;
                v[i, ny - 1] = 0;
            }
        }

        DrawVectorField(u, v);
        CreatePictureFromNegativArray(p);
    }
    //2D poisson
    public void TwoDPoisson() {
        //variables
        int nx = 20, ny = 20, nit = 1000;
        double dx = (double)2 / (nx - 1), dy = (double)1 / (ny - 1);

        double[,] p = new double[nx, ny];
        double[,] b = new double[nx, ny];

        //boundary conditions
        b[nx / 4, ny / 4] = 100;
        b[nx * 3 / 4, ny * 3 / 4] = -100;

        for (int iit = 0; iit < nit; iit++) {
            double[,] pn = p.Clone() as double[,];
            for (int i = 1; i < nx - 1; i++) {
                for (int j = 1; j < ny - 1; j++) {
                    p[i, j] = ((pn[i + 1, j] + pn[i - 1, j]) * (dy * dy) + (pn[i, j + 1] + pn[i, j - 1]) * (dx * dx) - b[i, j] * (dx * dx * dy * dy)) / (dx * dx + dy * dy) / 2;
                }
            }
        }

        CreatePictureFromArray(p);
    }
    //2D laplace
    public void TwoDLaplace() {
        //variables
        int nx = 20, ny = 20, nit = 1000;
        double dx = (double)2 / (nx - 1), dy = (double)1/(ny - 1);

        double[,] p = new double[nx, ny];

        //boundary conditions
        for (int j = 0; j < ny; j++) {
            p[0, j] = 0; //p = 0 @ x = 0
            p[nx - 1, j] = j; //p = y @ x = 2
        }
        for (int i = 0; i < nx; i++) {
            p[i, 0] = p[i, 1]; // dp/dy = 0 @ y = 0
            p[i, nx - 1] = p[i, nx - 2]; // dp/dy = 0 @ y = 1
        }

        //p[(int)(2 / dx), (int)(1 / dy)] = 1; // Test //black @ x = 2 && y = 1

        for (int iit = 0; iit < nit; iit++) {
            double[,] pd = (double[,])p.Clone();
            for (int i = 1; i < nx - 1; i++) {
                for (int j = 1; j < ny - 1; j++) {
                    p[i, j] = ((pd[i + 1, j] + pd[i - 1, j]) * (dy * dy) + (pd[i, j + 1] + pd[i, j - 1]) * (dx * dx)) / (dx * dx + dy * dy) / 2;
                }
            }

            //bounary conditions
            for (int i = 0; i < nx; i++) {
                p[i, 0] = p[i, 1]; // dp/dy = 0 @ y = 0
                p[i, nx - 1] = p[i, nx - 2]; // dp/dy = 0 @ y = 1
            }
        }

        CreatePictureFromArray(p);
    }

    //2D
    public void TwoDLinearConvection() {
        int nx = 20, ny = 20;
        double nt = 50, dt = 0.01;
        double c = 1;
        double dx = (double)2/(nx-1), dy = (double)2 /(ny-1);

        double[,] u = new double[nx,ny];

        for (int i = 0; i < nx; i++) {
            for (int j = 0; j < ny; j++) {
                double x = i * dx;
                double y = j * dy;

                if (0.5 <= x && x <= 1 && 0.5 <= y && y <= 1) {
                    u[i, j] = 2;
                } else {
                    u[i, j] = 1;
                }
            }
        }

        //CreatePictureFromArray(u);

        for (int it = 0; it < nt; it++) {
            double[,] un = u; //Maybe bug
            for (int i = 1; i < nx - 1; i++) {
                for (int j = 1; j < ny - 1; j++) {
                    u[i, j] = un[i, j] - c * dt / dx * (un[i, j] - un[i - 1, j]) - c * dt / dy * (un[i, j] - un[i, j - 1]);
                }
            }
        }

        CreatePictureFromArray(u);
    }

    //1D
    //Explicid
    public void OneDBurgersEquation() {
        double timeInS = 0.5;
        double dt = 0.01;
        int nx = 20, nt = (int)(timeInS / dt);
        double vis = 0.1;
        double dx = (double)2 * Math.PI / (nx - 1);

        PointCollection u = new PointCollection();

        for (int i = 0; i < nx; i++) {
            double phi = Math.Exp(-Math.Pow((i * dx), 2) / (4 * vis)) + Math.Exp(-Math.Pow(((i * dx) - 2 * Math.PI), 2) / (4 * vis));
            double dphi = -0.5 / vis * (i * dx) * Math.Exp(-Math.Pow((i * dx), 2) / (4 * vis)) - (0.5 / vis * ((i * dx) - 2 * Math.PI) *
                          Math.Exp(-Math.Pow(((i * dx) - 2 * Math.PI), 2) / (4 * vis)));
            u.Add(new(i * dx, -2 * vis * dphi / phi + 4));
        }

        CreatePlot(2 * Math.PI, 9, u);

        for (int it = 0; it < nt; it++) {
            var oldU = u;
            for (int i = 1; i < nx - 1; i++) {
                u[i] = new(i * dx, oldU[i].Y - oldU[i].Y * dt / dx * (oldU[i].Y - oldU[i - 1].Y) +
                            Math.Pow(vis * dt / dx, 2) * (oldU[i + 1].Y - 2 * oldU[i].Y + oldU[i - 1].Y));
            }
        }

        CreatePlot(2 * Math.PI, 9, u);
    }
    public void OneDDiffusion() {
        int nx = 10, nt = 1;
        double dt = 0.1, vis = 0.1;
        double dx = (double)2 / (nx - 1);

        PointCollection u = new PointCollection();

        //Boundry Conditions
        for (int i = 0; i < nx; i++) {
            double currentX = i * dx;

            if (0.5 <= currentX && currentX <= 1) {
                u.Add(new Point(currentX, 2));
            } else {
                u.Add(new Point(currentX, 1));
            }
        }

        CreatePlot(2, 2.5, u);

        //Solve for U n+1
        for (int it = 0; it < nt; it++) {
            var oldU = u;
            for (int i = 1; i < nx - 1; i++) {
                u[i] = new(i * dx, oldU[i].Y + vis * dt / dx / dx * (oldU[i + 1].Y - 2 * oldU[i].Y + oldU[i - 1].Y));
            }
        }

        CreatePlot(2, 2.5, u);
    }
    public void InviscidBurgersEquation() {
        PointCollection points = new PointCollection();
        PointCollection originalPoints = new PointCollection();

        int nx = 20, nt = 50;
        double dt = 0.01;
        double dx = (double)2 / (nx - 1);

        for (int i = 0; i < nx; i++) {
            if (0.5 * nx / 2 <= i && i <= 1 * nx / 2) {
                points.Add(new Point(i * dx, 2));
                originalPoints.Add(new Point(i * dx, 2));
            } else {
                points.Add(new Point(i * dx, 1));
                originalPoints.Add(new Point(i * dx, 1));
            }
        }

        for (int it = 0; it < nt; it++) {
            var oldPoints = points;

            for (int i = 1; i < nx; i++) {
                points[i] = new Point(i * dx, oldPoints[i].Y - oldPoints[i].Y * dt / dx * (oldPoints[i].Y - oldPoints[i - 1].Y));
            }
        }

        CreatePlot(2, 2.5, points);
        CreatePlot(2, 2.5, originalPoints);
    }
    public void OneDLinearConvection() {
        PointCollection points = new PointCollection();
        PointCollection originalPoints = new PointCollection();

        int nx = 51, nt = 15;
        double dt = 0.05, c = 0.5;
        double dx = (double)2 / (nx - 1);

        for (int i = 0; i < nx; i++) {
            if (0.5 * nx / 2 <= i && i <= 1 * nx / 2) {
                points.Add(new Point(i * dx, 2));
                originalPoints.Add(new Point(i * dx, 2));
            } else {
                points.Add(new Point(i * dx, 1));
                originalPoints.Add(new Point(i * dx, 1));
            }
        }

        for (int it = 0; it < nt; it++) {
            var oldPoints = points;

            for (int i = 1; i < nx; i++) {
                points[i] = (new Point(i * dx, oldPoints[i].Y - c * dt / dx * (oldPoints[i].Y - oldPoints[i - 1].Y)));
            }
            CreatePlot(2, 2.5, points);
        }

        CreatePlot(2, 2.5, originalPoints);
    }

    //Implicid
    public void TwoDConvectionImplicit() {
        int nx = 20, ny = 20;
        int nt = 50;
        double dt = 0.005;
        double c = 1;
        double dx = 2.0 / (nx - 1);
        double dy = 2.0 / (ny - 1);

        double[,] u = new double[nx, ny];

        // Initial condition
        for (int i = 0; i < nx; i++) {
            for (int j = 0; j < ny; j++) {
                double x = i * dx;
                double y = j * dy;

                if (0.5 <= x && x <= 1 && 0.5 <= y && y <= 1) {
                    u[i, j] = 2;
                } else {
                    u[i, j] = 1;
                }
            }
        }

        CreatePictureFromArray(u);

        double alpha = c * dt / dx;

        // Apply the boundary conditions
        for (int j = 0; j < ny; j++) {
            u[0, j] = 1;          // u at x=0
            u[nx - 1, j] = 1;     // u at x=2
        }
        for (int i = 0; i < nx; i++) {
            u[i, 0] = 1;          // u at y=0
            u[i, ny - 1] = 1;     // u at y=2
        }

        // Solve for U n+1
        for (int it = 1; it <= nt; it++) {
            // Construct the coefficient matrix
            double[,] coefficientMatrix = new double[nx * ny, nx * ny];

            for (int i = 0; i < nx; i++) {
                for (int j = 0; j < ny; j++) {
                    int index = i * ny + j;  // Convert 2D index to 1D index

                    if (i != 0 && j != 0 && i != nx - 1 && j != ny - 1) {
                        double coefficient_i = 1 + 2 * alpha;
                        double coefficient_im1 = -alpha;
                        double coefficient_ip1 = 0;
                        double coefficient_jm1 = -alpha;
                        double coefficient_jp1 = 0;

                        // Set the coefficients in the matrix
                        coefficientMatrix[index, index] = coefficient_i;
                        coefficientMatrix[index, index - ny] = coefficient_im1;
                        coefficientMatrix[index, index + ny] = coefficient_ip1;
                        coefficientMatrix[index, index - 1] = coefficient_jm1;
                        coefficientMatrix[index, index + 1] = coefficient_jp1;
                    }
                    if (i == 0) {
                        coefficientMatrix[index, index] = 1;
                    }
                    if (j == 0) {
                        coefficientMatrix[index, index] = 1;
                    }
                    if (j == ny - 1) {
                        coefficientMatrix[index, index] = 1;
                    }
                    if (i == nx - 1) {
                        coefficientMatrix[index, index] = 1;
                    }
                }
            }

            // Construct the RHS vector 'b'
            double[] b = new double[nx * ny];
            for (int i = 0; i < nx; i++) {
                for (int j = 0; j < ny; j++) {
                    int index = i * ny + j;  // Convert 2D index to 1D index
                    b[index] = u[i, j];
                }
            }

            // Solve the 2D system of equations using Gauss-Seidel
            b = GaussSeidelSolver(100, 0, coefficientMatrix, b);

            // Update 'u' with the new values from 'b'
            for (int i = 0; i < nx; i++) {
                for (int j = 0; j < ny; j++) {
                    int index = i * ny + j;  // Convert 2D index to 1D index
                    u[i, j] = b[index];
                }
            }

            // Create a visualization of the updated 'u'
            CreatePictureFromArray(u);
        }
    }
    public void OneDConvectionImplicit() {
        int nx = 41, nt = 25;
        double dt = 0.025, c = 1;
        double dx = (double)2 / (nx - 1);

        double[,] coefficientMatrix = new double[nx, nx];
        double[] u = new double[nx];

        //initial Conditions
        for (int i = 0; i < nx; i++) {
            double currentX = i * dx;

            if (0.5 <= currentX && currentX <= 1) {
                u[i] = 2;
            } else {
                u[i] = 1;
            }
        }

        PointCollection points2 = new PointCollection();
        for (int i = 0; i < nx; i++) {
            double currentX = i * dx;
            points2.Add(new(currentX, u[i]));
        }

        CreatePlot(2, 2.5, points2);

        double alpha = c * dt / dx;

        //Solve for U n+1
        for (int it = 1; it <= nt; it++) {
            // Reset the coefficient matrix to zeros at each time step
            for (int i = 0; i < nx; i++) {
                for (int j = 0; j < nx; j++) {
                    coefficientMatrix[i, j] = 0;
                }
            }


            for (int i = 1; i < nx - 1; i++) {
                coefficientMatrix[i, i - 1] = -alpha;
                coefficientMatrix[i, i] = 1 + alpha;
                coefficientMatrix[i, i + 1] = 0;
            }

            // Apply the boundary conditions to the coefficient matrix
            coefficientMatrix[0, 0] = 1;
            coefficientMatrix[0, 1] = 0;
            coefficientMatrix[nx - 1, nx - 1] = 1;
            coefficientMatrix[nx - 1, nx - 2] = 0;

            u = GaussSeidelSolver(10000, 0, coefficientMatrix, u);
        }

        PointCollection points = new PointCollection();
        for (int i = 0; i < nx; i++) {
            double currentX = i * dx;
            points.Add(new(currentX, u[i]));
        }

        CreatePlot(2, 2.5, points);
    }
    public void OneDDIffusionImplicit() {
        int nx = 10, nt = 1000;
        double dt = 0.001, vis = 0.1;
        double dx = (double)2 / (nx - 1);

        double[,] coefficientMatrix = new double[nx, nx];
        double[] u = new double[nx];

        //initial Conditions
        for (int i = 0; i < nx; i++) {
            double currentX = i * dx;

            if (0.5 <= currentX && currentX <= 1) {
                u[i] = 2;
            } else {
                u[i] = 1;
            }
        }

        PointCollection points2 = new PointCollection();
        for (int i = 0; i < nx; i++) {
            double currentX = i * dx;
            points2.Add(new(currentX, u[i]));
        }

        CreatePlot(2, 2.5, points2);

        double alpha = vis * dt / (dx * dx);

        //Solve for U n+1
        for (int it = 1; it <= nt; it++) {
            for (int i = 1; i < nx - 1; i++) {
                coefficientMatrix[i, i - 1] = 0;
                coefficientMatrix[i, i] = 0;
                coefficientMatrix[i, i + 1] = 0;
            }

            for (int i = 1; i < nx - 1; i++) {
                coefficientMatrix[i, i - 1] = -alpha;
                coefficientMatrix[i, i] = 1 + 2 * alpha;
                coefficientMatrix[i, i + 1] = -alpha;
            }

            // Apply the boundary conditions to the coefficient matrix
            coefficientMatrix[0, 0] = 1;
            coefficientMatrix[0, 1] = 0;
            coefficientMatrix[nx - 1, nx - 1] = 1;
            coefficientMatrix[nx - 1, nx - 2] = 0;

            u = GaussSeidelSolver(10000, 0, coefficientMatrix, u);
        }

        PointCollection points = new PointCollection();
        for (int i = 0; i < nx; i++) {
            double currentX = i * dx;
            points.Add(new(currentX, u[i]));
        }

        CreatePlot(2, 2.5, points);
    }
    public double[] GaussSeidelSolver(int sweepNumbers, double tolerance, double[,] coefficientMatrix, double[] sourceVector) {
        //INIT
        double[] calculatedValues = new double[coefficientMatrix.GetLength(0)];

        for (int i = 0; i < calculatedValues.GetLength(0); i++) {
            calculatedValues[i] = 0;
        }

        //Solve
        for (int iteration = 0; iteration < sweepNumbers; iteration++) {
            for (int currentRow = 0; currentRow < coefficientMatrix.GetLength(0); currentRow++) {
                double sum1 = 0;

                for (int j = 0; j < coefficientMatrix.GetLength(0); j++) {
                    if (j != currentRow) {
                        sum1 += coefficientMatrix[currentRow, j] * calculatedValues[j];
                    }
                }

                if (coefficientMatrix[currentRow, currentRow] == 0) {
                    calculatedValues[currentRow] = 0;
                    continue;
                }
                calculatedValues[currentRow] = (sourceVector[currentRow] - sum1) / coefficientMatrix[currentRow, currentRow];
            }

            double error = 0;
            for (int i = 0; i < coefficientMatrix.GetLength(0); i++) {
                double sum = 0;
                for (int j = 0; j < coefficientMatrix.GetLength(0); j++) {
                    sum += coefficientMatrix[i, j] * calculatedValues[j];
                }
                error += sourceVector[i] - sum;
            }

            if (error <= tolerance) {
                return calculatedValues;
            }
        }

        return calculatedValues;
    }


    public void CreatePlot(double XSize, double YSize, PointCollection points) {
        var polyLine = new Polyline();
        polyLine.StrokeThickness = 2;
        polyLine.Stroke = System.Windows.Media.Brushes.Blue;
        polyLine.FillRule = FillRule.EvenOdd;

        foreach (var point in points) {
            polyLine.Points.Add(new Point(point.X * (Width / XSize), Height - (point.Y * (Height / YSize))));
        }

        Grid.Children.Add(polyLine);
    }
    public void CreatePictureFromArray(double[,] array) {
        System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(array.GetLength(0), array.GetLength(1));

        double max = 0.00001;
        foreach (var item in array) {
            if (Math.Abs(item) > max) {
                max = Math.Abs(item);
            }
        }

        for (int i = 0; i < array.GetLength(0); i++) {
            for (int j = 0; j < array.GetLength(1); j++) {
                int value = (int)(-(((Math.Abs(array[i, j])) / max) - 1) * 255);
                bmp.SetPixel(i, array.GetLength(1) - 1 - j, System.Drawing.Color.FromArgb(value, value, 255));
            }
        }

        image.Source = BitmapToImageSource(bmp);
    }
    public void CreatePictureFromNegativArray(double[,] array) {
        System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(array.GetLength(0), array.GetLength(1));

        double max = 0.00001;
        double min = 0.00001;

        foreach (var item in array) {
            if (item > max) {
                max = item;
            }
            if (item < min) {
                min = item;
            }
        }

        for (int i = 0; i < array.GetLength(0); i++) {
            for (int j = 0; j < array.GetLength(1); j++) {
                double currentValue = array[i, j];
                if (currentValue >= 0) {
                    int value = (int)((1 - (currentValue / max)) * 255);
                    bmp.SetPixel(i, array.GetLength(1) - 1 - j, System.Drawing.Color.FromArgb(value, 255, value));
                }

                if (currentValue < 0) {
                    int value = (int)((1 - (Math.Abs(currentValue) / Math.Abs(min))) * 255);
                    bmp.SetPixel(i, array.GetLength(1) - 1 - j, System.Drawing.Color.FromArgb(value, value, 255));
                }
            }
        }

        image.Source = BitmapToImageSource(bmp);
    }
    public void DrawVectorField(double[,] u, double[,] v) {
        Canvas canvas = new Canvas();

        double maxU = 0.0001;
        double maxV = 0.0001;

        foreach (var element in u) {
            if (element > maxU) {
                maxU = element;
            }
        }
        foreach (var element in v) {
            if (element > maxV) {
                maxV = element;
            }
        }

        int stepX = ((int)Height / u.GetLength(0));
        int stepY = ((int)Height / u.GetLength(1));

        int currentX = 0;
        int currentY = 0;

        for (int i = 0; i < u.GetLength(0); i++) {
            currentY = stepY;
            for (int j = 0; j < u.GetLength(1); j++) {
                Vector vector = new Vector(((double)u[i, j] / maxU) * stepX, ((double)v[i, j] / maxV) * stepY);

                canvas.Children.Add(new Line { X1 = currentX, Y1 = Height - currentY, X2 = currentX + vector.X, Y2 = Height - (currentY + vector.Y), Stroke = Brushes.Black });

                currentY += stepY;
            }
            currentX += stepX;
        }

        Grid.Children.Add(canvas);
    }
    BitmapImage BitmapToImageSource(System.Drawing.Bitmap bitmap) {
        using (MemoryStream memory = new MemoryStream()) {
            bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
            memory.Position = 0;
            BitmapImage bitmapimage = new BitmapImage();
            bitmapimage.BeginInit();
            bitmapimage.StreamSource = memory;
            bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapimage.EndInit();

            return bitmapimage;
        }
    }
    ImageSource ToImageSource(DirectBitmap bitmap) {

        var bs = BitmapSource.Create(
                pixelWidth: bitmap.Width,
                pixelHeight: bitmap.Height,
                dpiX: 96,
                dpiY: 96,
                pixelFormat: PixelFormats.Bgr24, 
                palette: null,
                pixels: bitmap.GetBytes(),
                stride: bitmap.Stride);

        return bs;

    }

}
