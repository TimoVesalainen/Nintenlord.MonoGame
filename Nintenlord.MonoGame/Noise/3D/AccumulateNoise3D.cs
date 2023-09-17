using Microsoft.Xna.Framework;

namespace Nintenlord.MonoGame.Noise
{
    public sealed class AccumulateNoise3D : AccumulateNoiseBase
    {
        private const int offsetX = 499;
        private const int offsetY = 506;
        private const int offsetZ = 515;

        private readonly Vector3 offset = new Vector3(offsetX, offsetY, offsetZ);
        private readonly INoise3D noise;
        private readonly Matrix rotationMatrix;


        public AccumulateNoise3D(INoise3D noise, Quaternion rotation, float detail, float roughness)
            : base(detail, roughness)
        {
            this.noise = noise;

            rotationMatrix = Matrix.CreateFromQuaternion(rotation);
        }

        public float SumOctaves(Vector3 position)
        {
            float total = 0.0f;

            for (int i = 0; i < octaveLevels.Length; i++)
            {
                var level = octaveLevels[i];

                // rotate the coordinates.
                // reduces correlation between octaves.
                position = Vector3.Transform(position, rotationMatrix);

                var noiseLevel = (float)noise.Noise(position * level.Frequency);

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
                position += offset;
            }

            return total;
        }
    }
}
