using BenchmarkDotNet.Attributes;
using Math_lib;
using Math_lib.VertexAttributes;

namespace BenchmarkTests
{
    [MemoryDiagnoser]
    public class VertexBenchmarks
    {
        private readonly Vertex _vertex = new Vertex(new(1, 1, 1)).GetAddAttribute(new TextureCoordinateVertexAttribute(new(1, 1)));
        private readonly Vertex _vertex2 = new Vertex(new(1, 2, 3)).GetAddAttribute(new TextureCoordinateVertexAttribute(new(1, 1)));

        [Benchmark]
        public void VertexAdd()
        {
            var erg = _vertex + _vertex2;
        }

        //[Benchmark]
        //public void VertexSub()
        //{
        //    var erg = _vertex - _vertex2;
        //}

        //[Benchmark]
        //public void VertexMul()
        //{
        //    var erg = _vertex * _vertex2;
        //}

        //[Benchmark]
        //public void VertexDiv()
        //{
        //    var erg = _vertex / _vertex2;
        //}
    }
}
