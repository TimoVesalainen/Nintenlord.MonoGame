using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Geometry.Fields;

namespace Nintenlord.MonoGame.Noise
{
    /// <summary>
    /// Adopted from
    /// https://github.com/KdotJPG/OpenSimplex2/blob/3c64be93f7fadf3d67fbe45e4be5595eb83ef427/csharp/OpenSimplex2F.cs
    /// </summary>
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
