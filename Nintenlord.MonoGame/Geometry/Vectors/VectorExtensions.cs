using Microsoft.Xna.Framework;
using Nintenlord.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.MonoGame.Geometry.Vectors
{
    public static class VectorExtensions
    {
        #region Vector2

        public static Point Round(this Vector2 i)
        {
            var x = (float)Math.Round(i.X);
            var y = (float)Math.Round(i.Y);
            var p = new Point((int)x, (int)y);
            return p;
        }

        public static Vector2 ExactCenter(this Rectangle i)
        {
            return new Vector2(i.X + (float)i.Width / 2,
                               i.Y + (float)i.Height / 2);
        }

        public static Vector2 RelativeExactCenter(this Rectangle i)
        {
            return new Vector2((float)i.Width / 2, (float)i.Height / 2);
        }

        public static Vector2 Min(this IEnumerable<Vector2> vectors)
        {
            return vectors.Aggregate(Vector2.Min);
        }

        public static Vector2 Max(this IEnumerable<Vector2> vectors)
        {
            return vectors.Aggregate(Vector2.Max);
        }

        public static Tuple<Vector2, Vector2> MinMax(this IEnumerable<Vector2> vectors)
        {
            var min = new Vector2(float.MaxValue);
            var max = new Vector2(float.MinValue);

            foreach (Vector2 item in vectors)
            {
                min = Vector2.Min(min, item);
                max = Vector2.Max(max, item);
            }

            return Tuple.Create(min, max);
        }

        public static Tuple<Vector2, Vector2> MinMax(Vector2 a, Vector2 b)
        {
            Vector2 min = Vector2.Min(a, b);
            Vector2 max = Vector2.Max(a, b);

            return Tuple.Create(min, max);
        }

        public static Vector2 Abs(this Vector2 v)
        {
            v.X = Math.Abs(v.X);
            v.Y = Math.Abs(v.Y);
            return v;
        }

        public static float MaxCoord(this Vector2 v)
        {
            return Math.Max(v.X, v.Y);
        }

        public static float MinCoord(this Vector2 v)
        {
            return Math.Min(v.X, v.Y);
        }

        public static string ToString(this Vector2 v, string format)
        {
            return string.Format("{{{0}, {1}}}", v.X.ToString(format), v.Y.ToString(format));
        }


        public static Vector2 ProjectionX(this Vector2 v)
        {
            return new Vector2(v.X, 0);
        }

        public static Vector2 ProjectionY(this Vector2 v)
        {
            return new Vector2(0, v.Y);
        }

        public static Vector2 GetRandomUnitVector2(this Random random)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            var angle = random.NextDouble() * MathHelper.TwoPi;

            return new Vector2(
                (float)Math.Cos(angle),
                (float)Math.Sin(angle)
                );
        }

        #endregion

        #region Vector3

        public static Vector3 Min(this IEnumerable<Vector3> vectors)
        {
            return vectors.Aggregate(Vector3.Min);
        }

        public static Vector3 Max(this IEnumerable<Vector3> vectors)
        {
            return vectors.Aggregate(Vector3.Max);
        }

        public static Tuple<Vector3, Vector3> MinMax(this IEnumerable<Vector3> vectors)
        {
            var min = new Vector3(float.MaxValue);
            var max = new Vector3(float.MinValue);

            foreach (Vector3 item in vectors)
            {
                min = Vector3.Min(min, item);
                max = Vector3.Max(max, item);
            }

            return Tuple.Create(min, max);
        }

        public static Tuple<Vector3, Vector3> MinMax(Vector3 a, Vector3 b)
        {
            Vector3 min = Vector3.Min(a, b);
            Vector3 max = Vector3.Max(a, b);

            return Tuple.Create(min, max);
        }

        public static Vector3 Abs(this Vector3 v)
        {
            v.X = Math.Abs(v.X);
            v.Y = Math.Abs(v.Y);
            v.Z = Math.Abs(v.Z);
            return v;
        }

        public static float MaxCoord(this Vector3 v)
        {
            return Math.Max(Math.Max(v.X, v.Y), v.Z);
        }

        public static float MinCoord(this Vector3 v)
        {
            return Math.Min(Math.Min(v.X, v.Y), v.Z);
        }

        public static Vector3 ProjectionX(this Vector3 v)
        {
            return new Vector3(v.X, 0, 0);
        }

        public static Vector3 ProjectionY(this Vector3 v)
        {
            return new Vector3(0, v.Y, 0);
        }

        public static Vector3 ProjectionZ(this Vector3 v)
        {
            return new Vector3(0, 0, v.Z);
        }

        public static string ToString(this Vector3 v, string format)
        {
            return string.Format("{{{0}, {1}, {2}}}", v.X.ToString(format), v.Y.ToString(format), v.Z.ToString(format));
        }

        public static Vector3[] GramSchmidtProcess(IEnumerable<Vector3> basis)
        {
            List<Vector3> result = new List<Vector3>(4);

            foreach (var vector in basis)
            {
                var vector2 = vector;
                var baseVector = vector;
                for (int i = 0; i < result.Count; i++)
                {
                    var resI = result[i];
                    Vector3.Dot(ref resI, ref vector2, out float scalarTemp);

                    baseVector -= scalarTemp / resI.LengthSquared() * result[i];
                }
                if (baseVector.LengthSquared() < 0.0001f)
                {
                    baseVector.Normalize();
                    result.Add(baseVector);
                }

                if (result.Count == 3)
                {
                    break;
                }
            }

            return result.ToArray();
        }

        public static Vector3 GetRandomUnitVector3(this Random random)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            var theta = Math.Acos(2 * random.NextDouble() - 1);
            var phi = MathHelper.TwoPi * random.NextDouble();
            return new Vector3(
                (float)(Math.Cos(phi) * Math.Sin(theta)),
                (float)(Math.Sin(phi) * Math.Sin(theta)),
                (float)Math.Cos(theta));
        }

        #endregion
    }
}