using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.MonoGame.Geometry.Fields._1D
{
    public sealed class FiniteSupportField1<T> : IField1i<T>
    {
        readonly int start;
        readonly T[] items;
        readonly T rest;

        public FiniteSupportField1(int start, T[] items, T rest)
        {
            this.start = start;
            this.items = items;
            this.rest = rest;
        }

        public T this[int x]
        {
            get
            {
                if (x >= start && x < start + items.Length)
                {
                    return items[x - start];
                }
                else
                {
                    return rest;
                }
            }
        }
    }
}
