using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nintenlord.MonoGame.Geometry;
using Nintenlord.MonoGame.Graphics;
using Nintenlord.MonoGame.Graphics.Sprites;
using Nintenlord.StateMachines;
using System;
using System.Collections.Generic;

namespace Nintenlord.MonoGame.Map.TwoDimensions
{
    public abstract class Entity2D<TGame, TState, TCommand, TEnumerable> : DrawableGameComponent<TGame>, ICollidable2D
        where TGame : Game
        where TEnumerable : IEnumerable<(int textureIndex, Sheet sheet)>
    {
        readonly IStateMachine<TState, TCommand> stateMachine;
        private readonly string[] texturesNames;
        private readonly Texture2D[] textures;
        readonly List<TCommand> gottenCommands = new();

        TState state;

        abstract public RectangleF BoundingRectangle { get; }

        abstract public Vector2 Position { get; }

        protected abstract SpriteBatch SpriteBatch { get; }

        public event EventHandler<Moved2DEventArgs> Moved;

        public TState State => state;

        protected Entity2D(TGame game, IStateMachine<TState, TCommand> stateMachine, string[] textures) : base(game)
        {
            if (game is null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            if (textures is null)
            {
                throw new ArgumentNullException(nameof(textures));
            }

            if (textures.Length == 0)
            {
                throw new ArgumentException(nameof(textures), "Empty array of textures");
            }

            this.stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
            this.texturesNames = textures;
            this.textures = new Texture2D[textures.Length];
            state = stateMachine.StartState;
        }

        protected override void LoadContent()
        {
            for (int i = 0; i < textures.Length; i++)
            {
                textures[i] = MyGame.Content.Load<Texture2D>(texturesNames[i]);
            }
            base.LoadContent();
        }

        public void SendCommand(TCommand command)
        {
            gottenCommands.Add(command);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var command in gottenCommands)
            {
                this.state = stateMachine.Transition(this.state, command);
                if (stateMachine.IsFinalState(this.state))
                {
                    this.Enabled = false;
                    return;
                }
            }
            gottenCommands.Clear();
            base.Update(gameTime);
        }

        protected abstract TEnumerable GetSheets();

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            SpriteBatch.Begin();
            foreach (var (texture, sheet) in GetSheets())
            {
                SpriteBatch.Draw(textures[texture], sheet);
            }
            SpriteBatch.End();
        }
    }
}
