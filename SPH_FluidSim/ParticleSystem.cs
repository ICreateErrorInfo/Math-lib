using Moarx.Math;
using Moarx.Rasterizer;
using System.Threading.Tasks;

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

    public async Task Update(double deltaTime) {
        bitmap.Clear(DirectColors.Black);
        particle.Velocity += new Vector2D<double>(0, 1) * 9.81 * deltaTime;
        particle.Position += particle.Velocity * deltaTime;
        particle = ResolveCollision(particle);

        particle.Draw(graphics);
    }

    Particle ResolveCollision(Particle particle) {
        Vector2D<double> particleBounds = new Vector2D<double>(particle.Position.X + particle.particleSize, particle.Position.Y + particle.particleSize);

        if(particleBounds.X > 1920) {
            particle.Position = new(1920 - particle.particleSize - 1, particle.Position.Y);
            particle.Velocity = new(particle.Velocity.X * -1, particle.Velocity.Y);
        }
        if (particleBounds.Y > 1080) {
            particle.Position = new(particle.Position.X, 1080 - particle.particleSize - 1);
            particle.Velocity = new(particle.Velocity.X, particle.Velocity.Y * -1);
        }

        return particle;
    }

    public DirectBitmap GetBitmap() {
        return bitmap;
    }

}
