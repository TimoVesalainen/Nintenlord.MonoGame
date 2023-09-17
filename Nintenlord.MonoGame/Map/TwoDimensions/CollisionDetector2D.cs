using Microsoft.Xna.Framework;
using Nintenlord.Collections.Comparers;
using Nintenlord.Collections.Lists;
using Nintenlord.MonoGame.Games;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.MonoGame.Map.TwoDimensions
{
    public sealed class CollisionDetector2D<TGame> : GameComponent<TGame>, ICollisionDetector2D where TGame : Game
    {
        readonly List<ICollidable2D> xCoordSorted = new();
        readonly IComparer<ICollidable2D> xCoordComparer;

        readonly HashSet<(ICollidable2D, ICollidable2D)> collisions = new();

        public IEnumerable<(ICollidable2D, ICollidable2D)> Collisions => collisions;

        public CollisionDetector2D(TGame game) : base(game)
        {
            xCoordComparer = Comparer<float>.Default.Select((ICollidable2D x) => x.Position.X);
        }

        public void Add(ICollidable2D toTrack)
        {
            xCoordSorted.SortedInsert(toTrack, xCoordComparer);
            //toTrack.Moved += ToTrack_Moved;
        }

        private void ToTrack_Moved(object sender, Moved2DEventArgs e)
        {
            //TODO: Adjust single item position instead of sorting every frame
        }

        public void Remove(ICollidable2D toNotTrack)
        {
            xCoordSorted.SortedDelete(toNotTrack, xCoordComparer);
            //toNotTrack.Moved -= ToTrack_Moved;
        }

        public override void Update(GameTime gameTime)
        {
            collisions.Clear();

            xCoordSorted.Sort(xCoordComparer);

            for (int i = 0; i < xCoordSorted.Count; i++)
            {
                var left = xCoordSorted[i];
                int j = i + 1;
                while (j < xCoordSorted.Count)
                {
                    var right = xCoordSorted[j];
                    if (left.BoundingRectangle.Right < right.BoundingRectangle.Left)
                    {
                        break;
                    }
                    else if (left.BoundingRectangle.IntersectsWith(right.BoundingRectangle))
                    {
                        collisions.Add((left, right));
                    }
                }
            }

            base.Update(gameTime);
        }
    }
}
