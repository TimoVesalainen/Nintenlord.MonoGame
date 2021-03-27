using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.MonoGame.Geometry
{
    /// <summary>
    ///     In your face!
    /// </summary>
    public static class BlockFaceHelpers
    {
        /// <summary>
        ///     Block is from -cubeSize / 2 to cubeSize / 2.
        /// </summary>
        /// <param name="posRelativeToCube">Position relative to the center of the block</param>
        /// <param name="cubeSize">Ray of cube</param>
        /// <returns></returns>
        public static BlockFace GetVisibleFaces(Vector3 posRelativeToCube, Vector3 cubeSize)
        {
            cubeSize /= 2;
            var result = BlockFace.None;

            if (posRelativeToCube.X < -cubeSize.X)
            {
                result |= BlockFace.West;
            }
            else if (posRelativeToCube.X > cubeSize.X)
            {
                result |= BlockFace.East;
            }

            if (posRelativeToCube.Y < -cubeSize.Y)
            {
                result |= BlockFace.South;
            }
            else if (posRelativeToCube.Y > cubeSize.Y)
            {
                result |= BlockFace.North;
            }

            if (posRelativeToCube.Z < -cubeSize.Z)
            {
                result |= BlockFace.Down;
            }
            else if (posRelativeToCube.Z > cubeSize.Z)
            {
                result |= BlockFace.Up;
            }

            return result;
        }

        public static BlockFace GetVisibleFaces(IntegerVector3 posRelativeToCube, IntegerVector3 cubeSize)
        {
            cubeSize /= 2;
            var result = BlockFace.None;

            if (posRelativeToCube.X < -cubeSize.X)
            {
                result |= BlockFace.West;
            }
            else if (posRelativeToCube.X > cubeSize.X)
            {
                result |= BlockFace.East;
            }

            if (posRelativeToCube.Y < -cubeSize.Y)
            {
                result |= BlockFace.South;
            }
            else if (posRelativeToCube.Y > cubeSize.Y)
            {
                result |= BlockFace.North;
            }

            if (posRelativeToCube.Z < -cubeSize.Z)
            {
                result |= BlockFace.Down;
            }
            else if (posRelativeToCube.Z > cubeSize.Z)
            {
                result |= BlockFace.Up;
            }

            return result;
        }

        public static IntegerVector3 MoveTo(this IntegerVector3 vec, BlockFace faces)
        {
            int x = vec.X;
            int y = vec.Y;
            int z = vec.Z;

            if (faces.HasFlag(BlockFace.East))
            {
                x++;
            }
            if (faces.HasFlag(BlockFace.West))
            {
                x--;
            }
            if (faces.HasFlag(BlockFace.North))
            {
                y++;
            }
            if (faces.HasFlag(BlockFace.South))
            {
                y--;
            }
            if (faces.HasFlag(BlockFace.Up))
            {
                z++;
            }
            if (faces.HasFlag(BlockFace.Down))
            {
                z--;
            }
            return new IntegerVector3(x, y, z);
        }

        public static Vector3 MoveTo(Vector3 vec, BlockFace faces, float moveAMount = 1)
        {
            float x = vec.X;
            float y = vec.Y;
            float z = vec.Z;

            if (faces.HasFlag(BlockFace.East))
            {
                x += moveAMount;
            }
            if (faces.HasFlag(BlockFace.West))
            {
                x -= moveAMount;
            }
            if (faces.HasFlag(BlockFace.North))
            {
                y += moveAMount;
            }
            if (faces.HasFlag(BlockFace.South))
            {
                y -= moveAMount;
            }
            if (faces.HasFlag(BlockFace.Up))
            {
                z += moveAMount;
            }
            if (faces.HasFlag(BlockFace.Down))
            {
                z -= moveAMount;
            }
            return new Vector3(x, y, z);
        }

        public static IEnumerable<BlockFace> Filter(this IEnumerable<BlockFace> values, BlockFace filter)
        {
            return values.Select(x => x & filter);
        }

        public static BlockFace GetOpposite(this BlockFace faces)
        {
            return BlockFace.All ^ faces;
        }

        /// <remarks>box.Contains(vec) == ContainmentType.Intersects or Contains </remarks>
        public static BlockFace GetFace(this BoundingBox box, Vector3 vec, Vector3 eps)
        {
            var result = BlockFace.None;

            if (Math.Abs(box.Min.X - vec.X) < eps.X)
            {
                result |= BlockFace.West;
            }
            if (Math.Abs(box.Max.X - vec.X) < eps.X)
            {
                result |= BlockFace.East;
            }

            if (Math.Abs(box.Min.Y - vec.Y) < eps.Y)
            {
                result |= BlockFace.South;
            }
            if (Math.Abs(box.Max.Y - vec.Y) < eps.Y)
            {
                result |= BlockFace.North;
            }

            if (Math.Abs(box.Min.Z - vec.Z) < eps.Z)
            {
                result |= BlockFace.Down;
            }
            if (Math.Abs(box.Max.Z - vec.Z) < eps.Z)
            {
                result |= BlockFace.Up;
            }

            return result;
        }

        public static IEnumerable<Plane> GetFacePlanes(this BoundingBox box, BlockFace faces)
        {
            return box.GetFaceRectangles(faces).Select(vectors => new Plane(vectors[0], vectors[1], vectors[2]));
        }

        public static IEnumerable<Vector3[]> GetFaceRectangles(this BoundingBox box, BlockFace faces)
        {
            if (faces.HasFlag(BlockFace.Down))
            {
                Vector3 corner1 = box.Min;
                var corner2 = new Vector3(box.Min.X, box.Max.Y, box.Min.Z);
                var corner3 = new Vector3(box.Max.X, box.Max.Y, box.Min.Z);
                var corner4 = new Vector3(box.Max.X, box.Min.Y, box.Min.Z);
                yield return new[] { corner1, corner2, corner3, corner4 };
            }

            if (faces.HasFlag(BlockFace.Up))
            {
                Vector3 corner1 = box.Max;
                var corner2 = new Vector3(box.Min.X, box.Max.Y, box.Max.Z);
                var corner3 = new Vector3(box.Min.X, box.Min.Y, box.Max.Z);
                var corner4 = new Vector3(box.Max.X, box.Min.Y, box.Max.Z);
                yield return new[] { corner1, corner2, corner3, corner4 };
            }

            if (faces.HasFlag(BlockFace.East))
            {
                Vector3 corner1 = box.Max;
                var corner2 = new Vector3(box.Max.X, box.Min.Y, box.Max.Z);
                var corner3 = new Vector3(box.Max.X, box.Min.Y, box.Min.Z);
                var corner4 = new Vector3(box.Max.X, box.Max.Y, box.Min.Z);
                yield return new [] { corner1, corner2, corner3, corner4 };
            }

            if (faces.HasFlag(BlockFace.West))
            {
                Vector3 corner1 = box.Min;
                var corner2 = new Vector3(box.Min.X, box.Min.Y, box.Max.Z);
                var corner3 = new Vector3(box.Min.X, box.Max.Y, box.Max.Z);
                var corner4 = new Vector3(box.Min.X, box.Max.Y, box.Min.Z);
                yield return new [] { corner1, corner2, corner3, corner4 };
            }

            if (faces.HasFlag(BlockFace.North))
            {
                Vector3 corner1 = box.Max;
                var corner2 = new Vector3(box.Max.X, box.Max.Y, box.Min.Z);
                var corner3 = new Vector3(box.Min.X, box.Max.Y, box.Min.Z);
                var corner4 = new Vector3(box.Min.X, box.Max.Y, box.Max.Z);
                yield return new [] { corner1, corner2, corner3, corner4 };
            }

            if (faces.HasFlag(BlockFace.South))
            {
                Vector3 corner1 = box.Min;
                var corner2 = new Vector3(box.Max.X, box.Min.Y, box.Min.Z);
                var corner3 = new Vector3(box.Max.X, box.Min.Y, box.Max.Z);
                var corner4 = new Vector3(box.Min.X, box.Min.Y, box.Max.Z);
                yield return new[] { corner1, corner2, corner3, corner4 };
            }
        }

        public static IEnumerable<(T, BlockFace)> GetVisibleFaces<T>(this T[,,] blocks, Func<T, bool> isSolid)
        {
            for (int z = 0; z < blocks.GetLength(2); z++)
            {
                bool isFirstZ = z == 0;
                bool isLastZ = z == blocks.GetLength(2) - 1;
                for (int y = 0; y < blocks.GetLength(1); y++)
                {
                    bool isFirstY = y == 0;
                    bool isLastY = y == blocks.GetLength(1) - 1;
                    for (int x = 0; x < blocks.GetLength(0); x++)
                    {
                        bool isFirstX = x == 0;
                        bool isLastX = x == blocks.GetLength(0) - 1;

                        var item = blocks[x, y, z];     
                        if (isSolid(item))
                        {
                            BlockFace visibleFaces = BlockFace.None;

                            void CheckBlock(bool isInvalidBlock, BlockFace face, int newX, int newY, int newZ)
                            {
                                if (isInvalidBlock)
                                {
                                    visibleFaces |= face;
                                }
                                else
                                {
                                    var below = blocks[newX, newY, newZ];
                                    if (!isSolid(below))
                                    {
                                        visibleFaces |= face;
                                    }
                                }
                            }

                            CheckBlock(isFirstZ, BlockFace.Down, x, y, z - 1);
                            CheckBlock(isLastZ, BlockFace.Up, x, y, z + 1);
                            CheckBlock(isFirstY, BlockFace.South, x, y - 1, z);
                            CheckBlock(isLastY, BlockFace.North, x, y + 1, z);
                            CheckBlock(isFirstX, BlockFace.East, x - 1, y, z);
                            CheckBlock(isLastX, BlockFace.West, x + 1, y, z);

                            yield return (item, visibleFaces);
                        }
                    }
                }
            }
        }

        public static IEnumerable<(T, BlockFace)> GetVisibleFaces<T>(this T[,] blocks, Func<T, bool> isSolid)
        {
            for (int y = 0; y < blocks.GetLength(1); y++)
            {
                bool isFirstY = y == 0;
                bool isLastY = y == blocks.GetLength(1) - 1;
                for (int x = 0; x < blocks.GetLength(0); x++)
                {
                    bool isFirstX = x == 0;
                    bool isLastX = x == blocks.GetLength(0) - 1;

                    var item = blocks[x, y];
                    if (isSolid(item))
                    {
                        BlockFace visibleFaces = BlockFace.Up | BlockFace.Down;

                        void CheckBlock(bool isInvalidBlock, BlockFace face, int newX, int newY)
                        {
                            if (isInvalidBlock)
                            {
                                visibleFaces |= face;
                            }
                            else
                            {
                                var below = blocks[newX, newY];
                                if (!isSolid(below))
                                {
                                    visibleFaces |= face;
                                }
                            }
                        }
                        CheckBlock(isFirstY, BlockFace.South, x, y - 1);
                        CheckBlock(isLastY, BlockFace.North, x, y + 1);
                        CheckBlock(isFirstX, BlockFace.East, x - 1, y);
                        CheckBlock(isLastX, BlockFace.West, x + 1, y);

                        yield return (item, visibleFaces);
                    }
                }
            }
        }

        public static IEnumerable<Vector3> GetFaceTriangleList(this BoundingBox box, BlockFace faces)
        {
            var corners = box.GetCorners();

            foreach (var index in GetFaceTriangleListIndicis(faces))
            {
                yield return corners[index];
            }
        }

        public static IEnumerable<int> GetFaceTriangleListIndicis(this BlockFace faces)
        {
            /*
             * Depends on BoundingBox.GetCorners order:
            return new Vector3[] {
                new Vector3(this.Min.X, this.Max.Y, this.Max.Z), 
                new Vector3(this.Max.X, this.Max.Y, this.Max.Z),
                new Vector3(this.Max.X, this.Min.Y, this.Max.Z), 
                new Vector3(this.Min.X, this.Min.Y, this.Max.Z), 
                new Vector3(this.Min.X, this.Max.Y, this.Min.Z),
                new Vector3(this.Max.X, this.Max.Y, this.Min.Z),
                new Vector3(this.Max.X, this.Min.Y, this.Min.Z),
                new Vector3(this.Min.X, this.Min.Y, this.Min.Z)
            };
            */

            if (faces.HasFlag(BlockFace.East))
            {
                yield return 0;
                yield return 4;
                yield return 3;

                yield return 7;
                yield return 3;
                yield return 4;
            }
            if (faces.HasFlag(BlockFace.West))
            {
                yield return 1;
                yield return 2;
                yield return 5;

                yield return 2;
                yield return 6;
                yield return 5;
            }
            if (faces.HasFlag(BlockFace.North))
            {
                yield return 0;
                yield return 1;
                yield return 4;

                yield return 1;
                yield return 5;
                yield return 4;
            }
            if (faces.HasFlag(BlockFace.South))
            {
                yield return 2;
                yield return 3;
                yield return 7;

                yield return 2;
                yield return 7;
                yield return 6;
            }
            if (faces.HasFlag(BlockFace.Up))
            {
                yield return 0;
                yield return 3;
                yield return 1;

                yield return 1;
                yield return 3;
                yield return 2;
            }
            if (faces.HasFlag(BlockFace.Down))
            {
                yield return 4;
                yield return 5;
                yield return 7;

                yield return 5;
                yield return 6;
                yield return 7;
            }
        }
    }
}