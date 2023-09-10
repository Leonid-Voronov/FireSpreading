namespace FireSpreading
{
    public interface IFlaming
    {
        public void ResetValues();
        public void AddToNeighboursList(IFlammable flammable);
        public void RemoveFromNeighboursList(IFlammable flammable);
        public bool IsBurning();
        public void DamageNeighbours();
        public void CatchFire(IFire fire);
        public void BurnOut();
        public void PutOutFire();
    }
}

