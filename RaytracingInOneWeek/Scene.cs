using Math_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaytracingInOneWeek
{
    class Scene : Shape
    {
        //Properties
        public List<Shape> objects;

        //Ctors
        public Scene()
        {
            objects = new List<Shape>();
        }
        public Scene(List<Shape> ob)
        {
            objects = ob;
        }

        public override bool hit(Ray r, double tMin, ref IntersectionData ID)
        {
            IntersectionData tmpId = new IntersectionData();
            bool hitAny = false;

            foreach(Shape element in objects)
            {
                if(element.hit(r, tMin, ref tmpId))
                {
                    hitAny = true;
                    r.tMax = tmpId.t;
                    ID = tmpId;
                }
            }

            return hitAny;
        }
    }
}
