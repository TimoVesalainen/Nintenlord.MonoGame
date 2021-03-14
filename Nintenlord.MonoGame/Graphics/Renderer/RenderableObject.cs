using Microsoft.Xna.Framework;

namespace Nintenlord.MonoGame.Graphics.Renderer
{
    public sealed class RenderableObject<T> : AbstractDrawable
    {
        private readonly T itemToRender;
        private readonly IRenderer<T> renderer;

        public RenderableObject(T itemToRender, IRenderer<T> renderer)
        {
            this.itemToRender = itemToRender;
            this.renderer = renderer;
        }

        public override void Draw(GameTime gameTime)
        {
            renderer.Render(itemToRender, gameTime);
        }
    }
}