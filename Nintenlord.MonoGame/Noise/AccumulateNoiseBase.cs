using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.MonoGame.Noise
{
    public abstract class AccumulateNoiseBase
    {
        protected struct OctaveLevel
        {
            public float Amplitude;
            public float PartialOctaveFactor;
            public float Frequency;
        }

        protected readonly OctaveLevel[] octaveLevels;

        public AccumulateNoiseBase(float detail, float roughness)
        {
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

        private IEnumerable<float> PartialOctaveFactors(float detail)
        {
            var partialOctaveFactor = detail;
            while (true)
            {
                yield return partialOctaveFactor;
                partialOctaveFactor -= 1.0f;
            }
        }
    }
}