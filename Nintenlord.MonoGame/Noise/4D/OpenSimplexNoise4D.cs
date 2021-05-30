using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Geometry.Fields;

namespace Nintenlord.MonoGame.Noise
{
    /// <summary>
    /// Adopted from
    /// https://github.com/KdotJPG/OpenSimplex2/blob/3c64be93f7fadf3d67fbe45e4be5595eb83ef427/csharp/OpenSimplex2F.cs
    /// </summary>
    public sealed class OpenSimplexNoise4D : INoise4D
    {
        private readonly OpenSimplexNoise4DBase baseNoise;

        public OpenSimplexNoise4D(IVectorField4iTo4v gradientField)
        {
            baseNoise = new OpenSimplexNoise4DBase(gradientField);
        }

        /**
         * 4D OpenSimplex2F noise, classic lattice orientation.
         */
        public double Noise(Vector4 position)
        {
            // Get points for A4 lattice
            float s = -0.138196601125011f * (Vector4.Dot(position, Vector4.One));

            return baseNoise.Noise(position + new Vector4(s));
        }
    }
}
