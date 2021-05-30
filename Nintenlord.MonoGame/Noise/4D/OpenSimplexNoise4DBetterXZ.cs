using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Geometry.Fields;

namespace Nintenlord.MonoGame.Noise
{
    /// <summary>
    /// Adopted from
    /// https://github.com/KdotJPG/OpenSimplex2/blob/3c64be93f7fadf3d67fbe45e4be5595eb83ef427/csharp/OpenSimplex2F.cs
    /// </summary>
    public sealed class OpenSimplexNoise4DBetterXZ : INoise4D
    {
        private readonly OpenSimplexNoise4DBase baseNoise;

        public OpenSimplexNoise4DBetterXZ(IVectorField4iTo4v gradientField)
        {
            baseNoise = new OpenSimplexNoise4DBase(gradientField);
        }

        /**
         * 4D OpenSimplex2F noise, with XZ and YW forming orthogonal triangular-based planes.
         * Recommended for 3D terrain, where X and Z (or Y and W) are horizontal.
         */
        public double Noise(Vector4 position)
        {
            float s2 = (position.X + position.Z) * -0.178275657951399372f + (position.Y + position.W) * 0.215623393288842828f;
            float t2 = (position.Y + position.W) * -0.403949762580207112f + (position.X + position.Z) * -0.375199083010075342f;

            return baseNoise.Noise(position + new Vector4(s2, t2, s2, t2));
        }
    }
}
