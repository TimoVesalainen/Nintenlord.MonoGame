using Nintenlord.MonoGame.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.MonoGame.Map.TwoDimensions
{
    public interface ICollidable2D : IPositionable2D
    {
        RectangleF BoundingRectangle { get; }

        event EventHandler<Moved2DEventArgs> Moved;
    }
}
