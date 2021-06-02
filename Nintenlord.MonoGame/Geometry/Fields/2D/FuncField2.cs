using System;

namespace Nintenlord.MonoGame.Geometry.Fields._3D
{
    public sealed class FuncField2<T> : IField2i<T>
    {
        private readonly Func<int, int, T> func;

        public FuncField2(Func<int, int, T> func)
        {
            this.func = func;
        }

        public T this[int x, int y] => func(x, y);
    }
}
