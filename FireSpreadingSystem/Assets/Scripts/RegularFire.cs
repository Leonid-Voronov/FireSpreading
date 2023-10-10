using UnityEngine;

namespace FireSpreading
{
    public class RegularFire : IFire
    {
        public RegularFire(Material regularMaterial, Material burningMaterial, Material burntMaterial, int energy)
        {
            _regularMaterial = regularMaterial;
            _burningMaterial = burningMaterial;
            _burntMaterial = burntMaterial;
            _energy = energy;
        }

        private Material _regularMaterial;
        private Material _burningMaterial;
        private Material _burntMaterial;
        private int _energy;

        public void VisualizeBurning(MeshRenderer meshRenderer) { meshRenderer.material = _burningMaterial; }
        public void VisualizeBurnt(MeshRenderer meshRenderer) { meshRenderer.material = _burntMaterial; }
        public void VisualizeRegular(MeshRenderer meshRenderer) { meshRenderer.material = _regularMaterial; }
        public bool IsEnoughEnergy() { return _energy > 0; }
        public void SpendEnergy(int spendAmount) { _energy -= spendAmount; }
    }
}

