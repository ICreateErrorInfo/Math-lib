using System.Collections.Generic;

namespace Math_lib
{
    public class Mesh3D
    {

        public Mesh3D() {
            Triangles = new();
        }

        public List<Triangle3D> Triangles { get; }

        public static Mesh3D GetCube()
        {
            Mesh3D meshCube = new Mesh3D();

            meshCube.Triangles.Add(new Triangle3D(new Point3D(0, 0, 0), new Point3D(0, 1, 0), new Point3D(1, 1, 0)));
            meshCube.Triangles.Add(new Triangle3D(new Point3D(0, 0, 0), new Point3D(1, 1, 0), new Point3D(1, 0, 0)));
                                                                                               
            meshCube.Triangles.Add(new Triangle3D(new Point3D(1, 0, 0), new Point3D(1, 1, 0), new Point3D(1, 1, 1)));
            meshCube.Triangles.Add(new Triangle3D(new Point3D(1, 0, 0), new Point3D(1, 1, 1), new Point3D(1, 0, 1)));

            meshCube.Triangles.Add(new Triangle3D(new Point3D(1, 0, 1), new Point3D(1, 1, 1), new Point3D(0, 1, 1)));
            meshCube.Triangles.Add(new Triangle3D(new Point3D(1, 0, 1), new Point3D(0, 1, 1), new Point3D(0, 0, 1)));
                                           
            meshCube.Triangles.Add(new Triangle3D(new Point3D(0, 0, 1), new Point3D(0, 1, 1), new Point3D(0, 1, 0)));
            meshCube.Triangles.Add(new Triangle3D(new Point3D(0, 0, 1), new Point3D(0, 1, 0), new Point3D(0, 0, 0)));
                                             
            meshCube.Triangles.Add(new Triangle3D(new Point3D(0, 1, 0), new Point3D(0, 1, 1), new Point3D(1, 1, 1)));
            meshCube.Triangles.Add(new Triangle3D(new Point3D(0, 1, 0), new Point3D(1, 1, 1), new Point3D(1, 1, 0)));
                                                   
            meshCube.Triangles.Add(new Triangle3D(new Point3D(1, 0, 1), new Point3D(0, 0, 1), new Point3D(0, 0, 0)));
            meshCube.Triangles.Add(new Triangle3D(new Point3D(1, 0, 1), new Point3D(0, 0, 0), new Point3D(1, 0, 0)));

            return meshCube;
        }

    }

}