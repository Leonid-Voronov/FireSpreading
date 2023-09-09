using UnityEngine;

namespace FireSpreading
{
    public abstract class MouseMode
    {
        protected string _name; //Inheritors require constructor with name initialization

        public string GetName() { return _name; }
        public abstract void Interact(GameObject _pointedObject, InteractionSystem interactionSystem);
    }
}