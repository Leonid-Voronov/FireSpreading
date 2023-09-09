using System.Collections.Generic;
using UnityEngine;
using System;

namespace FireSpreading
{
    public class NeighboursSearcher : MonoBehaviour
    {
        private Collider[] _colliders = new Collider[50];

        public List<IFlammable> FindNeighbours(Vector3 startPosition, float radius, int layerMask, FlammableInspector flammableInspector)
        {
            Array.Clear(_colliders, 0, _colliders.Length);
            Physics.OverlapSphereNonAlloc(startPosition, radius, _colliders, layerMask);
            List<IFlammable> result = new List<IFlammable>();

            for (int i = 0; i < _colliders.Length; i++)
            {
                if (_colliders[i] != null)
                {
                    IFlammable neighbour = _colliders[i].GetComponent<IFlammable>();
                    if (flammableInspector.CheckRequiredProperties(neighbour))
                        result.Add(neighbour);
                }
            }

            return result;
        }
    }
}

