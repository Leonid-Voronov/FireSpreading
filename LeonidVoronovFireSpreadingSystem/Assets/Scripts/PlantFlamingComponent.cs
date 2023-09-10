using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace FireSpreading
{
    public class PlantFlamingComponent : MonoBehaviour, IFlaming
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private MouseInteractor _mouseInteractor;
        [SerializeField] private Plant _plantComponent;
        [SerializeField] private float _damageRate;
        [SerializeField] private float _burnTime;
        [SerializeField] private bool _affectedByWind;
        [SerializeField] private int _energyToBurn;

        private Simulation _simulation;
        private Transform _regularPlantsTransform;
        private Transform _burningPlantsTransform;
        private Transform _burntPlantsTransform;
        private NeighboursSearcher _neighboursSearcher;
        private FireStarter _fireStarter;
        private WindSystem _windSystem;


        private List<IFlammable> _neighbours = new List<IFlammable>();
        private bool _burning;
        private bool _burnt = false;
        private float _burnTimer;
        private float _firePropagationRadiusMultiplier;
        private IFire _fire;
        private ScanData _scanData;
        public bool Burning => _burning;
        public bool Burnt => _burnt;
        public IFire Fire => _fire;
        public float FirePropogationMultiplier => _firePropagationRadiusMultiplier;
        public ScanData ScanData => _scanData;
        public NeighboursSearcher NeighboursSearcher => _neighboursSearcher;
        public FireStarter FireStarter => _fireStarter;
        public Plant PlantComponent => _plantComponent;

        private void OnEnable()
        {
            ResetValues();
        }

        private void CreateScanData()
        {
            string layerMaskName = LayerMask.LayerToName(gameObject.layer);
            float scanningRadius = transform.localScale.x * _firePropagationRadiusMultiplier;
            _scanData = new ScanData(transform.position, scanningRadius, LayerMask.GetMask(layerMaskName));
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
        public void AddToNeighboursList(IFlammable flammable)
        {
            if (!_neighbours.Contains(flammable))
                _neighbours.Add(flammable);
        }

        public void BurnOut()
        {
            _burning = false;
            transform.parent = _burntPlantsTransform;
            _fire.VisualizeBurnt(_meshRenderer);
            _burnt = true;
        }

        public void CatchFire(IFire newFire)
        {
            _fire = newFire;
            _fire.VisualizeBurning(_meshRenderer);

            if (_fireStarter.FireRestricted)
                _fire.SpendEnergy(_energyToBurn);

            _burning = true;
            transform.parent = _burningPlantsTransform;
            _neighbours = _neighboursSearcher.FindBurnableNeighbours(_scanData);
        }

        public void PutOutFire()
        {
            ResetValues();
            transform.parent = _regularPlantsTransform;
            _fire.VisualizeRegular(_meshRenderer);
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

        public bool IsBurning() { return _burning; }

        public void RemoveFromNeighboursList(IFlammable flammable)
        {
            if (_neighbours.Contains(flammable))
                _neighbours.Remove(flammable);
        }

        public void ResetValues()
        {
            _burnTimer = _burnTime;
            _burning = false;
        }

        public void SetDependenciesAndData(PlantDependencies newDependencies, float newPropogationRadiusMultiplier)
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
            CreateScanData();
        }
    }

}

