using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace Nintenlord.MonoGame.Graphics
{
    public static class GraphicsDeviceExtensions
    {
        public static void DrawBoundingBoxLines(this GraphicsDevice device, BoundingBox box, Color color)
        {
            device.DrawBoundingBoxLines(box, vertice => new VertexPositionColor(vertice, color));
        }

        public static void DrawBoundingBox(this GraphicsDevice device, BoundingBox box, Color color)
        {
            device.DrawBoundingBox(box, vertice => new VertexPositionColor(vertice, color));
        }

        public static void DrawBoundingBoxLines<T>(this GraphicsDevice device, BoundingBox box, Func<Vector3, T> createVertex)
            where T : struct, IVertexType
        {
            var verticis = box.GetCorners().Select(createVertex).ToArray();

            //TODO: Figure out and refactor
            var indicis = new int[]{
                0, 1, 1, 2, 2, 3, 3, 0,
                0, 4, 1, 5, 2, 6, 3, 7,
                4, 5, 5, 6, 6, 7, 7, 4};

            device.DrawUserIndexedPrimitives(PrimitiveType.LineList, verticis, 0, verticis.Length, indicis, 0, 12);
        }

        public static void DrawBoundingBox<T>(this GraphicsDevice device, BoundingBox box, Func<Vector3, T> createVertex)
            where T : struct, IVertexType
        {
            var verticis = box.GetCorners().Select(createVertex).ToArray();

            //TODO: Figure out and refactor
            var indicis = new int[]{
                0,4,3,7,3,4,
                1,2,5,2,6,5,
                0,1,4,1,5,4,
                2,3,7,2,7,6,
                0,3,1,1,3,2,
                4,5,7,5,6,7};

            device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, verticis, 0, verticis.Length, indicis, 0, 12);
        }
    }
}
