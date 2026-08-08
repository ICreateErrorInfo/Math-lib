using System;

namespace Moarx.Math;
public class rng {

    private ulong state, inc;
    private const ulong PCG32_DEFAULT_STATE = 0x853c49e6748fea9bUL;
    private const ulong PCG32_DEFAULT_STREAM = 0xda3e39cb94b95bdbUL;
    private const ulong PCG32_MULT = 0x5851f42d4c957f2dUL;

    float FloatOneMinusEpsilon = 1 - float.Epsilon;

    public rng() {
        state = PCG32_DEFAULT_STATE;
        inc = PCG32_DEFAULT_STREAM;
    }

    public uint UniformInt() {
        ulong oldstate = state;
        state = oldstate * PCG32_MULT + inc;
        uint xorshifted = (uint)(((oldstate >> 18) ^ oldstate) >> 27);
        int rot = (int)(oldstate >> 59);
        return (xorshifted >> rot) | (xorshifted << ((~rot + 1) & 31));
    }

    public double Uniform() {
        return System.Math.Min(FloatOneMinusEpsilon, UniformInt() * (1.0 / uint.MaxValue));
    }
}
