using UnityEngine;

namespace FireSpreading
{
    public class Plant : MonoBehaviour, IFlammable
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Material _burningMaterial;
        [SerializeField] private Material _burntMaterial;
        
        public void CatchFire()
        {
            _meshRenderer.material = _burningMaterial;
        }

        public void DamageNeighbours()
        {
            throw new System.NotImplementedException();
        }
    }
}

