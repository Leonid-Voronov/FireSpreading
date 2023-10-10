using UnityEngine;

namespace FireSpreading
{
    [System.Serializable]
    public class GenerationPreset : IRepresentative
    {
        [SerializeField] private string _presetName;
        [SerializeField] private int _amountPerGeneration;
        [SerializeField] private int _minPlantSize;
        [SerializeField] private int _maxPlantSize;
        [SerializeField] private float _firePropogationRadiusMultiplier;

        public string GetName() { return _presetName; }
        public int AmountPerGeneration => _amountPerGeneration;
        public int MinPlantSize => _minPlantSize;
        public int MaxPlantSize => _maxPlantSize;
        public float FirePropogationRadiusMultiplier => _firePropogationRadiusMultiplier;
    }
}

