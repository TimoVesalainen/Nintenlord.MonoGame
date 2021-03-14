using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Games;

namespace Nintenlord.MonoGame.Graphics.Renderer
{
    public abstract class RendererGameComponent<TGame, T> : GameComponent<TGame>, IRenderer<T> where TGame : Game
    {
        protected RendererGameComponent(TGame game)
            : base(game) { }

        public abstract void Render(T item, GameTime gameTime);

    }
}