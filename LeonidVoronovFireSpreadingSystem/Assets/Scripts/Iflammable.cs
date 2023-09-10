using UnityEngine;

namespace FireSpreading
{
    public interface IFlammable
    {
        public void LoseHealth(float DamageAmount, IFire fire);
        public bool CanBurn();
        public Vector3 GetPosition();
        public void AwareNeighboursOnSpawning();
        public void AwareNeighboursOnRemoving();
    }
}
