using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Nintenlord.MonoGame.Graphics.Textures
{
    public static class TextureExtensions
    {
        public static IEnumerable<VertexPositionColor> GetVerticis(this Texture2D texture)
        {
            var pixels = texture.GetPixels<Color>();

            IEnumerable<VertexPositionColor> GetPixels()
            {
                for (int y = 0; y < texture.Height; y++)
                {
                    for (int x = 0; x < texture.Width; x++)
                    {
                        yield return new VertexPositionColor(new Vector3(x, y, 0), pixels[x + y * texture.Width]);
                    }
                }
            }

            return GetPixels();
        }

        public static T[] GetPixels<T>(this Texture2D texture) where T : struct
        {
            var pixels = new T[texture.Width * texture.Height];

            texture.GetData(pixels);

            return pixels;
        }

        public static void SetPixel<T>(this Texture2D texture, T pixel, int x, int y) where T : struct
        {
            texture.SetData(0, new Rectangle(x, y, 1, 1), new[] { pixel }, 0, 1);
        }
    }
}
