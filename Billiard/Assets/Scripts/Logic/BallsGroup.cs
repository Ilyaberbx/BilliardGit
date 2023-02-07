using UnityEngine;

namespace Logic
{
    public class BallsGroup : MonoBehaviour
    {
        private int _ballsCount;
        private GameRestart _gameRestart;

        public void Construct(GameRestart gameRestart) 
            => _gameRestart = gameRestart;

        private void Awake()
        {
            var balls = GetComponentsInChildren<Ball>();
            RegisterOnDestroyed(balls);
            _ballsCount = balls.Length;
        }

        private void OnBallDestroyed()
        {
            _ballsCount--;
            
            if (NoBalls()) 
                _gameRestart.Restart();
        }

        private bool NoBalls() 
            => _ballsCount <= 0;

        private void RegisterOnDestroyed(Ball[] balls)
        {
            foreach (var ball in balls)
                ball.OnDie += OnBallDestroyed;
        }
    }
}