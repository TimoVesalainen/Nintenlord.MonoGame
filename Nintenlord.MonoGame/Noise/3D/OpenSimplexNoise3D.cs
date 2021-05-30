using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Geometry.Fields;

namespace Nintenlord.MonoGame.Noise
{
    /// <summary>
    /// Adopted from
    /// https://github.com/KdotJPG/OpenSimplex2/blob/3c64be93f7fadf3d67fbe45e4be5595eb83ef427/csharp/OpenSimplex2F.cs
    /// </summary>
    public sealed class OpenSimplexNoise3D : INoise3D
    {
        private readonly OpenSimplexNoise3DBase baseNoise;

        public OpenSimplexNoise3D(IVectorField3iTo3v gradientField)
        {
            baseNoise = new OpenSimplexNoise3DBase(gradientField);
        }

        /**
            * 3D Re-oriented 4-point BCC noise, classic orientation.
            * Proper substitute for 3D Simplex in light of Forbidden Formulae.
            * Use noise3_XYBeforeZ or noise3_XZBeforeY instead, wherever appropriate.
            */
        public double Noise(Vector3 position)
        {

            // Re-orient the cubic lattices via rotation, to produce the expected look on cardinal planar slices.
            // If texturing objects that don't tend to have cardinal plane faces, you could even remove this.
            // Orthonormal rotation. Not a skew transform.
            float r = (2.0f / 3.0f) * (position.X + position.Y + position.Z);

            // Evaluate both lattices to form a BCC lattice.
            return baseNoise.Noise(new Vector3(r) - position);
        }
    }
}
