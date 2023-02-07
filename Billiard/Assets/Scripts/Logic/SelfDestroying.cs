using DG.Tweening;
using UnityEngine;

namespace Logic
{
    public class SelfDestroying : MonoBehaviour
    {
        [SerializeField] private float _disappearingDuration = 0.3f;
        
        public void SelfDestroy()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(Disappear());
            sequence.AppendCallback(Destroy);
        }

        private void Destroy() 
            => Destroy(gameObject);

        private Tween Disappear() 
            => transform.DOScale(Vector3.zero, _disappearingDuration);
    }
}