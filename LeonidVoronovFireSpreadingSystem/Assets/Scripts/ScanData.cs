using UnityEngine;

namespace FireSpreading
{
    [System.Serializable]
    public class ScanData 
    {
        public ScanData(Vector3 startPosition, float scanRadius, int layerMask) 
        {
            _startPosition = startPosition;
            _scanRadius = scanRadius;
            _layerMask = layerMask;
        }

        private Vector3 _startPosition;
        private float _scanRadius;
        private int _layerMask;

        public Vector3 StartPosition => _startPosition;
        public float ScanRadius => _scanRadius;
        public int LayerMask => _layerMask;
    }
}

