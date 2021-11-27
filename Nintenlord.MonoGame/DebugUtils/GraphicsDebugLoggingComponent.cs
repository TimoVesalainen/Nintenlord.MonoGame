using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nintenlord.MonoGame.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.MonoGame.Debug
{
    public sealed class GraphicsDebugLoggingComponent<TGame> : GameComponent<TGame> where TGame : Game
    {
        ILogger logger;
        GraphicsDebug debug;

        public GraphicsDebugLoggingComponent(TGame game) : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            logger = this.MyGame.Services.GetService<ILogger>();
            debug = this.MyGame.GraphicsDevice.GraphicsDebug ?? throw new InvalidOperationException("Can't get GraphicsDebug from GraphicsDevice");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            while (debug.TryDequeueMessage(out var message))
            {
                logger.Log(GetLogLevel(message), ToString(message));
            }
        }

        private string ToString(GraphicsDebugMessage message)
        {
            return $"{message.Category}/{message.Severity}: {message.Id} {message.IdName}: {message.Message}";
        }

        private LogLevel GetLogLevel(GraphicsDebugMessage message)
        {
            //TODO: Use message.Severity to get this.
            return LogLevel.Information;
        }
    }
}
