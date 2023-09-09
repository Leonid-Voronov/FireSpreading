using UnityEngine;

namespace FireSpreading
{
    public class MouseModeRemove : MouseMode
    {
        public MouseModeRemove() { _name = "Remove"; }

        public override void Interact(GameObject _pointedObject, InteractionSystem interactionSystem)
        {
            if (_pointedObject.GetComponent<Plant>())
            {
                interactionSystem.ResetPointedObject(_pointedObject);
                MonoBehaviour.Destroy(_pointedObject);

                //Scan for burning trees and stop being their neighbour
            }
        }
    }
}

