using Nintenlord.MonoGame.Geometry;
using System;

namespace Nintenlord.MonoGame.Map.TwoDimensions
{
    public interface ICollidable2D : IPositionable2D
    {
        RectangleF BoundingRectangle { get; }

        event EventHandler<Moved2DEventArgs> Moved;
    }
}
