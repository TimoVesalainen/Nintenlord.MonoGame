using Microsoft.Xna.Framework;
using Nintenlord.StateMachines;
using System;

namespace Nintenlord.MonoGame.Games
{
    public sealed class StateMachineComponentManager<TInput, TState> : IGameComponent
    {
        private readonly IStateMachine<TState, TInput> stateMachine;
        private readonly GameComponentCollection gameComponents;
        private readonly Func<TState, IGameComponent> componentCreator;

        private TState currentState;
        private IGameComponent currentComponent;

        public TState CurrentState => currentState;

        public StateMachineComponentManager(IStateMachine<TState, TInput> stateMachine, GameComponentCollection gameComponents, Func<TState, IGameComponent> component)
        {
            this.stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
            this.gameComponents = gameComponents ?? throw new ArgumentNullException(nameof(gameComponents));
            this.componentCreator = component ?? throw new ArgumentNullException(nameof(component));
        }

        public void Initialize()
        {
            currentState = stateMachine.StartState;
            currentComponent = componentCreator(currentState);
            gameComponents.Add(currentComponent);
            currentComponent.Initialize();
        }

        public void Event(TInput input)
        {
            gameComponents.Remove(currentComponent);
            if (currentComponent is IDisposable disposable)
            {
                disposable.Dispose();
            }

            currentState = stateMachine.Transition(currentState, input);
            currentComponent = componentCreator(currentState);

            gameComponents.Add(currentComponent);
        }
    }
}
