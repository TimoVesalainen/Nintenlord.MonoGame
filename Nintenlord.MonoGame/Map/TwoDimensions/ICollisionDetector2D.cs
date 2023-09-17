using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Nintenlord.MonoGame.Map.TwoDimensions
{
    public interface ICollisionDetector2D : IUpdateable
    {
        IEnumerable<(ICollidable2D, ICollidable2D)> Collisions { get; }

        void Add(ICollidable2D toTrack);
        void Remove(ICollidable2D toNotTrack);
    }
}