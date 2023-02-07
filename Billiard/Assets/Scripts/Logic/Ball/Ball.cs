using System;
using UnityEngine;

namespace Logic
{
    public class Ball : MonoBehaviour
    {
        public event Action OnDie;
        
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private SelfDestroying _destroying;

        private void Awake() 
            => _triggerObserver.OnTriggered += Die;

        private void OnDestroy() 
            => _triggerObserver.OnTriggered -= Die;

        private void Die()
        {
            OnDie?.Invoke();
            _destroying.SelfDestroy();
        }
    }
}