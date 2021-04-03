using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.MonoGame.Geometry
{
    static public class BoundingBoxHelpers
    {
        public static IEnumerable<IntegerVector3> GetIntersectingUnitBoxes(this BoundingBox box)
        {
            var minX = (int)box.Min.X;
            var maxX = (int)Math.Ceiling(box.Max.X);
            var minY = (int)box.Min.Y;
            var maxY = (int)Math.Ceiling(box.Max.Y);
            var minZ = (int)box.Min.Z;
            var maxZ = (int)Math.Ceiling(box.Max.Z);

            for (int z = minZ; z <= maxZ; z++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    for (int x = minX; x <= maxX; x++)
                    {
                        yield return new IntegerVector3(x, y, z);
                    }
                }
            }
        }

        public static IEnumerable<IntegerVector3> GetBottomUnitBoxes(this BoundingBox box)
        {
            var minX = (int)box.Min.X;
            var maxX = (int)Math.Ceiling(box.Max.X);
            var minY = (int)box.Min.Y;
            var maxY = (int)Math.Ceiling(box.Max.Y);
            var minZ = (int)box.Min.Z;

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    yield return new IntegerVector3(x, y, minZ);
                }
            }
        }

        public static Vector3 GetCenter(this BoundingBox box)
        {
            return (box.Max + box.Min) / 2;
        }
    }
}
