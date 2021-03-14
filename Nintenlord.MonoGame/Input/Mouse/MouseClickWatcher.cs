using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using GameMouse = Microsoft.Xna.Framework.Input.Mouse;

namespace Nintenlord.MonoGame.Input.Mouse
{
    public sealed class MouseClickWatcher : GameComponent
    {
        private readonly Dictionary<object, Tuple<Rectangle, Action<object, MouseClickEventArgs>>> subscriptions;
        private Tuple<Rectangle, Action<object, MouseClickEventArgs>>[] currentActions;
        private MouseState oldState;

        public MouseClickWatcher(Game game, int capacity = 0x10)
            : base(game)
        {
            subscriptions = new Dictionary<object, Tuple<Rectangle, Action<object, MouseClickEventArgs>>>(capacity);
            currentActions = new Tuple<Rectangle, Action<object, MouseClickEventArgs>>[capacity];
        }

        public void AddSubscription(object subscriber, Rectangle area, Action<object, MouseClickEventArgs> onClick)
        {
            subscriptions[subscriber] = new Tuple<Rectangle, Action<object, MouseClickEventArgs>>(area, onClick);
        }

        public void ChangeArea(object subscriber, Rectangle newArea)
        {
            Tuple<Rectangle, Action<object, MouseClickEventArgs>> action = subscriptions[subscriber];

            subscriptions[subscriber] = Tuple.Create(newArea, action.Item2);
        }

        public void RemoveSubscription(object subscriber)
        {
            subscriptions.Remove(subscriber);
        }

        public override void Update(GameTime gameTime)
        {
            if (currentActions.Length < subscriptions.Count)
            {
                currentActions = new Tuple<Rectangle, Action<object, MouseClickEventArgs>>[currentActions.Length * 2];
            }
            int length = subscriptions.Count;
            subscriptions.Values.CopyTo(currentActions, 0);

            MouseState currentState = GameMouse.GetState();
            var point = new Point(currentState.X, currentState.Y);
            bool leftClicked = currentState.LeftButton == ButtonState.Pressed &&
                               oldState.LeftButton == ButtonState.Released;
            bool rightClicked = currentState.RightButton == ButtonState.Pressed &&
                                oldState.RightButton == ButtonState.Released;
            bool middleClicked = currentState.MiddleButton == ButtonState.Pressed &&
                                 oldState.MiddleButton == ButtonState.Released;

            MouseClickEventArgs leftEA = leftClicked ? new MouseClickEventArgs(gameTime, MouseButton.Left, point) : null;
            MouseClickEventArgs rightEA = rightClicked
                                              ? new MouseClickEventArgs(gameTime, MouseButton.Right, point)
                                              : null;
            MouseClickEventArgs middleEA = middleClicked
                                               ? new MouseClickEventArgs(gameTime, MouseButton.Middle, point)
                                               : null;


            for (int i = 0; i < length; i++)
            {
                Tuple<Rectangle, Action<object, MouseClickEventArgs>> tuple = currentActions[i];
                if (tuple.Item1.Contains(point))
                {
                    if (leftClicked)
                    {
                        tuple.Item2(this, leftEA);
                    }

                    if (rightClicked)
                    {
                        tuple.Item2(this, rightEA);
                    }

                    if (middleClicked)
                    {
                        tuple.Item2(this, middleEA);
                    }
                }
            }
            oldState = currentState;
        }
    }
}
