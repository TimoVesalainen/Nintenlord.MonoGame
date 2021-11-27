using Microsoft.Xna.Framework;
using System;

namespace Nintenlord.MonoGame.Graphics
{
    public class DrawableGameComponent<TGame> : DrawableGameComponent
        where TGame : Game
    {
        public TGame MyGame { get; }

        public DrawableGameComponent(TGame game)
            : base(game)
        {
            if (game is null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            MyGame = game;
        }
    }
}
