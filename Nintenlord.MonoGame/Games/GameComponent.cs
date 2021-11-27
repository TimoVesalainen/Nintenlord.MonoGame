using Microsoft.Xna.Framework;
using System;

namespace Nintenlord.MonoGame.Games
{
    public class GameComponent<TGame> : GameComponent
        where TGame : Game
    {
        public GameComponent(TGame game) : base(game)
        {
            if (game is null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            MyGame = game;
        }

        public TGame MyGame { get; }
    }
}
