using Math_lib;
using Raytracing.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracing.Accelerators
{
    public abstract class Aggregate : Primitive
    {
        public override Material GetMaterial()
        {
            throw new Exception("Can not call GetMaterial");
        }
    }
}
