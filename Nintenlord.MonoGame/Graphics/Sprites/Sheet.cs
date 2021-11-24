using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nintenlord.MonoGame.Graphics.Sprites
{
    public readonly struct Sheet
    {
        public readonly Rectangle DestinationRectangle;
        public readonly Rectangle? SourceRectangle;
        public readonly Color Color;
        public readonly float Rotation;
        public readonly Vector2 Origin;
        public readonly SpriteEffects Effects;
        public readonly float LayerDepth;

        public Sheet(Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
        {
            DestinationRectangle = destinationRectangle;
            SourceRectangle = sourceRectangle;
            Color = color;
            Rotation = rotation;
            Origin = origin;
            Effects = effects;
            LayerDepth = layerDepth;
        }
    }
}
