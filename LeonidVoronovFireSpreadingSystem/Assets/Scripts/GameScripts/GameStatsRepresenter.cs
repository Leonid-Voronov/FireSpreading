using UnityEngine;
using UnityEngine.UI;

namespace FireSpreading
{
    public class GameStatsRepresenter : MonoBehaviour
    {
        [SerializeField] private Text _text;
        public void RepresentGameStats(float newValue)
        {
            string stringValue = newValue.ToString("0.00");
            _text.text = "You've burned " + stringValue + "% of forest!"  ;
        }
    }
}

