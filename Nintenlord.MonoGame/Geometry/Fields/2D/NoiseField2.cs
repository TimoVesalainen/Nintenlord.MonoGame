using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Noise;

namespace Nintenlord.MonoGame.Geometry.Fields
{
    public sealed class NoiseField2 : IField2i<double>
    {
        private readonly INoise2D noise;
        private readonly Vector2 scale;

        public NoiseField2(INoise2D noise, Vector2 scale)
        {
            this.noise = noise;
            this.scale = scale;
        }

        public double this[int x, int y] => noise.Noise(new Vector2(x, y) * scale);
    }
}
