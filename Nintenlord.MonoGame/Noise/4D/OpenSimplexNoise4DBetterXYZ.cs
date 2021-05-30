using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Geometry.Fields;

namespace Nintenlord.MonoGame.Noise
{
    /// <summary>
    /// Adopted from
    /// https://github.com/KdotJPG/OpenSimplex2/blob/3c64be93f7fadf3d67fbe45e4be5595eb83ef427/csharp/OpenSimplex2F.cs
    /// </summary>
    public sealed class OpenSimplexNoise4DBetterXYZ : INoise4D
    {
        private readonly OpenSimplexNoise4DBase baseNoise;

        public OpenSimplexNoise4DBetterXYZ(IVectorField4iTo4v gradientField)
        {
            baseNoise = new OpenSimplexNoise4DBase(gradientField);
        }

        /**
         * 4D OpenSimplex2F noise, with XYZ oriented like noise3_Classic,
         * and W for an extra degree of freedom. W repeats eventually.
         * Recommended for time-varied animations which texture a 3D object (W=time)
         */
        public double Noise(Vector4 position)
        {
            float xyz = position.X + position.Y + position.Z;
            float ww = position.W * 0.2236067977499788f;
            float s2 = xyz * -0.16666666666666666f + ww;
            float xs = position.X + s2, ys = position.Y + s2, zs = position.Z + s2, ws = -0.5f * xyz + ww;

            return baseNoise.Noise(new Vector4(xs, ys, zs, ws));
        }
    }
}
