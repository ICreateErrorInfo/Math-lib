using BenchmarkDotNet.Attributes;
using Math_lib;
using Math_lib.VertexAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkTests
{
    [MemoryDiagnoser]
    public class VertexBenchmarks
    {
        private Vertex vertex  = new Vertex(new(1, 1, 1)).GetAddAttribute(new TextureCoordinateVertexAttribute(new(1, 1)));
        private Vertex vertex2 = new Vertex(new(1, 2, 3)).GetAddAttribute(new TextureCoordinateVertexAttribute(new(1, 1)));

        [Benchmark]
        public void VertexAdd()
        {
            var erg = vertex + vertex2;
        }

        [Benchmark]
        public void VertexSub()
        {
            var erg = vertex - vertex2;
        }

        [Benchmark]
        public void VertexMul()
        {
            var erg = vertex * vertex2;
        }

        [Benchmark]
        public void VertexDiv()
        {
            var erg = vertex / vertex2;
        }
    }
}
