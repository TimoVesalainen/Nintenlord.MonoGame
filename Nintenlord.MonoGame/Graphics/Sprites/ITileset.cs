using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.MonoGame.Graphics.Sprites
{
    /// <summary>
    ///     Texture sheet containing fixed size tiles.
    /// </summary>
    public interface ITileset<T> : ITextureSheet<T>
    {
        int TileWidth { get; }
        int TileHeight { get; }
    }

    public static class TilesetHelpers
    {
        public static Vector2 TileSizeV<T>(this ITileset<T> tileset)
        {
            return new Vector2(tileset.TileWidth, tileset.TileHeight);
        }
        public static Point TileSize<T>(this ITileset<T> tileset)
        {
            return new Point(tileset.TileWidth, tileset.TileHeight);
        }

        public static ITileset<T> CastToTileset<T>(this ITextureSheet<T> tileset)
        {
            if (tileset is ITileset<T> actualTileset)
            {
                return actualTileset;
            }
            else
            {
                return new CastTileset<T>(tileset);
            }
        }

        private class CastTileset<T> : ITileset<T>
        {
            readonly ITextureSheet<T> sheet;

            public CastTileset(ITextureSheet<T> sheet)
            {
                this.sheet = sheet;
            }

            public Rectangle this[T index] => sheet[index];

            public int TileWidth => sheet[sheet.GetIndicis().First()].Width;

            public int TileHeight => sheet[sheet.GetIndicis().First()].Height;

            public Texture2D Sheet => sheet.Sheet;

            public IEnumerable<T> GetIndicis()
            {
                return sheet.GetIndicis();
            }

            public bool IsValidIndex(T index)
            {
                return sheet.IsValidIndex(index);
            }
        }
    }
}