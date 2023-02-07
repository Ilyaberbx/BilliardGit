using Infrastructure.Service.Factories;
using Infrastructure.Service.Scene;
using Infrastructure.StateMachine.States.StateInterfaces;
using Logic;
using Logic.UI;
using UnityEngine;

namespace Infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string PlayerSpawnPointTag = "PlayerSpawnPoint";
        private const string BallsGroupSpawnPointTag = "BallsSpawnPoint";
        
        private readonly LoadingCurtain _loadingCurtain;
        private readonly ISceneLoaderService _sceneLoader;
        private readonly ILevelFactory _levelFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IPlayerFactory _playerFactory;
        private readonly IBallFactory _ballFactory;

        public LoadLevelState(LoadingCurtain loadingCurtain, ISceneLoaderService sceneLoader,ILevelFactory 
            levelFactory,IUIFactory uiFactory,IPlayerFactory playerFactory,IBallFactory ballFactory)
        {
            _loadingCurtain = loadingCurtain;
            _sceneLoader = sceneLoader;
            _levelFactory = levelFactory;
            _uiFactory = uiFactory;
            _playerFactory = playerFactory;
            _ballFactory = ballFactory;
        }

        public void Enter(string payLoad)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(payLoad, OnLoaded);
        }

        public void Exit() { }

        private void OnLoaded()
        {
            InitGameWorld();
            _loadingCurtain.Hide();
        }

        private void InitGameWorld()
        {
            _levelFactory.CreateLevel();
            GameObject player = _playerFactory.CreatePlayer(PlayerSpawnPoint());
            GameObject hud =  _uiFactory.CreateHud();
            BallsGroup balls = _ballFactory.CreateBallGroup(BallsGroupSpawnPoint());
            InitGameRestart(balls, player);
            InitActorUI(hud, player);
        }

        private void InitGameRestart(BallsGroup balls, GameObject player) 
            => balls.Construct(player.GetComponent<GameRestart>());

        private void InitActorUI(GameObject hud, GameObject player) 
            => hud.GetComponent<ActorUI>().Construct(player.GetComponent<BallForceActor>());

        private Vector3 BallsGroupSpawnPoint()
            => GameObject.FindWithTag(BallsGroupSpawnPointTag).transform.position;

        private Vector3 PlayerSpawnPoint() 
            => GameObject.FindWithTag(PlayerSpawnPointTag).transform.position;
    }
}