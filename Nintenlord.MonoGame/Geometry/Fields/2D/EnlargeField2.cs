using System;

namespace Nintenlord.MonoGame.Geometry.Fields._2D
{
    public sealed class EnlargeField2<T> : IField2i<T>
    {
        private readonly IField2i<T> original;
        private readonly T filler;
        private readonly int modX;
        private readonly int modY;
        private readonly int xToUseTile;
        private readonly int yToUseTile;

        public EnlargeField2(IField2i<T> original, T filler, int modX, int modY, int xToUseTile, int yToUseTile)
        {
            if (modX <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(modX), "Modulus can't be non-positive");
            }
            if (modY <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(modY), "Modulus can't be non-positive");
            }
            if (xToUseTile < 0 || xToUseTile >= modX)
            {
                throw new ArgumentOutOfRangeException(nameof(xToUseTile), "Position in chunk to use needs to be in range [0..modX)");
            }
            if (yToUseTile < 0 || yToUseTile >= modY)
            {
                throw new ArgumentOutOfRangeException(nameof(yToUseTile), "Position in chunk to use needs to be in range [0..modY)");
            }

            this.original = original;
            this.filler = filler;
            this.modX = modX;
            this.modY = modY;
            this.xToUseTile = xToUseTile;
            this.yToUseTile = yToUseTile;
        }

        public T this[int x, int y]
        {
            get
            {
                var chunkX = Math.DivRem(x, modX, out var remainderX);
                var chunkY = Math.DivRem(y, modY, out var remainderY);
                while (remainderX < 0)
                {
                    chunkX--;
                    remainderX += modX;
                }
                while (remainderY < 0)
                {
                    chunkY--;
                    remainderY += modY;
                }

                if (remainderX == xToUseTile && remainderY == yToUseTile)
                {
                    return original[chunkX, chunkY];
                }
                else
                {
                    return filler;
                }
            }
        }
    }
}
