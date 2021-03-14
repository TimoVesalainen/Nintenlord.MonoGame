using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Utility;

namespace Nintenlord.MonoGame.Input.Mouse
{
    public sealed class MouseClickEventArgs : GameTimeEventArgs
    {
        public Point MousePosition { get; private set; }
        public MouseButton Button { get; private set; }

        public MouseClickEventArgs(GameTime gameTime, MouseButton button, Point mousePosition)
            : base(gameTime)
        {
            MousePosition = mousePosition;
            Button = button;
        }
    }
}
