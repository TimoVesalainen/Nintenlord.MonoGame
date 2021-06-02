using System;

namespace Nintenlord.MonoGame.Geometry.Fields._3D
{
    public sealed class FuncField3<T> : IField3i<T>
    {
        private readonly Func<int, int, int, T> func;

        public FuncField3(Func<int, int, int, T> func)
        {
            this.func = func;
        }

        public T this[int x, int y, int z] => func(x, y, z);
    }
}
