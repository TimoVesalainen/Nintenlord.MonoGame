﻿using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Geometry.Vectors;
using System;
using System.Collections.Generic;

namespace Nintenlord.MonoGame.Geometry
{
    static public class BoundingBoxHelpers
    {
        public static IEnumerable<IntegerVector3> GetIntersectingUnitBoxes(this BoundingBox box)
        {
            var minX = (int)Math.Floor(box.Min.X);
            var maxX = (int)Math.Ceiling(box.Max.X);
            var minY = (int)Math.Floor(box.Min.Y);
            var maxY = (int)Math.Ceiling(box.Max.Y);
            var minZ = (int)Math.Floor(box.Min.Z);
            var maxZ = (int)Math.Ceiling(box.Max.Z);

            for (int z = minZ; z < maxZ; z++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    for (int x = minX; x < maxX; x++)
                    {
                        yield return new IntegerVector3(x, y, z);
                    }
                }
            }
        }

        public static IEnumerable<IntegerVector3> GetBottomUnitBoxes(this BoundingBox box)
        {
            var minX = (int)Math.Floor(box.Min.X);
            var maxX = (int)Math.Ceiling(box.Max.X);
            var minY = (int)Math.Floor(box.Min.Y);
            var maxY = (int)Math.Ceiling(box.Max.Y);
            var minZ = (int)Math.Floor(box.Min.Z);

            for (int y = minY; y < maxY; y++)
            {
                for (int x = minX; x < maxX; x++)
                {
                    yield return new IntegerVector3(x, y, minZ);
                }
            }
        }

        public static Vector3 GetCenter(this BoundingBox box)
        {
            return (box.Max + box.Min) / 2;
        }

        public static BoundingBox Translate(this BoundingBox box, Vector3 translate)
        {
            BoundingBox newBox = new BoundingBox(box.Min + translate, box.Max + translate);
            return newBox;
        }
    }
}
