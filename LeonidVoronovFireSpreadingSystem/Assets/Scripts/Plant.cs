using System.Collections.Generic;
using UnityEngine;

namespace FireSpreading
{
    public class Plant : MonoBehaviour, IFlammable
    {
        [Header("References")]
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private PlantFlamingComponent _plantFlamingComponent;

        [Header("Values")]
        [SerializeField] private float _healthMax;

        private float _health;

        private void OnEnable()
        {
            ResetValues();
        }
        private void ResetValues() { _health = _healthMax; }
        public void LoseHealth(float damageAmount, IFire fire)
        {
            _health -= damageAmount;
            

            if (_health <= 0 && !_plantFlamingComponent.Burning)
                _plantFlamingComponent.CatchFire(fire);
        }

        public void ToggleFire()
        {
            if (_plantFlamingComponent.Burning)
                _plantFlamingComponent.PutOutFire();
            else if (!_plantFlamingComponent.Burnt)
                _plantFlamingComponent.CatchFire(_plantFlamingComponent.FireStarter.CreateRegularFire());
        }
        
        public bool CanBurn() { return !_plantFlamingComponent.Burning && !_plantFlamingComponent.Burnt; }
        public Vector3 GetPosition() { return transform.position; }

        public void AwareNeighboursOnSpawning()
        {
            List<IFlaming> burningNeighbours = _plantFlamingComponent.NeighboursSearcher.FindBurningNeighbours(_plantFlamingComponent.ScanData); 
            foreach (IFlaming burningNeighbour in burningNeighbours)
                burningNeighbour.AddToNeighboursList(this);
        }

        public void AwareNeighboursOnRemoving()
        {
            List<IFlaming> burningNeighbours = _plantFlamingComponent.NeighboursSearcher.FindBurningNeighbours(_plantFlamingComponent.ScanData);
            foreach (IFlaming burningNeighbour in burningNeighbours)
                burningNeighbour.RemoveFromNeighboursList(this);
        }
    }
}

