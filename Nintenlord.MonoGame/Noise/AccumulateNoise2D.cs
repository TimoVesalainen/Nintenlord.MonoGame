using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.MonoGame.Noise
{
    public sealed class AccumulateNoise2D
    {
        private const int offsetX = 499;
        private const int offsetY = 506;
        private readonly INoise2D noise2;
        private readonly float rot11;
        private readonly float rot12;
        private readonly float rot21;
        private readonly float rot22;

        private readonly OctaveLevel[] octaveLevels;

        private struct OctaveLevel
        {
            public float Amplitude;
            public float PartialOctaveFactor;
            public float Frequency;
        }

        public AccumulateNoise2D(INoise2D noise, float angle, float detail, float roughness)
        {
            this.noise2 = noise;
            rot11 = (float)Math.Cos(angle);
            rot12 = -(float)Math.Sin(angle);
            rot21 = (float)Math.Sin(angle);
            rot22 = (float)Math.Cos(angle);
            var octaves = (int)Math.Ceiling(detail);

            octaveLevels = Amplitudes(roughness)
                .Zip(PartialOctaveFactors(detail), (amplitude, partialOctave) => (amplitude, partialOctave))
                .Zip(Frequencies(), (pair, freq) => new OctaveLevel()
                {
                    Amplitude = pair.amplitude,
                    PartialOctaveFactor = pair.partialOctave,
                    Frequency = freq
                }).Take(octaves).ToArray();
        }

        private IEnumerable<float> PartialOctaveFactors(float detail)
        {
            var partialOctaveFactor = detail;
            while (true)
            {
                yield return partialOctaveFactor;
                partialOctaveFactor -= 1.0f;
            }
        }

        private IEnumerable<float> Amplitudes(float roughness)
        {
            float amplitude = 1;
            while (true)
            {
                yield return amplitude;
                // scale amplitude for next octave.
                amplitude *= roughness;
                if (amplitude < 0.001)
                {
                    // if the contribution is going to be negligable,
                    // don't bother with higher octaves.
                    break;
                }
            }
        }

        private IEnumerable<float> Frequencies()
        {
            float frequency = 1;
            while (true)
            {
                yield return frequency;
                frequency += frequency;
            }
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

                var noise = (float)noise2.Noise(new Vector2(xr * level.Frequency, yr * level.Frequency));

                noise *= level.Amplitude;

                // if this is the last 'partial' octave,
                // reduce its contribution accordingly.
                if (level.PartialOctaveFactor < 1)
                {
                    noise *= level.PartialOctaveFactor;
                }

                total += noise;

                // offset the coordinates by prime numbers, with prime difference.
                // reduces correlation between octaves.
                position.X = xr + offsetX;
                position.Y = yr + offsetY;
            }

            return total;
        }
    }
}
