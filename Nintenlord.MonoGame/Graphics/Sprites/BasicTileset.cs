using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.MonoGame.Graphics.Sprites
{
    public sealed class BasicTileset : ITileset<int>
    {
        public int AmountHeight { get; }
        public int AmountWidth { get; }

        public int TileWidth { get; }
        public int TileHeight { get; }

        public BasicTileset(Texture2D sheet, int amountWidth, int amountHeight, int tileWidth, int tileHeight)
        {
            Sheet = sheet;
            AmountWidth = amountWidth;
            AmountHeight = amountHeight;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
        }

        public Rectangle this[int index]
        {
            get
            {
                int x = index % AmountWidth;
                int y = index / AmountWidth;
                return new Rectangle(x * TileWidth, y * TileHeight, TileWidth, TileHeight);
            }
        }

        public Texture2D Sheet { get; }

        public bool IsValidIndex(int index)
        {
            return index >= 0 && index < AmountWidth * AmountHeight;
        }

        public IEnumerable<int> GetIndicis()
        {
            return Enumerable.Range(0, AmountWidth * AmountHeight);
        }

        static public BasicTileset FromTileSize(Texture2D texture, int tileWidth, int tileHeight)
        {
            var tilesHorizontal = texture.Height / tileHeight;
            var tilesVertical = texture.Width / tileWidth;

            return new BasicTileset(texture, tilesHorizontal, tilesVertical, tileWidth, tileHeight);
        }

        static public BasicTileset FromTileAmount(Texture2D texture, int tilesHorizontal, int tilesVertical)
        {
            var tileHeight = texture.Width / tilesHorizontal;
            var tileWidth = texture.Height / tilesVertical;

            return new BasicTileset(texture, tilesHorizontal, tilesVertical, tileWidth, tileHeight);
        }
    }
}