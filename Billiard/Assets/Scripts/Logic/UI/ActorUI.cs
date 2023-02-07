using UnityEngine;

namespace Logic.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private ForceSlider _slider;
        private BallForceActor _ballForceActor;

        public void Construct(BallForceActor ballForceActor)
        {
            _ballForceActor = ballForceActor;
            _ballForceActor.OnForceChanged += _slider.UpdateValue;
        }

        private void OnDestroy() 
            => _ballForceActor.OnForceChanged -= _slider.UpdateValue;
    }
}