using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FireSpreading
{
    public class MouseModeRepresenter : MonoBehaviour
    {
        [SerializeField] private Text _text;
        public void RepresentModeValue(MouseMode newMouseMode)
        {
            _text.text = newMouseMode.GetName();
        }
    }
}

