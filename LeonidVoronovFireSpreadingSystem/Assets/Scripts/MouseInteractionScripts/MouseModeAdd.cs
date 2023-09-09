using UnityEngine;

namespace FireSpreading
{
    public class MouseModeAdd : MouseMode
    {
        public MouseModeAdd(PlantSpawner plantSpawner) 
        { 
            _name = "Add"; 
            _plantSpawner = plantSpawner;
        }

        private PlantSpawner _plantSpawner;

        public override void Interact(GameObject pointedObject, InteractionSystem interactionSystem)
        {
            if (pointedObject.GetComponent<Terrain>())
            {
                Vector3 screenPosition = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(screenPosition);
                Physics.Raycast(ray, out RaycastHit hitInfo);

                if (hitInfo.collider.GetComponent<Terrain>())
                {
                    Plant newPlant = _plantSpawner.SpawnPlant(hitInfo.point);
                    newPlant.AwareNeighboursOnSpawning();
                }
                    
            }
        }
    }
}

