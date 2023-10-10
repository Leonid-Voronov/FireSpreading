using UnityEngine;

namespace FireSpreading
{
    public class FireStarter : MonoBehaviour
    {
        [SerializeField] private RandomPlantGetter _randomPlantGetter;
        [SerializeField] private int _firePlantsAmount;
        [SerializeField] private bool _fireRestricted;
        [SerializeField] private int _fireEnergyAmount;

        [Header("Fire dependencies")]
        [SerializeField] private Material _regularMaterial;
        [SerializeField] private Material _burningMaterial;
        [SerializeField] private Material _burntMaterial;

        public bool FireRestricted => _fireRestricted;
        public void FireRandomPlants()
        {
            for (int i = 0; i < _firePlantsAmount; i++)
            {
                GameObject plant = _randomPlantGetter.GetRandomPlant();
                if (plant == null)
                    return;

                RegularFire newFire = CreateRegularFire();
                plant.GetComponent<IFlaming>().CatchFire(newFire);
            }
        }

        public RegularFire CreateRegularFire()
        {
            return new RegularFire(_regularMaterial, _burningMaterial, _burntMaterial, _fireEnergyAmount);
        }
    }
}

