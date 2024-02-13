using Moarx.Math;
using Moarx.Graphics;
using System.Threading.Tasks;
using Moarx.Graphics.Color;

namespace SPH_FluidSim;
public class ParticleSystem {

    Particle[] particles;
    DirectGraphics graphics;
    DirectBitmap bitmap;

    public ParticleSystem(int particleCount) {
        particles = new Particle[particleCount];

        for(int i = 0; i < particleCount; i++) {
            particles[i] = new Particle() { Position = new(1920 / 2 + i * 50, 1080 / 2 + i * 50), Velocity = new(0) };
        }

        bitmap = DirectBitmap.Create(1920, 1080);
        bitmap.Clear(DirectColors.Black);
        graphics = DirectGraphics.Create(bitmap);
    }

    public async Task Update(double deltaTime) {
        bitmap.Clear(DirectColors.Black);

        for (int i = 0; i < particles.Length;i++) {
            particles[i].Velocity += new Vector2D<double>(0, 1) * 9.81 * deltaTime;
            particles[i].Position += particles[i].Velocity * deltaTime;
            particles[i] = ResolveCollision(particles[i]);

            particles[i].Draw(graphics);
        }
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
