using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Geometry.Fields;

namespace Nintenlord.MonoGame.Noise
{
    /// <summary>
    /// Adopted from
    /// https://github.com/KdotJPG/OpenSimplex2/blob/3c64be93f7fadf3d67fbe45e4be5595eb83ef427/csharp/OpenSimplex2F.cs
    /// </summary>
    public sealed class OpenSimplexNoise4DBetterXY : INoise4D
    {
        private readonly OpenSimplexNoise4DBase baseNoise;

        public OpenSimplexNoise4DBetterXY(IVectorField4iTo4v gradientField)
        {
            baseNoise = new OpenSimplexNoise4DBase(gradientField);
        }

        /**
         * 4D OpenSimplex2F noise, with XY and ZW forming orthogonal triangular-based planes.
         * Recommended for 3D terrain, where X and Y (or Z and W) are horizontal.
         * Recommended for noise(x, y, sin(time), cos(time)) trick.
         */
        public double Noise(Vector4 position)
        {
            float s2 = (position.X + position.Y) * -0.178275657951399372f + (position.Z + position.W) * 0.215623393288842828f;
            float t2 = (position.Z + position.W) * -0.403949762580207112f + (position.X + position.Y) * -0.375199083010075342f;

            return baseNoise.Noise(position + new Vector4(s2, s2, t2, t2));
        }
    }
}
