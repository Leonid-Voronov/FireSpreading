using System.Collections.Generic;
using UnityEngine;
using System;

namespace FireSpreading
{
    public class NeighboursSearcher : MonoBehaviour
    {
        private Collider[] _colliders = new Collider[50];
        public List<IFlammable> FindBurnableNeighbours(ScanData scanData)
        {
            Array.Clear(_colliders, 0, _colliders.Length);
            Physics.OverlapSphereNonAlloc(scanData.StartPosition, scanData.ScanRadius, _colliders, scanData.LayerMask);
            List<IFlammable> result = new List<IFlammable>();

            for (int i = 0; i < _colliders.Length; i++)
            {
                if (_colliders[i] != null)
                {
                    IFlammable neighbour = _colliders[i].GetComponent<IFlammable>();
                    if (neighbour.CanBurn())
                        result.Add(neighbour);
                }
            }

            return result;
        }

        public List<IFlaming> FindBurningNeighbours(ScanData scanData)
        {
            Array.Clear(_colliders, 0, _colliders.Length);
            Physics.OverlapSphereNonAlloc(scanData.StartPosition, scanData.ScanRadius, _colliders, scanData.LayerMask);
            List<IFlaming> result = new List<IFlaming>();

            for (int i = 0; i < _colliders.Length; i++)
            {
                if (_colliders[i] != null)
                {
                    IFlaming neighbour = _colliders[i].GetComponent<IFlaming>();
                    if (neighbour.IsBurning())
                        result.Add(neighbour);
                }
            }

            return result;
        }
    }
}

