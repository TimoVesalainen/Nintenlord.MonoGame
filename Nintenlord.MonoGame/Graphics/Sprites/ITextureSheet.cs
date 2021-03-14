using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Nintenlord.MonoGame.Graphics.Sprites
{
    public interface ITextureSheet<T>
    {
        Rectangle this[T index] { get; }

        Texture2D Sheet { get; }

        bool IsValidIndex(T index);

        IEnumerable<T> GetIndicis();
    }

    public static class TextureSheetExtensions
    {
        public static void Draw<T>(this SpriteBatch batch, ITextureSheet<T> sheet, IEnumerable<KeyValuePair<T, Vector2>> items, Color color)
        {
            var texture = sheet.Sheet;

            foreach (var item in items)
            {
                batch.Draw(texture, item.Value, sheet[item.Key], color);
            }
        }
        public static void Draw<T>(this SpriteBatch batch, ITextureSheet<T> sheet, T item, Vector2 position, Color color)
        {
            batch.Draw(sheet.Sheet, position, sheet[item], color);
        }
    }
}