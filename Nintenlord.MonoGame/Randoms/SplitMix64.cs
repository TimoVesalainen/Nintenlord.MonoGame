﻿namespace Nintenlord.MonoGame.Randoms
{
    /// <summary>
    /// Random number generator adopted from:
    /// https://github.com/svaarala/duktape/blob/master/misc/splitmix64.c
    /// </summary>
    public static class SplitMix64
    {
        public static ulong Next(ulong previous)
        {
            ulong z = previous + 0x9E3779B97F4A7C15;
            z = (z ^ (z >> 30)) * 0xBF58476D1CE4E5B9;
            z = (z ^ (z >> 27)) * 0x94D049BB133111EB;
            return z ^ (z >> 31);
        }
    }
}
