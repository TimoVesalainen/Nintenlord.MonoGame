using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Geometry;
using System;

namespace Nintenlord.MonoGame.Graphics
{
    public sealed class WindowPosition
    {
        private readonly RectanglePosition mode;
        private readonly Vector2 relativePosition;

        public WindowPosition(RectanglePosition mode, Vector2 relativePosition)
        {
            this.mode = mode;
            this.relativePosition = relativePosition;
        }

        public Vector2 GetRealPosition(GameWindow window)
        {
            switch (mode)
            {
                case RectanglePosition.TopLeft:
                    return relativePosition;
                case RectanglePosition.TopCenter:
                    return relativePosition + new Vector2(window.ClientBounds.Width / 2f, 0);
                case RectanglePosition.TopRight:
                    return relativePosition + new Vector2(window.ClientBounds.Width, 0);
                case RectanglePosition.RightCenter:
                    return relativePosition + new Vector2(window.ClientBounds.Width, window.ClientBounds.Height / 2f);
                case RectanglePosition.BottomRight:
                    return relativePosition + new Vector2(window.ClientBounds.Width, window.ClientBounds.Height);
                case RectanglePosition.BottomCenter:
                    return relativePosition + new Vector2(window.ClientBounds.Width / 2f, window.ClientBounds.Height);
                case RectanglePosition.BottomLeft:
                    return relativePosition + new Vector2(0, window.ClientBounds.Height);
                case RectanglePosition.LeftCenter:
                    return relativePosition + new Vector2(0, window.ClientBounds.Height / 2f);
                case RectanglePosition.Center:
                    return relativePosition + new Vector2(window.ClientBounds.Width / 2f, window.ClientBounds.Height / 2f);
                default:
                    throw new ArgumentException();
            }
        }
    }
}