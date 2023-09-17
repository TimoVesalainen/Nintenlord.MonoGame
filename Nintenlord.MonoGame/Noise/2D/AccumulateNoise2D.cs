using Microsoft.Xna.Framework;
using System;

namespace Nintenlord.MonoGame.Noise
{
    public sealed class AccumulateNoise2D : AccumulateNoiseBase
    {
        private const int offsetX = 499;
        private const int offsetY = 506;
        private readonly INoise2D noise;
        private readonly float rot11;
        private readonly float rot12;
        private readonly float rot21;
        private readonly float rot22;

        public AccumulateNoise2D(INoise2D noise, float angle, float detail, float roughness)
            : base(detail, roughness)
        {
            this.noise = noise;
            rot11 = (float)Math.Cos(angle);
            rot12 = -(float)Math.Sin(angle);
            rot21 = (float)Math.Sin(angle);
            rot22 = (float)Math.Cos(angle);
        }

        public float SumOctaves(Vector2 position)
        {
            float total = 0.0f;

            for (int i = 0; i < octaveLevels.Length; i++)
            {
                var level = octaveLevels[i];

                // rotate the coordinates.
                // reduces correlation between octaves.
                var xr = position.X * rot11 + position.Y * rot12;
                var yr = position.X * rot21 + position.Y * rot22;

                var noiseLevel = (float)noise.Noise(new Vector2(xr * level.Frequency, yr * level.Frequency));

                noiseLevel *= level.Amplitude;

                // if this is the last 'partial' octave,
                // reduce its contribution accordingly.
                if (level.PartialOctaveFactor < 1)
                {
                    noiseLevel *= level.PartialOctaveFactor;
                }

                total += noiseLevel;

                // offset the coordinates by prime numbers, with prime difference.
                // reduces correlation between octaves.
                position.X = xr + offsetX;
                position.Y = yr + offsetY;
            }

            return total;
        }
    }
}
