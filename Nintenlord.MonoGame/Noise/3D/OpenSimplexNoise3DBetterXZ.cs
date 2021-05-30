using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Geometry.Fields;

namespace Nintenlord.MonoGame.Noise
{
    /// <summary>
    /// Adopted from
    /// https://github.com/KdotJPG/OpenSimplex2/blob/3c64be93f7fadf3d67fbe45e4be5595eb83ef427/csharp/OpenSimplex2F.cs
    /// </summary>
    public sealed class OpenSimplexNoise3DBetterXZ : INoise3D
    {
        private readonly OpenSimplexNoise3DBase baseNoise;

        public OpenSimplexNoise3DBetterXZ(IVectorField3iTo3v gradientField)
        {
            baseNoise = new OpenSimplexNoise3DBase(gradientField);
        }

        /**
         * 3D Re-oriented 4-point BCC noise, with better visual isotropy in (X, Z).
         * Recommended for 3D terrain and time-varied animations.
         * The Y coordinate should always be the "different" coordinate in your use case.
         * If Y is vertical in world coordinates, call noise3_XZBeforeY(x, Y, z).
         * If Z is vertical in world coordinates, call noise3_XZBeforeY(x, Z, y) or use noise3_XYBeforeZ.
         * For a time varied animation, call noise3_XZBeforeY(x, T, y) or use noise3_XYBeforeZ.
         */
        public double Noise(Vector3 position)
        {
            // Re-orient the cubic lattices without skewing, to make X and Z triangular like 2D.
            // Orthonormal rotation. Not a skew transform.
            float xz = position.X + position.Z;
            float s2 = xz * -0.211324865405187f;
            float yy = position.Y * 0.577350269189626f;
            float xr = position.X + s2 - yy;
            float zr = position.Z + s2 - yy;
            float yr = xz * 0.577350269189626f + yy;

            // Evaluate both lattices to form a BCC lattice.
            return baseNoise.Noise(new Vector3(xr, yr, zr));
        }
    }
}
