using Infrastructure.Service;
using Infrastructure.Service.AssetsProvider;
using Infrastructure.Service.Factories;
using Infrastructure.Service.Input;
using Infrastructure.Service.Scene;
using Infrastructure.StateMachine.States.StateInterfaces;


namespace Infrastructure.StateMachine.States
{
    public class BootStrapState : IState
    {
        private const string Init = "Init";
        private const string PayLoad = "Gameplay";

        private readonly ISceneLoaderService _sceneLoader;
        private readonly ServiceLocator _serviceLocator;
        private readonly GameStateMachine _stateMachine;

        public BootStrapState(ISceneLoaderService sceneLoader, ServiceLocator serviceLocator,
            GameStateMachine stateMachine)
        {
            _sceneLoader = sceneLoader;
            _serviceLocator = serviceLocator;
            _stateMachine = stateMachine;
            RegisterServices();
        }

        public void Enter() =>
            _sceneLoader.Load(Init, OnLoaded);

        public void Exit() {}

        private void OnLoaded()
            => _stateMachine.Enter<LoadLevelState, string>(PayLoad);

        private void RegisterServices()
        {
            RegisterSceneLoader();
            RegisterAssetProvider();
            RegisterFactories();
        }

        private void RegisterFactories()
        {
            RegisterInput();
            RegisterLevelFactory();
            RegisterUIFactory();
            RegisterPlayerFactory();
            RegisterBallsFactory();
        }

        private void RegisterBallsFactory()
            => _serviceLocator.RegisterService<IBallFactory>
                (new BallFactory(_serviceLocator.Single<IAssetsProviderService>()));

        private void RegisterInput()
            => _serviceLocator.RegisterService<IInputService>
                (new StandaloneInputService());

        private void RegisterPlayerFactory()
            => _serviceLocator.RegisterService<IPlayerFactory>
                (new PlayerFactory(_serviceLocator.Single<IAssetsProviderService>(),
                    _serviceLocator.Single<IInputService>(),_serviceLocator.Single<ISceneLoaderService>()));

        private void RegisterUIFactory()
            => _serviceLocator.RegisterService<IUIFactory>
                (new UIFactory(_serviceLocator.Single<IAssetsProviderService>()));

        private void RegisterLevelFactory() 
            => _serviceLocator.RegisterService<ILevelFactory>
                (new LevelFactory(_serviceLocator.Single<IAssetsProviderService>()));

        private void RegisterAssetProvider()
            => _serviceLocator.RegisterService<IAssetsProviderService>(new AssetProvider());
        

        private void RegisterSceneLoader()
            => _serviceLocator.RegisterService(_sceneLoader);
    }
}