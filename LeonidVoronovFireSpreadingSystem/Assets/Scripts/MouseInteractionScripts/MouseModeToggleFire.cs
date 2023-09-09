using UnityEngine;

namespace FireSpreading
{
    public class MouseModeToggleFire : MouseMode
    {
        public MouseModeToggleFire() { _name = "ToggleFire"; }

        public override void Interact(GameObject pointedObject, InteractionSystem interactionSystem)
        {
            if (pointedObject.TryGetComponent(out Plant plantComponent)) 
                plantComponent.ToggleFire();
        }
    }
}

