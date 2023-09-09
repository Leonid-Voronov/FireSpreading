using UnityEngine;

namespace FireSpreading
{
    public class MouseModeToggleFire : MouseMode
    {
        public MouseModeToggleFire() { _name = "ToggleFire"; }

        public override void Interact(GameObject _pointedObject, InteractionSystem interactionSystem)
        {
            if (_pointedObject.TryGetComponent(out Plant plantComponent)) 
                plantComponent.ToggleFire();
        }
    }
}

