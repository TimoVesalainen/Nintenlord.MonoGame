using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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

        public static void SaveMipmapLevels(this Texture2D texture, GraphicsDevice device, Func<int, string> filenameGenerator = null)
        {
            filenameGenerator = filenameGenerator ?? (i => $"level_{i}.png");

            var width = texture.Width;
            var height = texture.Height;

            var array = new Color[width * height];
            for (int i = 0; i < texture.LevelCount; i++)
            {
                var smallerTexture = new Texture2D(device, width, height);

                texture.GetData(i, null, array, 0, width * height);

                smallerTexture.SetData(array, 0, width * height);

                smallerTexture.SaveAsPng(System.IO.File.OpenWrite(filenameGenerator(i)), width, height);
                width /= 2;
                height /= 2;
            }
        }

        /// <summary>
        /// Prevents color bleeding, unlike default mipmap generation
        /// </summary>
        public static void CreateMipmaps(this Texture2D texture, int maxLevel)
        {
            var width = texture.Width;
            var height = texture.Height;

            var originalArray = new Color[width * height];
            texture.GetData(originalArray);

            Rectangle rect = Rectangle.Empty;

            for (int i = 1; i < maxLevel; i++)
            {
                if (width > 1)
                    width /= 2;
                if (height > 1)
                    height /= 2;

                var array2 = new Color[width * height];

                rect.Width = texture.Width / width;
                rect.Height = texture.Height / height;
                int rectPixelCount = rect.Width * rect.Height;

                for (int y = 0; y < height; y++)
                {
                    var rowStart = width * y;
                    for (int x = 0; x < width; x++)
                    {
                        int index = rowStart + x;

                        rect.X = x * rect.Width;
                        rect.Y = y * rect.Height;

                        int red = 0;
                        int green = 0;
                        int blue = 0;
                        int alpha = 0;
                        foreach (var pixel in IterateRect(originalArray, texture.Width, rect))
                        {
                            red += pixel.R;
                            green += pixel.G;
                            blue += pixel.B;
                            alpha += pixel.A;
                        }
                        array2[index] = new Color(
                            red / rectPixelCount,
                            green / rectPixelCount,
                            blue / rectPixelCount,
                            alpha / rectPixelCount);
                    }
                }

                texture.SetData(i, null, array2, 0, array2.Length);
            }
        }

        public static IEnumerable<T> IterateRect<T>(T[] pixels, int width, Rectangle rect)
        {
            for (int y = rect.Top; y < rect.Bottom; y++)
            {
                int rowStart = width * y;
                for (int x = rect.Left; x < rect.Right; x++)
                {
                    int index = rowStart + x;

                    yield return pixels[index];
                }
            }
        }
    }
}
