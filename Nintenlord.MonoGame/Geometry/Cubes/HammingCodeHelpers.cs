using System.Linq;
using System.Text;
using Nintenlord.Utility.Primitives;

namespace Nintenlord.MonoGame.Geometry.Cubes
{
    public static class HammingCodeHelpers
    {
        public static int HammingDistance(this int value, int other)
        {
            var xor = value ^ other;

            return xor.CountOneBits();
        }
    }
}
