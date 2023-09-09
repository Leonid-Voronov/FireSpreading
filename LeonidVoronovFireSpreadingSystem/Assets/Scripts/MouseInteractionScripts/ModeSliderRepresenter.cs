using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FireSpreading
{
    public class ModeSliderRepresenter : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private Slider _slider;
        [SerializeField] private List<MouseMode> mouseModes = new List<MouseMode>();

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(delegate { RepresentSliderValue(); });
            RepresentSliderValue();
        }

        private void RepresentSliderValue()
        {
            _text.text = mouseModes[(int)_slider.value].ToString();
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(delegate { RepresentSliderValue(); });
        }
    }
}

