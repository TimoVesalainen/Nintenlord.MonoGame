using Microsoft.Xna.Framework;
using Nintenlord.Utility.Primitives;
using System;

namespace Nintenlord.MonoGame.Geometry
{
    public static class CollisionDetection
    {
        //Ray x Sphere

        //Ray x BBox

        //Line x BBox

        //Ray x Triangle

        //Plane x BBox

        //Triangle x Triangle

        public static bool ClipMovementAgainstSolid(BoundingBox original, BoundingBox futureCollidingSolid, ref Vector3 movement)
        {
            bool xIntersectPast = Intersects(original.Min.X, original.Max.X, futureCollidingSolid.Min.X, futureCollidingSolid.Max.X);
            bool yIntersectPast = Intersects(original.Min.Y, original.Max.Y, futureCollidingSolid.Min.Y, futureCollidingSolid.Max.Y);
            bool zIntersectPast = Intersects(original.Min.Z, original.Max.Z, futureCollidingSolid.Min.Z, futureCollidingSolid.Max.Z);

            bool xIntersectPresent = Intersects(original.Min.X + movement.X, original.Max.X + movement.X, futureCollidingSolid.Min.X, futureCollidingSolid.Max.X);
            bool yIntersectPresent = Intersects(original.Min.Y + movement.Y, original.Max.Y + movement.Y, futureCollidingSolid.Min.Y, futureCollidingSolid.Max.Y);
            bool zIntersectPresent = Intersects(original.Min.Z + movement.Z, original.Max.Z + movement.Z, futureCollidingSolid.Min.Z, futureCollidingSolid.Max.Z);

            bool xCausesIntersection = !xIntersectPast && xIntersectPresent;
            bool yCausesIntersection = !yIntersectPast && yIntersectPresent;
            bool zCausesIntersection = !zIntersectPast && zIntersectPresent;

            float xRatio = GetRatio(original.Min.X, original.Max.X, futureCollidingSolid.Min.X, futureCollidingSolid.Max.X, movement.X, xCausesIntersection);
            float yRatio = GetRatio(original.Min.Y, original.Max.Y, futureCollidingSolid.Min.Y, futureCollidingSolid.Max.Y, movement.Y, yCausesIntersection);
            float zRatio = GetRatio(original.Min.Z, original.Max.Z, futureCollidingSolid.Min.Z, futureCollidingSolid.Max.Z, movement.Z, zCausesIntersection);

            var ratioOfMovement = Math.Max(Math.Max(xRatio, yRatio), zRatio);

            if (ratioOfMovement >= 0)
            {
                movement *= ratioOfMovement;
            }

            return xCausesIntersection || yCausesIntersection || zCausesIntersection;
        }

        public static bool Intersects(float valueMin, float valueMax, float min, float max)
        {
            if (valueMax < valueMin)
            {
                throw new ArgumentException("valueMax < valueMin");
            }
            if (max < min)
            {
                throw new ArgumentException("max < min");
            }

            if (valueMin > min && valueMin < max)
            {
                return true;
            }
            else if (valueMax > min && valueMax < max)
            {
                return true;
            }
            else
            {
                return valueMin <= min && valueMax >= max;
            }
        }

        private static float GetRatio(float valueMin, float valueMax, float min, float max, float movement, bool causedIntersection)
        {
            if (causedIntersection)
            {
                var maxMovement = ClipLine(valueMin, valueMax, min, max, movement);
                return maxMovement / movement;
            }
            else
            {
                return float.NegativeInfinity;
            }
        }

