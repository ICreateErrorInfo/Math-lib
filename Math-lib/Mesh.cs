using System.Collections.Generic;

namespace Math_lib
{
    public class Mesh
    {

        public Mesh() {
            Triangles = new();
        }

        public List<Triangle3> Triangles { get; }

        public static Mesh GetCube()
        {
            Mesh meshCube = new Mesh();

            meshCube.Triangles.Add(new Triangle3(new Point3(0, 0, 0), new Point3(0, 1, 0), new Point3(1, 1, 0)));
            meshCube.Triangles.Add(new Triangle3(new Point3(0, 0, 0), new Point3(1, 1, 0), new Point3(1, 0, 0)));
                                                                                               
            meshCube.Triangles.Add(new Triangle3(new Point3(1, 0, 0), new Point3(1, 1, 0), new Point3(1, 1, 1)));
            meshCube.Triangles.Add(new Triangle3(new Point3(1, 0, 0), new Point3(1, 1, 1), new Point3(1, 0, 1)));

            meshCube.Triangles.Add(new Triangle3(new Point3(1, 0, 1), new Point3(1, 1, 1), new Point3(0, 1, 1)));
            meshCube.Triangles.Add(new Triangle3(new Point3(1, 0, 1), new Point3(0, 1, 1), new Point3(0, 0, 1)));
                                           
            meshCube.Triangles.Add(new Triangle3(new Point3(0, 0, 1), new Point3(0, 1, 1), new Point3(0, 1, 0)));
            meshCube.Triangles.Add(new Triangle3(new Point3(0, 0, 1), new Point3(0, 1, 0), new Point3(0, 0, 0)));
                                             
            meshCube.Triangles.Add(new Triangle3(new Point3(0, 1, 0), new Point3(0, 1, 1), new Point3(1, 1, 1)));
            meshCube.Triangles.Add(new Triangle3(new Point3(0, 1, 0), new Point3(1, 1, 1), new Point3(1, 1, 0)));
                                                   
            meshCube.Triangles.Add(new Triangle3(new Point3(1, 0, 1), new Point3(0, 0, 1), new Point3(0, 0, 0)));
            meshCube.Triangles.Add(new Triangle3(new Point3(1, 0, 1), new Point3(0, 0, 0), new Point3(1, 0, 0)));

            return meshCube;
        }

    }

}