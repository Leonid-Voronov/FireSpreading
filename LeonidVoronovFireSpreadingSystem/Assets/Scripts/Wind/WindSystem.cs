using UnityEngine;
using UnityEngine.UI;

namespace FireSpreading
{
    public class WindSystem : MonoBehaviour
    {
        [SerializeField] private WindRepresenter _windRepresenter;
        [SerializeField] private Slider _windDirectionSlider;
        [SerializeField] private Slider _windSpeedSlider;
        [SerializeField] private float _maxWindSpeed;

        private Vector3 _windDirection;
        private float _windSpeed;

        public Vector3 WindDirection => _windDirection;

        private void OnEnable()
        {
            _windDirectionSlider.onValueChanged.AddListener (delegate { SetWindDirection(); });
            _windSpeedSlider.onValueChanged.AddListener (delegate { SetWindSpeed(); });
        }

        private void SetWindSpeed()
        {
            float newValue = _maxWindSpeed * _windSpeedSlider.value;

            if (newValue < 0)
                return;

            _windSpeed = newValue;
        }

        private void SetWindDirection()
        {
            int newValue = Mathf.RoundToInt(_windDirectionSlider.value);
            if (newValue < 0 || newValue > 360)
                return;

            float theta = newValue * Mathf.PI / 180;
            _windDirection = new Vector3(Mathf.Cos(theta), 0, Mathf.Sin(theta));

            _windRepresenter.RepresentWindDirection(newValue);
        }

        private void OnDisable()
        {
            _windDirectionSlider.onValueChanged.RemoveListener (delegate { SetWindDirection(); });
            _windSpeedSlider.onValueChanged.RemoveListener(delegate { SetWindSpeed(); });
        }
    }
}

