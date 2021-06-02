using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Noise;

namespace Nintenlord.MonoGame.Geometry.Fields
{
    public sealed class NoiseField3 : IField3i<double>
    {
        private readonly INoise3D noise;
        private readonly Vector3 scale;

        public NoiseField3(INoise3D noise, Vector3 scale)
        {
            this.noise = noise;
            this.scale = scale;
        }

        public double this[int x, int y, int z] => noise.Noise(new Vector3(x, y, z) * scale);
    }
}
