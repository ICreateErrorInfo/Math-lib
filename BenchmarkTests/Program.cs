using BenchmarkDotNet.Running;

namespace BenchmarkTests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<VertexBenchmarks>();
        }
    }
}
