using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Nintenlord.MonoGame.Games
{
    public sealed class CompositeComponent<TGame> : GameComponent<TGame> where TGame : Game
    {
        private readonly Func<IEnumerable<IGameComponent>> getComponents;
        private readonly List<IGameComponent> components = new();

        public CompositeComponent(TGame game, Func<IEnumerable<IGameComponent>> getComponents) : base(game)
        {
            this.getComponents = getComponents ?? throw new ArgumentNullException(nameof(getComponents));
        }

        public override void Initialize()
        {
            base.Initialize();
            foreach (var component in getComponents())
            {
                components.Add(component);
                this.MyGame.Components.Add(component);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                foreach (var component in components)
                {
                    this.MyGame.Components.Remove(component);
                    if (component is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
            }
        }
    }
}
