using Infrastructure.Service;
using Infrastructure.StateMachine;
using Logic;
using Logic.UI;

namespace Infrastructure
{
    internal sealed class Game
    {
        public readonly GameStateMachine StateMachine;
        public Game(ICoroutineRunner coroutineRunner,LoadingCurtain loadingCurtain) 
            => StateMachine = InitStateMachine(coroutineRunner, loadingCurtain);

        private static GameStateMachine InitStateMachine(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain) 
            => new GameStateMachine(coroutineRunner, loadingCurtain,ServiceLocator.Container);
    }
}