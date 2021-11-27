using Microsoft.Xna.Framework.Input;
using Nintenlord.MonoGame.Utility;
using System.Collections.Generic;
using GameKeyboard = Microsoft.Xna.Framework.Input.Keyboard;

namespace Nintenlord.MonoGame.Input.Keyboard
{
    public sealed class KeyboardKeyMapping<TControl> : IControl<TControl>
    {
        readonly Dictionary<Keys, TControl> controls = new();

        public void SetKeymap(Keys key, TControl control)
        {
            controls[key] = control;
        }

        public IEnumerable<TControl> GetCommands()
        {
            KeyboardState keyState = GameKeyboard.GetState();
            foreach (var (key, control) in controls)
            {
                if (keyState.IsKeyDown(key))
                {
                    yield return control;
                }
            }
        }
    }
}
