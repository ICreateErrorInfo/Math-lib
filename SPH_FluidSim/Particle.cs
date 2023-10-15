using Moarx.Math;
using Moarx.Rasterizer;
using System.Drawing;

namespace SPH_FluidSim; 
public struct Particle
{
    public Point2D<int> Position;
    public Vector2D<double> Velocity;

    public void Draw(DirectGraphics graphics) {
        graphics.DrawEllipse(new Ellipse2D<int>(Position, 50), new(DirectColors.LightBlue, 1, DirectColors.LightBlue));
    }
}
