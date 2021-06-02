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
