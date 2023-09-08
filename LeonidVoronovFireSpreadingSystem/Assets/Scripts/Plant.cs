using System.Collections.Generic;
using UnityEngine;

namespace FireSpreading
{
    public class Plant : MonoBehaviour, IFlammable
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Material _burningMaterial;
        [SerializeField] private Material _burntMaterial;
        [SerializeField] private int _firePropagationRadiusMultiplier;
        private NeighboursSearcher _neighboursSearcher;

        private List<Collider> neighbours = new List<Collider>();

        public void CatchFire()
        {
            _meshRenderer.material = _burningMaterial;
            string layerMaskName = LayerMask.LayerToName(gameObject.layer);
            neighbours = _neighboursSearcher.FindNeighbours(transform.position, transform.localScale.x * _firePropagationRadiusMultiplier, LayerMask.GetMask(layerMaskName));
            Debug.Log(neighbours.Count);
        }

        public void DamageNeighbours()
        {
            throw new System.NotImplementedException();
        }

        public void SetNeighboursSearcher(NeighboursSearcher newNeighboursSearcher)
        {
            _neighboursSearcher = newNeighboursSearcher;
        }
    }
}

