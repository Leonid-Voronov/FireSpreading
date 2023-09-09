using System.Collections.Generic;
using UnityEngine;

namespace FireSpreading
{
    public class Plant : MonoBehaviour, IFlammable
    {
        [Header("References")]
        [SerializeField] private MouseInteractor _mouseInteractor;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Material _defaultMaterial;
        [SerializeField] private Material _burningMaterial;
        [SerializeField] private Material _burntMaterial;

        [Header("Values")]
        [SerializeField] private int _firePropagationRadiusMultiplier;
        [SerializeField] private float _healthMax;
        [SerializeField] private float _damageRate;
        [SerializeField] private float _burnTime;

        private Transform _regularPlantsTransform;
        private Transform _burningPlantsTransform;
        private Transform _burntPlantsTransform;
        private NeighboursSearcher _neighboursSearcher;
        private WindSystem _windSystem;

        private BurningInspector _burningInspector;
        private BurnableInspector _burnableInspector;

        private List<IFlammable> _neighbours = new List<IFlammable>();
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
            if (_burning)
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

        public void CatchFire()
        {
            _burning = true;
            transform.parent = _burningPlantsTransform;
            _meshRenderer.material = _burningMaterial;

            string layerMaskName = LayerMask.LayerToName(gameObject.layer);
            float scanningRadius = transform.localScale.x * _firePropagationRadiusMultiplier;
            _neighbours = _neighboursSearcher.FindNeighbours(transform.position, scanningRadius , LayerMask.GetMask(layerMaskName), _burnableInspector);
        }

        private void PutOutFire()
        {
            ResetValues();
            transform.parent = _regularPlantsTransform;
            _meshRenderer.material = _defaultMaterial;
        }

        public void BurnOut()
        {
            _burning = false;
            transform.parent = _burntPlantsTransform;
            _meshRenderer.material = _burntMaterial;
            _burnt = true;
        }

        public void DamageNeighbours()
        {
            foreach (IFlammable neighbour in _neighbours) 
            {
                Vector3 neighbourDirection = neighbour.GetPosition() - transform.position;
                float dotProduct = Vector3.Dot(neighbourDirection, _windSystem.WindDirection);

                neighbour.LoseHealth(_damageRate * Time.deltaTime);
            }
        }
        
        public void LoseHealth(float DamageAmount)
        {
            _health -= DamageAmount;

            if (_health <= 0)
                CatchFire();
        }

        public void ToggleFire()
        {
            if (_burning)
                PutOutFire();
            else if (!_burnt)
                CatchFire();
        }

        public bool IsBurning() { return _burning; }
        public bool CanBurn() { return !_burning && !_burnt; }
        public Vector3 GetPosition() { return transform.position; }
        public void SetDependencies (NeighboursSearcher newNeighboursSearcher, Transform newRegularPlantsTransform, Transform newBurningPlantsTransform, Transform newBurntPlantsTransform, WindSystem newWindSystem, InteractionSystem newInteractionSystem)
        {
            _neighboursSearcher = newNeighboursSearcher;
            _regularPlantsTransform = newRegularPlantsTransform;
            _burningPlantsTransform = newBurningPlantsTransform;
            _burntPlantsTransform = newBurntPlantsTransform;
            _windSystem = newWindSystem;

            _mouseInteractor.SetInteractionSystem(newInteractionSystem);
        }

        public void AwareNeighboursOnSpawning()
        {
            string layerMaskName = LayerMask.LayerToName(gameObject.layer);
            float scanningRadius = transform.localScale.x * _firePropagationRadiusMultiplier;
            List<IFlammable> burningNeighbours = _neighboursSearcher.FindNeighbours(transform.position, scanningRadius, LayerMask.GetMask(layerMaskName), _burningInspector);

            foreach (IFlammable burningNeighbour in burningNeighbours)
                burningNeighbour.AddToNeighboursList(this);
        }

        public void AwareNeighboursOnRemoving()
        {
            string layerMaskName = LayerMask.LayerToName(gameObject.layer);
            float scanningRadius = transform.localScale.x * _firePropagationRadiusMultiplier;
            List<IFlammable> burningNeighbours = _neighboursSearcher.FindNeighbours(transform.position, scanningRadius, LayerMask.GetMask(layerMaskName), _burningInspector);

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

