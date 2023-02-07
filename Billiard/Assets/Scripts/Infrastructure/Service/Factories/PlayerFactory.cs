using Data;
using Infrastructure.Service.AssetsProvider;
using Infrastructure.Service.Input;
using Infrastructure.Service.Scene;
using Logic;
using UnityEngine;

namespace Infrastructure.Service.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        public IAssetsProviderService AssetsProvider { get; }
        private readonly IInputService _inputService;
        private readonly ISceneLoaderService _sceneLoader;

        public PlayerFactory(IAssetsProviderService assetsProvider,IInputService inputService,ISceneLoaderService sceneLoader)
        {
            _inputService = inputService;
            _sceneLoader = sceneLoader;
            AssetsProvider = assetsProvider;
        }

        public GameObject CreatePlayer(Vector3 at)
        {
            var player = AssetsProvider.Instantiate(AssetsPath.Player);
            player.transform.position = at;
            
            InitPlayer(player);

            return player;
        }

        private void InitPlayer(GameObject player)
        {
            player.GetComponent<BallForceActor>().Construct(_inputService);
            player.GetComponent<GameRestart>().Construct(_sceneLoader);
        }
    }
}