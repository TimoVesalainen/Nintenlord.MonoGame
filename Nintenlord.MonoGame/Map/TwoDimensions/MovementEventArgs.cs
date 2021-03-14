using Microsoft.Xna.Framework;
using System;

namespace Nintenlord.MonoGame.Map.TwoDimensions
{
    public class MovementEventArgs : EventArgs
    {
        public MovementEventArgs(
            IPositionable2D moved,
            Vector2 oldPosition,
            Vector2 newPosition)
        {
            MovedUnit = moved;
            OldPosition = oldPosition;
            NewPosition = newPosition;
        }

        public IPositionable2D MovedUnit { get; private set; }
        public Vector2 OldPosition { get; private set; }
        public Vector2 NewPosition { get; private set; }
    }
}