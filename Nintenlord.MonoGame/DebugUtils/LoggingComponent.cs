using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nintenlord.MonoGame.Graphics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.MonoGame.DebugUtils
{
    public sealed class LoggingComponent<TGame> : DrawableGameComponent<TGame>, ILoggerProvider where TGame : Game
    {
        readonly string fontName;
        readonly int lineCount;
        readonly Logger logger;
        readonly Vector2 position;
        readonly Color textColor;
        readonly StringBuilder builder = new();

        SpriteBatch batch;
        SpriteFont font;

        public LoggingComponent(TGame game, string fontName, Color textColor, Vector2 position = default, int lineCount = 10) : base(game)
        {
            if (string.IsNullOrEmpty(fontName))
            {
                throw new ArgumentException($"'{nameof(fontName)}' cannot be null or empty.", nameof(fontName));
            }
            if (lineCount <= 0)
            {
                throw new ArgumentException($"'{nameof(lineCount)}' has to be positive.", nameof(lineCount));
            }

            this.fontName = fontName;
            this.lineCount = lineCount;
            this.logger = new Logger(lineCount);
            this.position = position;
            this.textColor = textColor;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            batch = new SpriteBatch(Game.GraphicsDevice);
            font = MyGame.Content.Load<SpriteFont>(fontName);
        }

        public ILogger CreateLogger(string categoryName)
        {
            return logger;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            builder.Clear();

            builder.AppendJoin("\n", logger.Lines);

            batch.Begin(SpriteSortMode.Immediate);
            batch.DrawString(font, builder, position, textColor);
            batch.End();
        }

        private sealed class Logger : ILogger
        {
            private readonly int lineCount;

            public Logger(int lineCount)
            {
                this.lineCount = lineCount;
            }

            readonly ConcurrentQueue<string> lines = new();

            public IEnumerable<string> Lines { get => lines; }

            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                while (lines.Count > lineCount)
                {
                    if (!lines.TryDequeue(out var t))
                    {
                        throw new Exception("Shouldn't happen");
                    }
                }

                lines.Enqueue($"{eventId.Name}/{logLevel}: {formatter(state, exception)}");
            }
        }
    }
}
