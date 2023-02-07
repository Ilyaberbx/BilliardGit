using UnityEngine;

namespace Logic
{
    public class Scaler : MonoBehaviour
    {
        [SerializeField] private float _scale;
        public float Scale => _scale;

        private void OnValidate() 
            => transform.localScale = CalculateScale();

        private Vector3 CalculateScale() 
            => Vector3.one * _scale;
    }
}