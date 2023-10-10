using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FireSpreading
{
    public class NameRepresenter : MonoBehaviour
    {
        [SerializeField] private Text _text;
        public void RepresentName(IRepresentative representative)
        {
            _text.text = representative.GetName();
        }
    }
}

