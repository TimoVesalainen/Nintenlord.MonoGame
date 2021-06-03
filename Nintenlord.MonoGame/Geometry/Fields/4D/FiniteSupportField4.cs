using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.MonoGame.Geometry.Fields._4D
{
    public sealed class FiniteSupportField4<T> : IField4i<T>
    {
        readonly int startX;
        readonly int startY;
        readonly int startZ;
        readonly int startW;
        readonly T[,,,] items;
        readonly T rest;

        public FiniteSupportField4(int startX, int startY, int startZ, int startW, T[,,,] items, T rest)
        {
            this.startY = startY;
            this.startX = startX;
            this.startZ = startZ;
            this.startW = startW;
            this.items = items;
            this.rest = rest;
        }


        public T this[int x, int y, int z, int w]
        {
            get
            {
                if (x >= startX && x < startX + items.GetLength(3) &&
                    y >= startY && y < startY + items.GetLength(2) &&
                    z >= startZ && z < startZ + items.GetLength(1) &&
                    w >= startW && w < startW + items.GetLength(0))
                {
                    return items[w - startW, z - startZ, y - startY, x - startX];
                }
                else
                {
                    return rest;
                }
            }
        }
    }
}
