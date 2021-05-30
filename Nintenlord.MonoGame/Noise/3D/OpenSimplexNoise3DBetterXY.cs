using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Geometry.Fields;

namespace Nintenlord.MonoGame.Noise
{
    /// <summary>
    /// Adopted from
    /// https://github.com/KdotJPG/OpenSimplex2/blob/3c64be93f7fadf3d67fbe45e4be5595eb83ef427/csharp/OpenSimplex2F.cs
    /// </summary>
    public sealed class OpenSimplexNoise3DBetterXY : INoise3D
    {
        private readonly OpenSimplexNoise3DBase baseNoise;

        public OpenSimplexNoise3DBetterXY(IVectorField3iTo3v gradientField)
        {
            baseNoise = new OpenSimplexNoise3DBase(gradientField);
        }

        /**
         * 3D Re-oriented 4-point BCC noise, with better visual isotropy in (X, Y).
         * Recommended for 3D terrain and time-varied animations.
         * The Z coordinate should always be the "different" coordinate in your use case.
         * If Y is vertical in world coordinates, call noise3_XYBeforeZ(x, z, Y) or use noise3_XZBeforeY.
         * If Z is vertical in world coordinates, call noise3_XYBeforeZ(x, y, Z).
         * For a time varied animation, call noise3_XYBeforeZ(x, y, T).
         */
        public double Noise(Vector3 position)
        {
            // Re-orient the cubic lattices without skewing, to make X and Y triangular like 2D.
            // Orthonormal rotation. Not a skew transform.
            float xy = position.X + position.Y;
            float s2 = xy * -0.211324865405187f;
            float zz = position.Z * 0.577350269189626f;
            float xr = position.X + s2 - zz;
            float yr = position.Y + s2 - zz;
            float zr = xy * 0.577350269189626f + zz;

            // Evaluate both lattices to form a BCC lattice.
            return baseNoise.Noise(new Vector3(xr, yr, zr));
        }
    }
}
