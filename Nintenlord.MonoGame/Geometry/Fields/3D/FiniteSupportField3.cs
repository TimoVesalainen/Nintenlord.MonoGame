using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.MonoGame.Geometry.Fields._3D
{
    public sealed class FiniteSupportField3<T> : IField3i<T>
    {
        readonly int startX;
        readonly int startY;
        readonly int startZ;
        readonly T[,,] items;
        readonly T rest;

        public FiniteSupportField3(int startX, int startY, int startZ, T[,,] items, T rest)
        {
            this.startY = startY;
            this.startX = startX;
            this.startZ = startZ;
            this.items = items;
            this.rest = rest;
        }


        public T this[int x, int y, int z]
        {
            get
            {
                if (x >= startX && x < startX + items.GetLength(2) &&
                    y >= startY && y < startY + items.GetLength(1) &&
                    z >= startZ && z < startZ + items.GetLength(0))
                {
                    return items[z - startZ, y - startY, x - startX];
                }
                else
                {
                    return rest;
                }
            }
        }
    }
}
