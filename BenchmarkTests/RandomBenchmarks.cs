using BenchmarkDotNet.Attributes;
using Moarx.Math;
using System;
using System.Security.Cryptography;

namespace BenchmarkTests;

[MemoryDiagnoser]
public class RandomBenchmarks {

    rng random = new rng();

    [Benchmark]
    public void RandomDouble0To1() {
        var rnd = MathmaticMethods.GetRandomDouble(0, 1);
    }

    [Benchmark]
    public void RandomDouble0To1new() {
        var rand = random.Uniform();
    }
}
