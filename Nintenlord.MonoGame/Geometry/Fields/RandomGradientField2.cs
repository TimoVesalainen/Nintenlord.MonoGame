using Microsoft.Xna.Framework;
using System;

namespace Nintenlord.MonoGame.Geometry.Fields
{
    public sealed class RandomGradientField2 : IVectorField2iTo2v
    {
        readonly Vector2[,] gradients;
        readonly int mask;

        public RandomGradientField2(int hexLength, Random random)
        {
            int length = 1 << hexLength;
            mask = length - 1;
            gradients = new Vector2[length, length];
            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    gradients[y, x] = random.GetRandomUnitVector2();
                }
            }
        }

        private Vector2 GradientVector(int x, int y)
        {
            return gradients[y & mask, x & mask];
        }

        public Vector2 this[int x, int y] => GradientVector(x, y);
    }
}
