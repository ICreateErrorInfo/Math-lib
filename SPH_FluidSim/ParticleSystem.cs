using Moarx.Rasterizer;
using System.Drawing;

namespace SPH_FluidSim; 
public class ParticleSystem {

    Particle particle;
    DirectGraphics graphics;
    DirectBitmap bitmap;

    public ParticleSystem() {
        particle = new Particle() { Position = new(1920/2, 1080/2), Velocity = new(0)};
        bitmap = DirectBitmap.Create(1920, 1080);
        bitmap.Clear(DirectColors.Black);
        graphics = DirectGraphics.Create(bitmap);
    }

    public void Update() {
        particle.Draw(graphics);
    }

    public DirectBitmap GetBitmap() {
        return bitmap;
    }

}
