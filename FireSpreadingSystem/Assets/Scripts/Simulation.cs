using UnityEngine;

namespace FireSpreading
{
    public class Simulation : MonoBehaviour
    {
        public class SimulationState : IRepresentative
        {
            public SimulationState(string name) { _name = name; }
            private string _name;

            public string GetName() { return _name; }
        }

        [SerializeField] private UnityEngine.UI.Slider _simulationSlider;
        [SerializeField] private NameRepresenter _nameRepresenter;

        private SimulationState _stateOn;
        private SimulationState _stateOff;
        private bool _simulate = true;

        public bool IsSimulating => _simulate;

        private void OnEnable()
        {
            _simulationSlider.onValueChanged.AddListener(delegate { SetSimulating(); });
            _stateOn = new SimulationState("On");
            _stateOff = new SimulationState("Off");
            _nameRepresenter.RepresentName(_stateOn);
        }

        private void SetSimulating()
        {
            _simulate = _simulationSlider.value != 0;
            SimulationState stateRef = _simulate ? _stateOn : _stateOff;
            _nameRepresenter.RepresentName(stateRef);
        }

        private void OnDisable()
        {
            _simulationSlider.onValueChanged.RemoveListener(delegate { SetSimulating(); });
        }
    }
}

