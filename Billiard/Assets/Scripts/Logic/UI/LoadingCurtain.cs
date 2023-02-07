using DG.Tweening;
using UnityEngine;

namespace Logic.UI
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        private void Awake()
            => DontDestroyOnLoad(this);

        public void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1;
        }
        public void Hide()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(_canvasGroup.DOFade(0, 1f));
            sequence.AppendCallback(DisableObject);
        }

        private void DisableObject() 
            => gameObject.SetActive(false);
    }
}
