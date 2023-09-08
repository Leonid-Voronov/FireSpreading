using UnityEngine;
using UnityEngine.UI;

namespace FireSpreading
{
    public class SliderValueRepresenter : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Text _text;
        [SerializeField] private string format;

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener (delegate { RepresentSliderValue(); });
        }

        private void RepresentSliderValue()
        {
            _text.text = _slider.value.ToString(format);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener (delegate { RepresentSliderValue(); });
        }
    }
}

