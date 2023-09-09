namespace FireSpreading
{
    public class BurningInspector : FlammableInspector
    {
        public override bool CheckRequiredProperties(IFlammable flammable)
        {
            return flammable.IsBurning();
        }
    }
}
