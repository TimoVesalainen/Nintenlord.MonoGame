using Microsoft.Xna.Framework;
using System;

namespace Nintenlord.MonoGame.Utility
{
    public class GameTimeEventArgs : EventArgs
    {
        public readonly GameTime GameTime;

        public GameTimeEventArgs(GameTime gameTime)
        {
            GameTime = gameTime;
        }
    }
}
