using Microsoft.Xna.Framework;
using System;

namespace Nintenlord.MonoGame.Map.TwoDimensions
{
    public sealed class Moved2DEventArgs : EventArgs
    {
        public Vector2 OldPosition { get; }
        public Vector2 NewPosition { get; }

        public Moved2DEventArgs(Vector2 oldPosition, Vector2 newPosition)
        {
            OldPosition = oldPosition;
            NewPosition = newPosition;
        }
    }
}
