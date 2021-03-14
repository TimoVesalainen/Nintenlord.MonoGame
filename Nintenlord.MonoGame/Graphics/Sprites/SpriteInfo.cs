using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nintenlord.MonoGame.Graphics.Sprites
{
    public struct SpriteInfo
    {
        public Texture Texture;
        public Rectangle Source;
        public Vector2 Dest;
        public Vector2 Scale;
        public Vector2 Origin;
        public SpriteEffect Effect;
        public float Rotation;
        public float Depth;
        public Color Color;
    }
}
