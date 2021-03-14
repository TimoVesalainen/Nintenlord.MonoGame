using Microsoft.Xna.Framework;

namespace Nintenlord.MonoGame.Games
{
    public class GameComponent<TGame> : GameComponent
        where TGame : Game
    {
        public GameComponent(TGame game) : base(game)
        {
            MyGame = game;
        }

        public TGame MyGame { get; }
    }
}
