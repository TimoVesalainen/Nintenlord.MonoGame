using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameMouse = Microsoft.Xna.Framework.Input.Mouse;

namespace Nintenlord.MonoGame.Input.Mouse
{
    public sealed class FPSMouseControls : GameComponent
    {
        private Point center;
        private bool isFixedToCenter;
        private MouseState previousState;

        public int ChangeX { get; private set; }
        public int ChangeY { get; private set; }
        public int ChangeWheel { get; private set; }
        public bool LeftPressed { get; private set; }
        public bool RightPressed { get; private set; }

        public bool IsFixedToCenter
        {
            get => isFixedToCenter;
            set
            {
                if (value)
                {
                    SetToCenter();
                }
                isFixedToCenter = value;
            }
        }
        public Point MousePosition
        {
            get
            {
                if (IsFixedToCenter)
                {
                    return center;
                }
                else
                {
                    MouseState state = GameMouse.GetState();
                    return new Point(state.X, state.Y);
                }
            }
        }

        public FPSMouseControls(Game game, bool startFixedToCenter = true)
            : base(game)
        {
            IsFixedToCenter = startFixedToCenter;

            previousState = GameMouse.GetState();
        }



        public override void Initialize()
        {
            base.Initialize();
            center = new Point(this.Game.Window.ClientBounds.Width / 2, this.Game.Window.ClientBounds.Height / 2);
            if (IsFixedToCenter)
            {
                SetToCenter();
            }
        }

        public override void Update(GameTime gameTime)
        {
            MouseState currentMousestate = GameMouse.GetState();

            center = new Point(this.Game.Window.ClientBounds.Width / 2, this.Game.Window.ClientBounds.Height / 2);
            if (IsFixedToCenter)
            {
                ChangeX = center.X - currentMousestate.X;
                ChangeY = center.Y - currentMousestate.Y;
                SetToCenter();
            }
            else
            {
                ChangeX = previousState.X - currentMousestate.X;
                ChangeY = previousState.Y - currentMousestate.Y;
            }

            ChangeWheel = currentMousestate.ScrollWheelValue - previousState.ScrollWheelValue;

            LeftPressed = currentMousestate.LeftButton == ButtonState.Pressed &&
                          previousState.LeftButton == ButtonState.Released;

            RightPressed = currentMousestate.RightButton == ButtonState.Pressed &&
                           previousState.RightButton == ButtonState.Released;

            previousState = currentMousestate;
        }

        private void SetToCenter()
        {
            GameMouse.SetPosition(center.X, center.Y);
        }
    }
}
