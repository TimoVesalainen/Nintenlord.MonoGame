using Microsoft.Xna.Framework;
using Nintenlord.Matricis;
using System;
using System.Collections.Generic;
using System.Text;

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
