using Microsoft.Xna.Framework;
using Nintenlord.Collections;
using Nintenlord.Utility.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.MonoGame.Geometry
{
    public static class IntegerVectorHelpers
    {
        public static IEnumerable<IntegerVector3> GetVectors(IEnumerable<int> xs, IEnumerable<int> ys, IEnumerable<int> zs)
        {
            if (xs is null)
            {
                throw new ArgumentNullException(nameof(xs));
            }

            if (ys is null)
            {
                throw new ArgumentNullException(nameof(ys));
            }

            if (zs is null)
            {
                throw new ArgumentNullException(nameof(zs));
            }

            foreach (var z in zs)
            {
                foreach (var y in ys)
                {
                    foreach (var x in xs)
                    {
                        yield return new IntegerVector3(x,y,z);
                    }
                }
            }
        }
        public static IEnumerable<IntegerVector3> GetVectors(int x, IEnumerable<int> ys, IEnumerable<int> zs) => GetVectors(new int[] { x }, ys, zs);
        public static IEnumerable<IntegerVector3> GetVectors(IEnumerable<int> xs, int y, IEnumerable<int> zs) => GetVectors(xs, new int[] { y }, zs);
        public static IEnumerable<IntegerVector3> GetVectors(IEnumerable<int> xs, IEnumerable<int> ys, int z) => GetVectors(xs, ys, new int[] { z });
        public static IEnumerable<IntegerVector3> GetVectors(int x, int y, IEnumerable<int> zs) => GetVectors(new int[] { x }, new int[] { y }, zs);
        public static IEnumerable<IntegerVector3> GetVectors(IEnumerable<int> xs, int y, int z) => GetVectors(xs, new int[] { y }, new int[] { z });
        public static IEnumerable<IntegerVector3> GetVectors(int x, IEnumerable<int> ys, int z) => GetVectors(new int[] { x }, ys, new int[] { z });

        public static IEnumerable<IntegerVector3> GetFromBottomToTop(int x, int y, int minZ, int maxZ)
        {
            return GetVectors(x, y, IntegerExtensions.GetIntegers(minZ, maxZ));
        }

        public static IEnumerable<IntegerVector3> GetFromTopToBottom(int x, int y, int minZ, int maxZ)
        {
            return GetVectors(x, y, IntegerExtensions.GetIntegers(maxZ, minZ, -1));
        }

        public static IEnumerable<IntegerVector3> GetRectX(int x, Rectangle rect)
        {
            foreach (var (y,z) in rect.GetPoints())
            {
                yield return new IntegerVector3(x, y, z);
            }
        }

        public static IEnumerable<IntegerVector3> GetRectY(int y, Rectangle rect)
        {
            foreach (var (x, z) in rect.GetPoints())
            {
                yield return new IntegerVector3(x, y, z);
            }
        }

        public static IEnumerable<IntegerVector3> GetRectZ(int z, Rectangle rect)
        {
            foreach (var (x, y) in rect.GetPoints())
            {
                yield return new IntegerVector3(x, y, z);
            }
        }

        public static IEnumerable<IntegerVector3> GetRadius(int r)
        {
            var range = IntegerExtensions.GetIntegers(-r, r);
            return GetVectors(range, range, range);
        }

        public static IEnumerable<IntegerVector3> GetRadiusHollow(int r)
        {
            var rangeFull = IntegerExtensions.GetIntegers(-r, r);
            var rangeEdgeless = IntegerExtensions.GetIntegers(-r + 1, r - 1);

            return GetVectors(r, rangeFull, rangeFull)
                .Concat(GetVectors(-r, rangeFull, rangeFull))
                .Concat(GetVectors(rangeEdgeless, r, rangeFull))
                .Concat(GetVectors(rangeEdgeless, -r, rangeFull))
                .Concat(GetVectors(rangeEdgeless, rangeEdgeless, r))
                .Concat(GetVectors(rangeEdgeless, rangeEdgeless, -r));
        }

        public static IEnumerable<IntegerVector3> GetRadiusX(int r, int x)
        {
            var range = IntegerExtensions.GetIntegers(-r, r);
            return GetVectors(x, range, range);
        }

        public static IEnumerable<IntegerVector3> GetRadiusY(int r, int y)
        {
            var range = IntegerExtensions.GetIntegers(-r, r);
            return GetVectors(range, y, range);
        }

        public static IEnumerable<IntegerVector3> GetRadiusZ(int r, int z)
        {
            var range = IntegerExtensions.GetIntegers(-r, r);
            return GetVectors(range, range, z);
        }

        public static IEnumerable<IntegerVector3> GetRadiusHollowX(int r, int x)
        {
            var rangeFull = IntegerExtensions.GetIntegers(-r, r);
            var rangeEdgeless = IntegerExtensions.GetIntegers(-r + 1, r - 1);

            return GetVectors(x, r, rangeFull)
                .Concat(GetVectors(x, -r, rangeFull))
                .Concat(GetVectors(x, rangeEdgeless, r))
                .Concat(GetVectors(x, rangeEdgeless, -r));
        }

        public static IEnumerable<IntegerVector3> GetRadiusHollowY(int r, int y)
        {
            var rangeFull = IntegerExtensions.GetIntegers(-r, r);
            var rangeEdgeless = IntegerExtensions.GetIntegers(-r + 1, r - 1);

            return GetVectors(r, y, rangeFull)
                .Concat(GetVectors(-r, y, rangeFull))
                .Concat(GetVectors(rangeEdgeless, y, r))
                .Concat(GetVectors(rangeEdgeless, y, -r));
        }

        public static IEnumerable<IntegerVector3> GetRadiusHollowZ(int r, int z)
        {
            var rangeFull = IntegerExtensions.GetIntegers(-r, r);
            var rangeEdgeless = IntegerExtensions.GetIntegers(-r + 1, r - 1);

            return GetVectors(r, rangeFull, z)
                .Concat(GetVectors(-r, rangeFull, z))
                .Concat(GetVectors(rangeEdgeless, r, z))
                .Concat(GetVectors(rangeEdgeless, -r, z));
        }

        public static IEnumerable<IntegerVector3> GetTouching()
        {
            yield return IntegerVector3.UnitX;
            yield return -IntegerVector3.UnitX;
            yield return IntegerVector3.UnitY;
            yield return -IntegerVector3.UnitY;
            yield return IntegerVector3.UnitZ;
            yield return -IntegerVector3.UnitZ;
        }

        public static IEnumerable<IntegerVector3> GetCube(IntegerVector3 start, IntegerVector3 end)
        {
            return GetVectors(
                IntegerExtensions.GetIntegers(start.X, end.X),
                IntegerExtensions.GetIntegers(start.Y, end.Y),
                IntegerExtensions.GetIntegers(start.Z, end.Z));
        }

        public static IEnumerable<IntegerVector3> GetInsideBoundingBox(BoundingBox box)
        {
            IntegerVector3 min = IntegerVector3.Ceiling(box.Min);
            IntegerVector3 max = IntegerVector3.Floor(box.Max);

            return GetCube(min, max);
        }

        public static IEnumerable<IntegerVector3> GetAllVectors()
        {
            yield return IntegerVector3.Zero;

            for (int i = 1; i <= int.MaxValue; i++)
            {
                foreach (var vector in GetRadiusHollow(i))
                {
                    yield return vector;
                }
            }

            var invertibleInts = IntegerExtensions.GetIntegers(-int.MaxValue, int.MaxValue);

            var onlyMinValue = new int[] { int.MinValue };

            foreach (var vector in GetVectors(onlyMinValue, invertibleInts, invertibleInts))
            {
                yield return vector;
            }
            foreach (var vector in GetVectors(invertibleInts, onlyMinValue, invertibleInts))
            {
                yield return vector;
            }
            foreach (var vector in GetVectors(invertibleInts, invertibleInts, onlyMinValue))
            {
                yield return vector;
            }
            foreach (var vector in GetVectors(invertibleInts, onlyMinValue, onlyMinValue))
            {
                yield return vector;
            }
            foreach (var vector in GetVectors(onlyMinValue, invertibleInts, onlyMinValue))
            {
                yield return vector;
            }
            foreach (var vector in GetVectors(onlyMinValue, onlyMinValue, invertibleInts))
            {
                yield return vector;
            }
            yield return IntegerVector3.MinVector;
        }
    }
}