        public static float ClipLine(float valueMin, float valueMax, float min, float max, float movement) 
        {
            if (valueMax < valueMin)
            {
                throw new ArgumentException("valueMax < valueMin");
            }
            if (max < min)
            {
                throw new ArgumentException("max < min");
            }

            if (movement < 0)
            {
                if (valueMax <= min)
                {
                    return movement;
                }
                else if (valueMin >= max)
                {
                    var maxDiff = max - valueMin;
                    return Math.Max(movement, maxDiff); //Larger value == smaller movement
                }
                else
                {
                    //valueMax > min && valueMin < max && movement < 0
                    var minDiff = min - valueMax;
                    return Math.Min(movement, minDiff);//Move atleast out of block

                }
            }
            else if (movement > 0)
            {
                if (valueMin >= max)
                {
                    return movement;
                }
                else if (valueMax <= min)
                {
                    var maxDiff = min - valueMax;
                    return Math.Min(movement, maxDiff); //Larger value == smaller movement
                }
                else
                {
                    //valueMin < max && valueMax > min && movement > 0
                    var minDiff = max - valueMin;
                    return Math.Max(movement, minDiff);//Move atleast out of block

                }
            }
            else
            {
                if (valueMax <= min || valueMin >= max)
                {
                    return 0;
                }
                else
                {
                    var valueCenter = (valueMin + valueMax) / 2;
                    var center = (min + max) / 2;

                    if (valueCenter < center)
                    {
                        return min - valueMax;
                    }
                    else
                    {
                        //Including valueCenter == center
                        return max - valueMin;
                    }
                }
            }
        }

        public static bool GetIntersectionXOffset(BoundingBox a, BoundingBox b, ref float xSpeed)
        {
            //When faces in X direction don't hit, don't edit.
            if (b.Max.Y <= a.Min.Y || b.Min.Y >= a.Max.Y)
            {
                return false;
            }

            if (b.Max.Z <= a.Min.Z || b.Min.Z >= a.Max.Z)
            {
                return false;
            }

            bool result = false;
            //If speed was positive in the start
            if (xSpeed > 0.0D && b.Max.X <= a.Min.X)
            {
                float d = a.Min.X - b.Max.X;

                if (d < xSpeed)
                {
                    result = true;
                    xSpeed = d;
                }
            }

            //If speed was negative in the start
            if (xSpeed < 0.0D && b.Min.X >= a.Max.X)
            {
                float d1 = a.Max.X - b.Min.X;

                if (d1 > xSpeed)
                {
                    result = true;
                    xSpeed = d1;
                }
            }
            return result;
        }

        public static bool GetIntersectionYOffset(BoundingBox a, BoundingBox b, ref float ySpeed)
        {
            if (b.Max.X <= a.Min.X || b.Min.X >= a.Max.X)
            {
                return false;
            }

            if (b.Max.Z <= a.Min.Z || b.Min.Z >= a.Max.Z)
            {
                return false;
            }

            bool result = false;
            if (ySpeed > 0.0D && b.Max.Y <= a.Min.Y)
            {
                float d = a.Min.Y - b.Max.Y;

                if (d < ySpeed)
                {
                    ySpeed = d;
                    result = true;
                }
            }

            if (ySpeed < 0.0D && b.Min.Y >= a.Max.Y)
            {
                float d1 = a.Max.Y - b.Min.Y;

                if (d1 > ySpeed)
                {
                    ySpeed = d1;
                    result = true;
                }
            }
            return result;
        }

        public static bool GetIntersectionZOffset(BoundingBox a, BoundingBox b, ref float zSpeed)
        {
            if (b.Max.X <= a.Min.X || b.Min.X >= a.Max.X)
            {
                return false;
            }

            if (b.Max.Y <= a.Min.Y || b.Min.Y >= a.Max.Y)
            {
                return false;
            }

            bool result = false;
            if (zSpeed > 0.0D && b.Max.Z <= a.Min.Z)
            {
                float d = a.Min.Z - b.Max.Z;

                if (d < zSpeed)
                {
                    zSpeed = d;
                    result = true;
                }
            }

            if (zSpeed < 0.0D && b.Min.Z >= a.Max.Z)
            {
                float d1 = a.Max.Z - b.Min.Z;

                if (d1 > zSpeed)
                {
                    zSpeed = d1;
                    result = true;
                }
            }
            return result;
        }
    }
}
