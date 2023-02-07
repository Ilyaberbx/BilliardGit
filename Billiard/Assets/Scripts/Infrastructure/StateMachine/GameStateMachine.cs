using System;
using System.Collections.Generic;
using Infrastructure.Service;
using Infrastructure.Service.Factories;
using Infrastructure.Service.Scene;
using Infrastructure.StateMachine.States;
using Infrastructure.StateMachine.States.StateInterfaces;
using Logic;
using Logic.UI;

namespace Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _statesMap;
        private IExitableState _activeState;

        public GameStateMachine(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain,
            ServiceLocator serviceLocator)
        {
            var sceneLoader = new SceneLoader(coroutineRunner);

            _statesMap = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootStrapState)] = new BootStrapState(sceneLoader, serviceLocator, this),
                [typeof(LoadLevelState)] = new LoadLevelState(loadingCurtain, sceneLoader,serviceLocator.Single<ILevelFactory>(),
                    serviceLocator.Single<IUIFactory>(),serviceLocator.Single<IPlayerFactory>(),
                    serviceLocator.Single<IBallFactory>())
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayLoad>(TPayLoad payLoad) where TState : class, IPayloadedState<TPayLoad>
        {
            TState state = ChangeState<TState>();
            state.Enter(payLoad);
        }

        private TState GetState<TState>() where TState : class, IExitableState
            => _statesMap[typeof(TState)] as TState;

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }
    }
}