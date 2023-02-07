using System;
using UnityEngine;

namespace Logic
{
    public class TriggerObserver : MonoBehaviour
    {
        public event Action OnTriggered;

        private void OnTriggerEnter(Collider other) 
            => OnTriggered?.Invoke();
    }
}