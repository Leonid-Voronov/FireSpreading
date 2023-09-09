using UnityEngine;

namespace FireSpreading
{
    public class MouseInteractor : MonoBehaviour
    {
        private InteractionSystem _interactionSystem;

        private void OnMouseEnter()
        {
            _interactionSystem.SetPointedObject(gameObject);
        }

        private void OnMouseExit() 
        {
            _interactionSystem.ResetPointedObject(gameObject);
        }

        public void SetInteractionSystem(InteractionSystem newSystem) { _interactionSystem = newSystem; }
    }
}

