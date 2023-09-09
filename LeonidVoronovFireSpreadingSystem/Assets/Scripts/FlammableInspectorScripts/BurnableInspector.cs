namespace FireSpreading
{
    public class BurnableInspector: FlammableInspector
    {
        public override bool CheckRequiredProperties(IFlammable flammable)
        {
            return flammable.CanBurn();
        }
    }
}

