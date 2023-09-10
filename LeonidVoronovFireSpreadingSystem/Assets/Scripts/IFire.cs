using UnityEngine;

namespace FireSpreading
{
    public interface IFire
    {
        public void VisualizeRegular(MeshRenderer meshRenderer);
        public void VisualizeBurning (MeshRenderer meshRenderer);
        public void VisualizeBurnt(MeshRenderer meshRenderer);
        public bool IsEnoughEnergy();
        public void SpendEnergy(int spentAmount);
    }
}
