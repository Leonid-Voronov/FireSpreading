using UnityEngine;

namespace FireSpreading
{
    public class MouseModeAdd : MouseMode
    {
        public MouseModeAdd() { _name = "Add"; }

        public override void Interact(GameObject _pointedObject, InteractionSystem interactionSystem)
        {
            if (_pointedObject.GetComponent<Terrain>())
            {
                //Add plant
            }
        }
    }
}

