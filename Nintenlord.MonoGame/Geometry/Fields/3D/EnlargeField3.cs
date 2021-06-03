using System;

namespace Nintenlord.MonoGame.Geometry.Fields._2D
{
    public sealed class EnlargeField3<T> : IField3i<T>
    {
        private readonly IField3i<T> original;
        private readonly T filler;
        private readonly int modX;
        private readonly int modY;
        private readonly int modZ;
        private readonly int xToUseTile;
        private readonly int yToUseTile;
        private readonly int zToUseTile;

        public EnlargeField3(IField3i<T> original, T filler, int modX, int modY, int modZ, int xToUseTile, int yToUseTile, int zToUseTile)
        {
            if (modX <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(modX), "Modulus can't be non-positive");
            }
            if (modY <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(modY), "Modulus can't be non-positive");
            }
            if (modZ <= 0)
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
            if (zToUseTile < 0 || zToUseTile >= modZ)
            {
                throw new ArgumentOutOfRangeException(nameof(xToUseTile), "Position in chunk to use needs to be in range [0..modZ)");
            }

            this.original = original;
            this.filler = filler;
            this.modX = modX;
            this.modY = modY;
            this.modZ = modZ;
            this.xToUseTile = xToUseTile;
            this.yToUseTile = yToUseTile;
            this.zToUseTile = zToUseTile;
        }

        public T this[int x, int y, int z]
        {
            get
            {
                var chunkX = Math.DivRem(x, modX, out var remainderX);
                var chunkY = Math.DivRem(y, modY, out var remainderY);
                var chunkZ = Math.DivRem(z, modZ, out var remainderZ);
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
                while (remainderZ < 0)
                {
                    chunkZ--;
                    remainderZ += modZ;
                }

                if (remainderX == xToUseTile && remainderY == yToUseTile && remainderZ == zToUseTile)
                {
                    return original[chunkX, chunkY, chunkZ];
                }
                else
                {
                    return filler;
                }
            }
        }
    }
}
