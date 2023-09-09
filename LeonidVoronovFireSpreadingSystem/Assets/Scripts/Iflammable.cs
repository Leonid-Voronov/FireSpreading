using UnityEngine;

namespace FireSpreading
{
    public interface IFlammable
    {
        public void CatchFire();
        public void DamageNeighbours();
        public void BurnOut();
        public void LoseHealth(float DamageAmount);
        public bool IsBurning();
        public bool CanBurn();
        public Vector3 GetPosition();
        public void AwareNeighboursOnSpawning();
        public void AwareNeighboursOnRemoving();
        public void AddToNeighboursList(IFlammable flammable);
        public void RemoveFromNeighboursList(IFlammable flammable);
    }
}
