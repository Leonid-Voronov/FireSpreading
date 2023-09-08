using System.Collections.Generic;
using UnityEngine;
using System;

namespace FireSpreading
{
    public class NeighboursSearcher : MonoBehaviour
    {
        private Collider[] _colliders = new Collider[50];

        public List<Collider> FindNeighbours(Vector3 startPosition, float radius, int layerMask)
        {
            Array.Clear(_colliders, 0, _colliders.Length);
            Physics.OverlapSphereNonAlloc(startPosition, radius, _colliders, layerMask);
            List<Collider> result = new List<Collider>();

            for (int i = 0; i < _colliders.Length; i++)
            {
                if (_colliders[i] != null)
                    result.Add(_colliders[i]);
            }

            return result;
        }
    }
}

