using Infrastructure.StateMachine.States;
using Logic;
using Logic.UI;
using UnityEngine;

namespace Infrastructure
{
    internal sealed class GameBootstrapper : MonoBehaviour,ICoroutineRunner
    {
        [SerializeField] 
        private LoadingCurtain _loadingCurtain;
        
        private Game _game;

        private void Awake()
        {
            PerformGameInitializationProcess();
            DontDestroyOnLoad(this);
        }

        private void PerformGameInitializationProcess()
        {
            _game = new Game(this, _loadingCurtain);
            _game.StateMachine.Enter<BootStrapState>();
        }
    }
}
