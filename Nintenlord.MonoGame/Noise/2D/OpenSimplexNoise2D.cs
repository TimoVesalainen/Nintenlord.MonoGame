using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Geometry.Fields;

namespace Nintenlord.MonoGame.Noise
{
    public sealed class OpenSimplexNoise2D : INoise2D
    {
        readonly OpenSimplexNoise2DBase noiseBase;

        public OpenSimplexNoise2D(IVectorField2iTo2v gradients)
        {
            noiseBase = new OpenSimplexNoise2DBase(gradients);
        }

        public double Noise(Vector2 position)
        {
            // Get points for A2* lattice
            float s = 0.366025403784439f * (position.X + position.Y);

            return noiseBase.Noise(position + new Vector2(s));
        }
    }
}
