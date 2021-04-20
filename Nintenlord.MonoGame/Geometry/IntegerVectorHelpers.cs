using Microsoft.Xna.Framework;
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

        public static IEnumerable<IntegerVector3> GetFromBottomToTop(int x, int y, int minZ, int maxZ)
        {
            var position = new IntegerVector3(x, y, 0);
            for (position.Z = minZ; position.Z <= maxZ; position.Z++)
            {
                yield return position;
            }
        }

        public static IEnumerable<IntegerVector3> GetFromTopToBottom(int x, int y, int minZ, int maxZ)
        {
            var position = new IntegerVector3(x, y, 0);
            for (position.Z = maxZ; position.Z <= minZ; position.Z--)
            {
                yield return position;
            }
        }

        public static IEnumerable<IntegerVector3> GetRadius(int r)
        {
            for (int i = -r; i <= r; i++)
            {
                for (int j = -r; j <= r; j++)
                {
                    for (int k = -r; k <= r; k++)
                    {
                        yield return new IntegerVector3(i, j, k);
                    }
                }
            }
        }

        public static IEnumerable<IntegerVector3> GetRadiusHollow(int r)
        {
            for (int z = -r; z <= r; z++)
            {
                for (int y = -r; y <= r; y++)
                {
                    yield return new IntegerVector3(-r, y, z);
                    yield return new IntegerVector3(r, y, z);
                }
            }

            for (int z = -r; z <= r; z++)
            {
                for (int x = -r + 1; x < r; x++)
                {
                    yield return new IntegerVector3(x, -r, z);
                    yield return new IntegerVector3(x, r, z);
                }
            }

            for (int y = -r + 1; y < r; y++)
            {
                for (int x = -r + 1; x < r; x++)
                {
                    yield return new IntegerVector3(x, y, -r);
                    yield return new IntegerVector3(x, y, r);
                }
            }
        }

        public static IEnumerable<IntegerVector3> GetRadius(int r, int z)
        {
            for (int i = -r; i <= r; i++)
            {
                for (int j = -r; j <= r; j++)
                {
                    yield return new IntegerVector3(i, j, z);
                }
            }
        }

        public static IEnumerable<IntegerVector3> GetRadiusHollow(int r, int z)
        {
            for (int j = -r; j <= r; j++)
            {
                yield return new IntegerVector3(-r, j, z);
                yield return new IntegerVector3(r, j, z);
            }


            for (int i = -r + 1; i < r; i++)
            {
                yield return new IntegerVector3(i, -r, z);
                yield return new IntegerVector3(i, r, z);
            }
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
            for (int i = start.X; i <= end.X; i++)
            {
                for (int j = start.Y; j <= end.Y; j++)
                {
                    for (int k = start.Z; k <= end.Z; k++)
                    {
                        yield return new IntegerVector3(i, j, k);
                    }
                }
            }
        }

        public static IEnumerable<IntegerVector3> GetCartesianProduct(IEnumerable<int> x, IEnumerable<int> y,
                                                                      IEnumerable<int> z)
        {
            var position = new IntegerVector3();

            foreach (int zPos in z)
            {
                position.Z = zPos;
                foreach (int yPas in y)
                {
                    position.Y = yPas;
                    foreach (int xPos in x)
                    {
                        position.X = xPos;
                        yield return position;
                    }
                }
            }
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

            IEnumerable<int> NotMinValue()//Can't be expressed as Enumerable.Range()
            {
                for (int i = -int.MaxValue; i <= int.MaxValue; i++)
                {
                    yield return i;
                }
            }
            var onlyMinValue = new int[] { int.MinValue };

            foreach (var vector in GetVectors(onlyMinValue, NotMinValue(), NotMinValue()))
            {
                yield return vector;
            }
            foreach (var vector in GetVectors(NotMinValue(), onlyMinValue, NotMinValue()))
            {
                yield return vector;
            }
            foreach (var vector in GetVectors(NotMinValue(), NotMinValue(), onlyMinValue))
            {
                yield return vector;
            }
            foreach (var vector in GetVectors(NotMinValue(), onlyMinValue, onlyMinValue))
            {
                yield return vector;
            }
            foreach (var vector in GetVectors(onlyMinValue, NotMinValue(), onlyMinValue))
            {
                yield return vector;
            }
            foreach (var vector in GetVectors(onlyMinValue, onlyMinValue, NotMinValue()))
            {
                yield return vector;
            }
            yield return IntegerVector3.MinVector;
        }
    }
}
