using UnityEngine;

namespace FireSpreading
{
    public class FireStarter : MonoBehaviour
    {
        [SerializeField] private RandomPlantGetter _randomPlantGetter;
        [SerializeField] private int _firePlantsAmount;

        public void FireRandomPlants()
        {

            for (int i = 0; i < _firePlantsAmount; i++)
            {
                GameObject plant = _randomPlantGetter.GetRandomPlant();

                if (plant == null)
                {
                    return;
                }

                plant.GetComponent<IFlammable>().CatchFire();
            }
        }
    }
}

