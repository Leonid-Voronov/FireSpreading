using UnityEngine;

namespace FireSpreading
{
    public class MouseModeRemove : MouseMode
    {
        public MouseModeRemove() { _name = "Remove"; }

        public override void Interact(GameObject pointedObject, InteractionSystem interactionSystem)
        {
            if (pointedObject.TryGetComponent(out Plant plantComponent))
            {
                plantComponent.AwareNeighboursOnRemoving();
                interactionSystem.ResetPointedObject(pointedObject);
                MonoBehaviour.Destroy(pointedObject);

                //Scan for burning trees and stop being their neighbour
            }
        }
    }
}

