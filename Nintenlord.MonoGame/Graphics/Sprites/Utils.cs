using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Nintenlord.MonoGame.Content;
using System;
using System.Linq;

namespace Nintenlord.MonoGame.Graphics.Sprites
{
    public static class Utils
    {
        public static DictionarySpriteSheet<T> ToDictionarySpriteSheet<T>(this Sheets sheets, ContentManager content, Func<string, T> sheetNameParser)
        {
            var texture = content.Load<Texture2D>(sheets.Bitmap);

            var dict = sheets.SheetList.ToDictionary(
                sheet => sheetNameParser(sheet.Name),
                sheet => new Rectangle(sheet.X, sheet.Y, sheet.Width, sheet.Height));

            return new DictionarySpriteSheet<T>(dict, texture);
        }

        public static DictionarySpriteSheet<T> LoadDictionarySpriteSheet<T>(this ContentManager content, string assetName, Func<string, T> sheetNameParser)
        {
            var sheets = content.Load<Sheets>(assetName);

            return sheets.ToDictionarySpriteSheet(content, sheetNameParser);
        }

        public static BasicTileset ToBasicTileSet(this TileSheets sheets, ContentManager content)
        {
            var texture = content.Load<Texture2D>(sheets.Bitmap);

            return BasicTileset.FromTileSize(texture, sheets.TileWidth, sheets.TileHeight);
        }

        public static BasicTileset LoadBasicTileSet(this ContentManager content, string assetName)
        {
            var sheet = content.Load<TileSheets>(assetName);

            return sheet.ToBasicTileSet(content);
        }

        public static void Draw(this SpriteBatch batch, Texture2D texture, Sheet sheet)
        {
            batch.Draw(texture, sheet.DestinationRectangle, sheet.SourceRectangle, sheet.Color, sheet.Rotation, sheet.Origin, sheet.Effects, sheet.LayerDepth);
        }
    }
}
