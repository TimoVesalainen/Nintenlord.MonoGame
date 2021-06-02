using Microsoft.Xna.Framework;
using System;

namespace Nintenlord.MonoGame.Geometry.Fields
{
    public sealed class RandomGradientField3 : IVectorField3iTo3v
    {
        private readonly Vector3[,,] gradients;
        private readonly int mask;

        public RandomGradientField3(int hexLength, Random random)
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
                        gradients[z, y, x] = random.GetRandomUnitVector3();
                    }
                }
            }
        }

        private Vector3 GradientVector(int x, int y, int z)
        {
            return gradients[z & mask, y & mask, x & mask];
        }

        public Vector3 this[int x, int y, int z] => GradientVector(x, y, z);
    }
}
