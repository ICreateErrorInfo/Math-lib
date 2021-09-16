using System;
using System.Windows.Media.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Drawing.Imaging;
using Math_lib;

namespace RaytracingInOneWeek
{
    public class Raytracer
    {
        int width;
        int height;
        double aspectRatio;

        public Raytracer(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public DirectBitmap Render()
        {
            //Image
            aspectRatio = (double)width / height;
            int samplesPerPixel = 1;
            int maxDepth = 50;


            //World
            Scene world = new Scene();
            world.objects.Add(new Sphere(new(0, 0, -1), 0.5));
            world.objects.Add(new Sphere(new(0, -100.5, -1), 100));


            //Camera 
            Camera cam = new Camera(aspectRatio);


            //random numbers
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            byte[] RandX = new byte[samplesPerPixel];
            byte[] RandY = new byte[samplesPerPixel];
            crypto.GetBytes(RandX);
            crypto.GetBytes(RandY);


            //Render Loop
            DirectBitmap bmp = new DirectBitmap(width, height);
            Parallel.For(0, height, y =>
            {
                for (int x = 0; x < width; x++)
                {
                    Vector3D pixelColor = new Vector3D();
                    for (int s = 0; s < samplesPerPixel; s++)
                    {
                        double u = (x *1) / (width - 1);
                        double v = (y *1) / (height - 1);

                        Ray r = cam.GetRay(u, v);
                        pixelColor += RayColor(r, world, maxDepth);
                    }

                    bmp.SetPixel(x, -(y - height + 1), pixelColor.ToColor(samplesPerPixel));
                }

            });

            return bmp;
        }

        private Vector3D RayColor(Ray r, Shape shape, int depth)
        {
            if(depth <= 0)
            {
                return new Vector3D(0,0,0);
            }

            IntersectionData Id = new IntersectionData();
            if (shape.hit(r, 0, ref Id))
            {
                Point3D target = Id.p + (Point3D)Id.normal + Vector3D.RandomInUnitSphere();
                return 0.5 * RayColor(new Ray(Id.p, target - Id.p), shape, depth - 1);
            }

            Vector3D unitDir = Vector3D.Normalize(r.d);
            var t = 0.5 * (unitDir.Y + 1);

            Vector3D colInVec = (1 - t) * 1 + t * new Vector3D(0.5, 0.7, 1);

            return colInVec;
        }
    }
}
