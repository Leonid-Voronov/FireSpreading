using UnityEngine;
using UnityEngine.UI;

namespace FireSpreading
{
    public class WindRepresenter : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;

        public void RepresentWindDirection(int newValue)
        {
            _rectTransform.rotation = Quaternion.Euler(0, 0, newValue);
        }
    }
}

