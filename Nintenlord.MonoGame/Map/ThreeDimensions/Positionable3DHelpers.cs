using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Geometry.Vectors;

namespace Nintenlord.MonoGame.Map.ThreeDimensions
{
    public static class Positionable3DHelpers
    {
        public static float Distance(this IPositionable3D value1, IPositionable3D value2)
        {
            var dist = value1.Position - value2.Position;
            return dist.Length();
        }

        public static float Distance(this IPositionable3D value1, Vector3 value2)
        {
            var dist = value1.Position - value2;
            return dist.Length();
        }

        public static float DistanceSquared(this IPositionable3D value1, IPositionable3D value2)
        {
            var dist = value1.Position - value2.Position;
            return dist.LengthSquared();
        }

        public static float DistanceSquared(this IPositionable3D value1, Vector3 value2)
        {
            var dist = value1.Position - value2;
            return dist.LengthSquared();
        }

        public static Vector3 DistanceVector(this IPositionable3D value1, IPositionable3D value2)
        {
            return value1.Position - value2.Position;
        }

        public static Vector3 DistanceVector(this IPositionable3D value1, Vector3 value2)
        {
            return value1.Position - value2;
        }

        public static double Distance(this IDiscretePositionable3D value1, IDiscretePositionable3D value2)
        {
            return IntegerVector3.Distance(value1.Position, value2.Position);
        }

        public static double Distance(this IDiscretePositionable3D value1, IntegerVector3 value2)
        {
            return IntegerVector3.Distance(value1.Position, value2);
        }

        public static int DistanceSquared(this IDiscretePositionable3D value1, IDiscretePositionable3D value2)
        {
            return IntegerVector3.DistanceSquared(value1.Position, value2.Position);
        }

        public static int DistanceSquared(this IDiscretePositionable3D value1, IntegerVector3 value2)
        {
            return IntegerVector3.DistanceSquared(value1.Position, value2);
        }
    }
}