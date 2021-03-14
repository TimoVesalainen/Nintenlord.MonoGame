using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Nintenlord.MonoGame.Graphics.Sprites
{
    public sealed class DictionarySpriteSheet<T> : ITextureSheet<T>
    {
        private readonly IDictionary<T, Rectangle> rectangles;

        public DictionarySpriteSheet(IDictionary<T, Rectangle> rectangles, Texture2D sheet)
        {
            Sheet = sheet;
            this.rectangles = rectangles;
        }

        public Rectangle this[T index] => rectangles[index];

        public Texture2D Sheet
        {
            get;
        }

        public bool IsValidIndex(T index)
        {
            return rectangles.Keys.Contains(index);
        }

        public IEnumerable<T> GetIndicis()
        {
            return rectangles.Keys;
        }

        /// <summary>
        /// DictionarySpriteSheet is universal.
        /// </summary>
        public static DictionarySpriteSheet<T> GetFromTextureSheet(ITextureSheet<T> sheet)
        {
            Dictionary<T, Rectangle> sheets = new Dictionary<T, Rectangle>();

            foreach (var sheetItem in sheet.GetIndicis())
            {
                sheets[sheetItem] = sheet[sheetItem];
            }

            return new DictionarySpriteSheet<T>(sheets, sheet.Sheet);
        }
    }
}
