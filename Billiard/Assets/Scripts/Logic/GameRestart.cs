using Infrastructure.Service.Scene;
using UnityEngine;

namespace Logic
{
    public class GameRestart : MonoBehaviour
    {
        private const string Init = "Init";
        
        [SerializeField] private TriggerObserver triggerObserver;
        
        private ISceneLoaderService _sceneLoader;
        public void Construct(ISceneLoaderService sceneLoader) 
            => _sceneLoader = sceneLoader;
        private void Awake() 
            => triggerObserver.OnTriggered += Restart;
        private void OnDestroy() 
            => triggerObserver.OnTriggered -= Restart;
        public void Restart() 
            => _sceneLoader.Load(Init);
    }
}