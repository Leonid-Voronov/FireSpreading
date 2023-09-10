using System.Collections.Generic;
using UnityEngine;

namespace FireSpreading
{
    public class Plant : MonoBehaviour, IFlammable
    {
        [Header("References")]
        [SerializeField] private MouseInteractor _mouseInteractor;
        [SerializeField] private MeshRenderer _meshRenderer;

        [Header("Values")]
        [SerializeField] private float _healthMax;
        [SerializeField] private float _damageRate;
        [SerializeField] private float _burnTime;
        [SerializeField] private bool _affectedByWind;
        [SerializeField] private int _energyToBurn;

        private Transform _regularPlantsTransform;
        private Transform _burningPlantsTransform;
        private Transform _burntPlantsTransform;
        private NeighboursSearcher _neighboursSearcher;
        private WindSystem _windSystem;
        private FireStarter _fireStarter;
        private Simulation _simulation;

        private BurningInspector _burningInspector;
        private BurnableInspector _burnableInspector;
        private IFire _fire;

        private List<IFlammable> _neighbours = new List<IFlammable>();
        private float _firePropagationRadiusMultiplier;
        private float _burnTimer;
        private float _health;
        private bool _burning;
        private bool _burnt = false;

        private void OnEnable()
        {
            ResetValues();
            _burningInspector = new BurningInspector();
            _burnableInspector = new BurnableInspector();
        }

        private void Update()
        {
            if (_burning && _simulation.IsSimulating)
            {
                DamageNeighbours();
                _burnTimer -= Time.deltaTime;

                if (_burnTimer <= 0)
                    BurnOut();
            }
        }

        private void ResetValues()
        {
            _health = _healthMax;
            _burnTimer = _burnTime;
            _burning = false;
        }

        private List<IFlammable> ScanNeighbours(FlammableInspector flammableInspector)
        {
            string layerMaskName = LayerMask.LayerToName(gameObject.layer);
            float scanningRadius = transform.localScale.x * _firePropagationRadiusMultiplier;
            return _neighboursSearcher.FindNeighbours(transform.position, scanningRadius, LayerMask.GetMask(layerMaskName), flammableInspector);
        }

        public void CatchFire(IFire newFire)
        {
            _fire = newFire;
            _fire.VisualizeBurning(_meshRenderer);

            if (_fireStarter.FireRestricted)
                _fire.SpendEnergy(_energyToBurn);
                

            _burning = true;
            transform.parent = _burningPlantsTransform;
            _neighbours = ScanNeighbours(_burnableInspector);
        }

        private void PutOutFire()
        {
            ResetValues();
            transform.parent = _regularPlantsTransform;
            _fire.VisualizeRegular(_meshRenderer);
        }

        public void BurnOut()
        {
            _burning = false;
            transform.parent = _burntPlantsTransform;
            _fire.VisualizeBurnt(_meshRenderer);
            _burnt = true;
        }

        public void DamageNeighbours()
        {
            if (!_simulation.IsSimulating || (_fireStarter.FireRestricted && !_fire.IsEnoughEnergy()))
                return;

            foreach (IFlammable neighbour in _neighbours) 
            {
                float damage = _damageRate * Time.deltaTime;

                if (_affectedByWind)
                {
                    Vector3 neighbourDirection = (neighbour.GetPosition() - transform.position).normalized;
                    float dotProduct = Vector3.Dot(_windSystem.WindDirection, neighbourDirection);
                    float directionDamageModifier = Mathf.Lerp(0, 1, dotProduct);
                    damage = directionDamageModifier * _windSystem.WindSpeed * damage;
                }

                neighbour.LoseHealth(damage, _fire);
            }
        }
        
        public void LoseHealth(float DamageAmount, IFire fire)
        {
            _health -= DamageAmount;

            if (_health <= 0 && !_burning)
                CatchFire(fire);
        }

        public void ToggleFire()
        {
            if (_burning)
                PutOutFire();
            else if (!_burnt)
                CatchFire(_fireStarter.CreateRegularFire());
        }

        public bool IsBurning() { return _burning; }
        public bool CanBurn() { return !_burning && !_burnt; }
        public Vector3 GetPosition() { return transform.position; }
        public void SetDependenciesAndData (PlantDependencies newDependencies, float newPropogationRadiusMultiplier)
        {
            _neighboursSearcher = newDependencies.NeighboursSearcher;
            _regularPlantsTransform = newDependencies.RegularPlantsTransform;
            _burningPlantsTransform = newDependencies.BurningPlantsTransform;
            _burntPlantsTransform = newDependencies.BurntPlantsTransform;
            _windSystem = newDependencies.WindSystem;
            _mouseInteractor.SetInteractionSystem(newDependencies.InteractionSystem);
            _fireStarter = newDependencies.FireStarter;
            _simulation = newDependencies.Simulation;
            _firePropagationRadiusMultiplier = newPropogationRadiusMultiplier;
        }

        public void AwareNeighboursOnSpawning()
        {
            List<IFlammable> burningNeighbours = ScanNeighbours(_burningInspector);
            foreach (IFlammable burningNeighbour in burningNeighbours)
                burningNeighbour.AddToNeighboursList(this);
        }

        public void AwareNeighboursOnRemoving()
        {
            List<IFlammable> burningNeighbours = ScanNeighbours(_burningInspector);
            foreach (IFlammable burningNeighbour in burningNeighbours)
                burningNeighbour.RemoveFromNeighboursList(this);
        }

        public void AddToNeighboursList(IFlammable flammable)
        {
            if (!_neighbours.Contains(flammable))
                _neighbours.Add(flammable);
        }

        public void RemoveFromNeighboursList(IFlammable flammable)
        {
            if (_neighbours.Contains(flammable))
                _neighbours.Remove(flammable);
        }
    }
}

