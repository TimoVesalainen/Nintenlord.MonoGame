using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Geometry.Fields;

namespace Nintenlord.MonoGame.Noise
{
    /// <summary>
    /// Adopted from
    /// https://github.com/KdotJPG/OpenSimplex2/blob/3c64be93f7fadf3d67fbe45e4be5595eb83ef427/csharp/OpenSimplex2F.cs
    /// </summary>
    public sealed class OpenSimplexNoise2DBetterX : INoise2D
    {
        readonly OpenSimplexNoise2DBase noiseBase;

        public OpenSimplexNoise2DBetterX(IVectorField2iTo2v gradients)
        {
            noiseBase = new OpenSimplexNoise2DBase(gradients);
        }

        /**
         * 2D Simplex noise, with Y pointing down the main diagonal.
         * Might be better for a 2D sandbox style game, where Y is vertical.
         * Probably slightly less optimal for heightmaps or continent maps.
         */
        public double Noise(Vector2 position)
        {
            // Skew transform and rotation baked into one.
            float xx = position.X * 0.7071067811865476f;
            float yy = position.Y * 1.224744871380249f;

            return noiseBase.Noise(new Vector2(yy + xx, yy - xx));
        }
    }
}
