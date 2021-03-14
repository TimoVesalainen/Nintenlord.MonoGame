using Microsoft.Xna.Framework;

namespace Nintenlord.MonoGame.Geometry
{
    public static class MatrixExtensions
    {
        public static Matrix GetProperOrthogonal(float width, float height, float depthStart = 0, float depth = 1)
        {
            Matrix.CreateOrthographic(width, height, depthStart, depth, out Matrix orthographic);

            Matrix.CreateScale(1, -1, 1, out Matrix scale);
            Matrix.Multiply(ref scale, ref orthographic, out Matrix scaled);

            Matrix.CreateTranslation(-1, 1, 0, out scale);
            Matrix.Multiply(ref scaled, ref scale, out orthographic);

            return orthographic;

            //Original code:
            //var newProj =
            //    Matrix.CreateOrthographic(width, height, depthStart, 0depth
            //    * Matrix.CreateScale(1, -1, 1)
            //    * Matrix.CreateTranslation(-1, 1, 0);
            //return newProj;
        }
    }
}