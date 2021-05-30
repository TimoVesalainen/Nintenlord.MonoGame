using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Geometry.Fields;

namespace Nintenlord.MonoGame.Noise
{
    public sealed class OpenSimplexNoise2DBetterX : INoise2D
    {
        readonly OpenSimplexNoise2DBase noiseBase;

        public OpenSimplexNoise2DBetterX(IVectorField2iTo2v gradients)
        {
            noiseBase = new OpenSimplexNoise2DBase(gradients);
        }

        public double Noise(Vector2 position)
        {
            // Skew transform and rotation baked into one.
            float xx = position.X * 0.7071067811865476f;
            float yy = position.Y * 1.224744871380249f;

            return noiseBase.Noise(new Vector2(yy + xx, yy - xx));
        }
    }
}
