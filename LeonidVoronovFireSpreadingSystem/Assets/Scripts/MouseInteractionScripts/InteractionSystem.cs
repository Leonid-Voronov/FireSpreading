using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FireSpreading
{
    public class InteractionSystem : MonoBehaviour
    {
        [SerializeField] private PlantSpawner _plantSpawner;
        [SerializeField] private MouseModeRepresenter _mouseModeRepresenter;
        [SerializeField] private Slider _slider;

        private GameObject _pointedObject;
        private MouseMode _currentMouseMode;

        private MouseModeAdd _mouseModeAdd;
        private MouseModeRemove _mouseModeRemove;
        private MouseModeToggleFire _mouseModeToggleFire;

        private List<MouseMode> _availableMouseModes = new List<MouseMode>();

        private void Start()
        {
            _mouseModeAdd = new MouseModeAdd(_plantSpawner);
            _mouseModeRemove = new MouseModeRemove();
            _mouseModeToggleFire = new MouseModeToggleFire();

            _availableMouseModes.Add(_mouseModeAdd);
            _availableMouseModes.Add(_mouseModeRemove);
            _availableMouseModes.Add(_mouseModeToggleFire);

            _currentMouseMode = _mouseModeAdd;

            _slider.onValueChanged.AddListener(delegate { SetMouseMode(); });
            _mouseModeRepresenter.RepresentModeValue(_currentMouseMode);
        }

        private void Update() 
        {
            if (Input.GetMouseButtonDown(0) && _pointedObject != null)
            {
                _currentMouseMode.Interact(_pointedObject, this);
            }
        }

        private void SetMouseMode() 
        { 
            _currentMouseMode = _availableMouseModes[(int)_slider.value];
            _mouseModeRepresenter.RepresentModeValue(_currentMouseMode);
        }
        public void SetPointedObject(GameObject newObject) { _pointedObject = newObject; }
        public void ResetPointedObject(GameObject sender)
        {
            if (sender == _pointedObject)
                _pointedObject = null;
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(delegate { SetMouseMode(); });
        }
    }
}

