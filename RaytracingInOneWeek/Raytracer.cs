using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //Calc AspectRatio
            aspectRatio = (double)width / height;

            //World
            Scene world = new Scene();
            world.objects.Add(new Sphere(new(0,0,-1), 0.5));
            world.objects.Add(new Sphere(new(0, -100.5, -1), 100));


            //Camera Init
            double vHeight = 2;
            double vWidth = aspectRatio * vHeight;
            double focalLength = 1;

            //Calc Virtual Viewport
            Point3D  origin     = new Point3D (0, 0, 0);
            Vector3D horizontal = new Vector3D(vWidth, 0, 0);
            Vector3D vertical   = new Vector3D(0, vHeight, 0);
            Point3D lowerLeftCorner = origin - horizontal / 2 - vertical / 2 - new Vector3D(0, 0, focalLength);

            //Render Loop
            DirectBitmap bmp = new DirectBitmap(width, height);
            for (int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    double u = (double)x / (width  - 1);
                    double v = (double)y / (height - 1);

                    Ray r = new Ray(origin, lowerLeftCorner + u * horizontal + v * vertical - origin);

                    bmp.SetPixel(x,-(y - height + 1), RayColor(r, world));
                }
            }

            return bmp;
        }

        private Color RayColor(Ray r, Shape shape)
        {
            IntersectionData Id = new IntersectionData();
            if (shape.hit(r, 0, ref Id))
            {
                return (0.5 * ((Vector3D)Id.normal + 1)).ToColor();
            }

            Vector3D unitDir = Vector3D.Normalize(r.d);
            var t = 0.5 * (unitDir.Y + 1);

            Vector3D colInVec = (1 - t) * 1 + t * new Vector3D(0.5, 0.7, 1);

            return colInVec.ToColor();
        }
    }
}
