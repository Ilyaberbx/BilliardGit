using UnityEngine;
using UnityEngine.UI;

namespace Logic.UI
{
    public class ForceSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        public void UpdateValue(float value)
            => _slider.value = value;
    }
}