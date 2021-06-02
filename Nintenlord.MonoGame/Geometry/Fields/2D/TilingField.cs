using Nintenlord.Tilings;

namespace Nintenlord.MonoGame.Geometry.Fields._2D
{
    public sealed class TilingField<T> : IField2i<T>
    {
        private readonly ITiling<T> tiling;

        public TilingField(ITiling<T> tiling)
        {
            this.tiling = tiling;
        }

        public T this[int x, int y] => tiling[x, y];
    }
}
