using Moarx.Math;
using Moarx.Graphics;
using Moarx.Graphics.Color;

namespace SPH_FluidSim;
public struct Particle
{
    public Point2D<double> Position;
    public Vector2D<double> Velocity;
    public int particleSize;

    public Particle() {
        particleSize = 50;
    }

    public void Draw(DirectGraphics graphics) {
        graphics.DrawEllipse(new Ellipse2D<int>((Point2D<int>)Position, particleSize), new(DirectColors.LightBlue, 1, DirectColors.LightBlue));
    }
}
