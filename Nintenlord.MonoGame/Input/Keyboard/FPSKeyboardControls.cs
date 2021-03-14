using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameKeyboard = Microsoft.Xna.Framework.Input.Keyboard;

namespace Nintenlord.MonoGame.Input.Keyboard
{
    public class FPSKeyboardControls : GameComponent
    {
        private readonly Keys backwardKey = Keys.S;
        private readonly Keys forwardKey = Keys.W;
        private readonly Keys leftKey = Keys.A;
        private readonly Keys rightKey = Keys.D;
        private readonly Keys jumpKey = Keys.Space;

        private KeyboardState previousState;

        public FPSKeyboardControls(Game game)
            : base(game)
        {
        }

        public bool Left { get; private set; }
        public bool Right { get; private set; }
        public bool Forward { get; private set; }
        public bool Backward { get; private set; }
        public bool Up { get; private set; }
        public bool Down { get; private set; }
        public bool Jump { get; private set; }
        public bool Number0 { get; private set; }
        public bool Number1 { get; private set; }
        public bool Number2 { get; private set; }
        public bool Number3 { get; private set; }
        public bool Number4 { get; private set; }
        public bool Number5 { get; private set; }
        public bool Number6 { get; private set; }
        public bool Number7 { get; private set; }
        public bool Number8 { get; private set; }
        public bool Number9 { get; private set; }
        public bool EnterInventory { get; private set; }
        public bool ExitMenu { get; private set; }
        public bool SpewItems { get; private set; }
        public bool ToggleFlying { get; private set; }
        public bool ToggleCollision { get; private set; }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyState = GameKeyboard.GetState();

            if (keyState.IsKeyDown(rightKey) && keyState.IsKeyUp(leftKey))
            {
                Right = true;
                Left = false;
            }
            else if (keyState.IsKeyUp(rightKey) && keyState.IsKeyDown(leftKey))
            {
                Right = false;
                Left = true;
            }
            else
            {
                Right = false;
                Left = false;
            }

            if (keyState.IsKeyDown(forwardKey) && keyState.IsKeyUp(backwardKey))
            {
                Forward = true;
                Backward = false;
            }
            else if (keyState.IsKeyUp(forwardKey) && keyState.IsKeyDown(backwardKey))
            {
                Forward = false;
                Backward = true;
            }
            else
            {
                Forward = false;
                Backward = false;
            }

            if (keyState.IsKeyDown(Keys.Space) && keyState.IsKeyUp(Keys.LeftShift))
            {
                Up = true;
                Down = false;
            }
            else if (keyState.IsKeyUp(Keys.Space) && keyState.IsKeyDown(Keys.LeftShift))
            {
                Up = false;
                Down = true;
            }
            else
            {
                Up = false;
                Down = false;
            }

            Jump = Pressed(keyState, jumpKey);
            Number0 = Pressed(keyState, Keys.D1);
            Number1 = Pressed(keyState, Keys.D2);
            Number2 = Pressed(keyState, Keys.D3);
            Number3 = Pressed(keyState, Keys.D4);
            Number4 = Pressed(keyState, Keys.D5);
            Number5 = Pressed(keyState, Keys.D6);
            Number6 = Pressed(keyState, Keys.D7);
            Number7 = Pressed(keyState, Keys.D8);
            Number8 = Pressed(keyState, Keys.D9);
            Number9 = Pressed(keyState, Keys.D0);
            EnterInventory = Pressed(keyState, Keys.E);
            ExitMenu = Pressed(keyState, Keys.Q);
            SpewItems = Pressed(keyState, Keys.R);
            ToggleFlying = Pressed(keyState, Keys.F);
            ToggleCollision = Pressed(keyState, Keys.C);

            previousState = keyState;
        }

        private bool Pressed(KeyboardState keyState, Keys Keys)
        {
            return previousState.IsKeyUp(Keys) && keyState.IsKeyDown(Keys);
        }
    }
}
