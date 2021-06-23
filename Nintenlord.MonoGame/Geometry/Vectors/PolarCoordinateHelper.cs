using Microsoft.Xna.Framework;
using System;

namespace Nintenlord.MonoGame.Geometry.Vectors
{
    public static class PolarCoordinateHelper
    {
        public static Vector2 PolarCoordinates(Vector2 normalCoordinates)
        {
            Vector2 result;
            result.X = (float)Math.Atan2(normalCoordinates.Y, normalCoordinates.X);
            result.Y = normalCoordinates.Length();
            return result;
        }

        public static Vector2 NormalCoordinates(Vector2 cartesianCoordinates)
        {
            Vector2 position;
            position.X = cartesianCoordinates.Y * (float)Math.Cos(cartesianCoordinates.X);
            position.Y = cartesianCoordinates.Y * (float)Math.Sin(cartesianCoordinates.X);
            return position;
        }

        public static Vector2 Rotate(Vector2 vector, float radians)
        {
            var cos = (float)Math.Cos(radians);
            var sin = (float)Math.Sin(radians);
            return new Vector2(vector.X * cos - vector.Y * sin, vector.X * sin + vector.Y * cos);
        }
    }
}