using System;

namespace FireSpreading
{
    public abstract class FlammableInspector
    {
        public abstract bool CheckRequiredProperties(IFlammable flammable);
    }
}

