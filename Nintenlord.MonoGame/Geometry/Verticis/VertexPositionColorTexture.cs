using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.InteropServices;

namespace Nintenlord.MonoGame.Geometry.Verticis
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexPositionColorTexture
    {
        public Vector3 Position;
        public Color Color;
        public Vector2 TextureCoord;

        public VertexPositionColorTexture(Vector3 position, Color color, Vector2 textureCoord)
        {
            Position = position;
            Color = color;
            TextureCoord = textureCoord;
        }

        public static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration(
            new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
            new VertexElement(3, VertexElementFormat.Color, VertexElementUsage.Color, 1),
            new VertexElement(7, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 2));

        //new VertexElement(0, 3, VertexAttribPointerType.Float, false, Marshal.SizeOf(typeof(VertexPositionColorTexture)), 0),
        //new VertexElement(1, 4, VertexAttribPointerType.Byte, false, Marshal.SizeOf(typeof(VertexPositionColorTexture)), Vector3.SizeInBytes),
        //new VertexElement(2, 2, VertexAttribPointerType.Float, false, Marshal.SizeOf(typeof(VertexPositionColorTexture)), Vector3.SizeInBytes + 4));
    }
}
