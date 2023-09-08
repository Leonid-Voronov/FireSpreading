namespace FireSpreading
{
    public interface IFlammable
    {
        public void CatchFire();
        public void DamageNeighbours();
        public void SetNeighboursSearcher(NeighboursSearcher neighboursSearcher);
    }
}
