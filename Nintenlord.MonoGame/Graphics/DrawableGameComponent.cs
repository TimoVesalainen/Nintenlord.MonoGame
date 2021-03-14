using Microsoft.Xna.Framework;

namespace Nintenlord.MonoGame.Graphics
{
    public class DrawableGameComponent<TGame> : DrawableGameComponent
        where TGame : Game
    {
        public TGame MyGame { get; }

        public DrawableGameComponent(TGame game)
            : base(game)
        {
            MyGame = game;
        }
    }
}
