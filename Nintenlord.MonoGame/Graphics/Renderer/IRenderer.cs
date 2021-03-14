using Microsoft.Xna.Framework;
using System;

namespace Nintenlord.MonoGame.Graphics.Renderer
{
    public interface IRenderer<in T>
    {
        void Render(T itemToRender, GameTime gameTime);
    }

    public static class RendererHelpers
    {
        public static bool TryRender<T>(this object obj, IRenderer<T> renderer, GameTime gameTime)
            where T : class
        {
            if (obj is T renderable)
            {
                renderer.Render(renderable, gameTime);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void RenderObject<T>(this T obj, IRenderer<T> renderer, GameTime gameTime)
        {
            renderer.Render(obj, gameTime);
        }

        public static IRenderer<TOut> Convert<TIn, TOut>(this IRenderer<TIn> renderer, Func<TOut, TIn> conversion)
        {
            return new ConvertedRenderer<TIn, TOut>(renderer, conversion);
        }

        private class ConvertedRenderer<TIn, TOut> : IRenderer<TOut>
        {
            private readonly Func<TOut, TIn> conversion;
            private readonly IRenderer<TIn> renderer;

            public ConvertedRenderer(IRenderer<TIn> renderer, Func<TOut, TIn> conversion)
            {
                this.renderer = renderer;
                this.conversion = conversion;
            }

            #region IRenderer<Tout> Members

            public void Render(TOut itemToRender, GameTime gameTime)
            {
                renderer.Render(conversion(itemToRender), gameTime);
            }

            #endregion
        }
    }
}