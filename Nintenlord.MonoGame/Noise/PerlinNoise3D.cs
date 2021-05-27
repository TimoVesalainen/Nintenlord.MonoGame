using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Geometry;
using System;

namespace Nintenlord.MonoGame.Noise
{
    public sealed class PerlinNoise3D : AbstractPerlinNoise3D
    {
        readonly Vector3[,,] gradients;
        readonly int mask;

        public PerlinNoise3D(int hexLength, Random random)
        {
            int length = 1 << hexLength;
            mask = length - 1;
            gradients = new Vector3[length, length, length];
            for (int z = 0; z < length; z++)
            {
                for (int y = 0; y < length; y++)
                {
                    for (int x = 0; x < length; x++)
                    {
                        gradients[z, y, x] = random.GetRandomUnitVector();
                    }
                }
            }
        }


        protected override float Fade(float value)
        {
            return value * value * value * (value * (value * 6 - 15) + 10);
        }

        protected override Vector3 GradientVector(int x, int y, int z)
        {
            return gradients[z & mask, y & mask, x & mask];
        }
    }
}
