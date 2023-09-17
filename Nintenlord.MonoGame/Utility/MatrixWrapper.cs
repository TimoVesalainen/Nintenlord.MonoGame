using Microsoft.Xna.Framework;
using Nintenlord.Matricis;

namespace Nintenlord.MonoGame.Utility
{
    public sealed class MatrixWrapper : IMatrix<float>
    {
        readonly Matrix matrix;

        public float this[int x, int y] => matrix[y, x];

        public int Width => 4;

        public int Height => 4;
    }
}
