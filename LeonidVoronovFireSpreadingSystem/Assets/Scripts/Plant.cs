using System.Collections.Generic;
using UnityEngine;

namespace FireSpreading
{
    public class Plant : MonoBehaviour, IFlammable
    {
        [Header("References")]
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Material _burningMaterial;
        [SerializeField] private Material _burntMaterial;

        [Header("Values")]
        [SerializeField] private int _firePropagationRadiusMultiplier;
        [SerializeField] private float _health;
        [SerializeField] private float _damageRate;
        [SerializeField] private float _burnTime;

        private Transform _burningPlantsTransform;
        private Transform _burntPlantsTransform;
        private NeighboursSearcher _neighboursSearcher;
        private WindSystem _windSystem;

        private List<IFlammable> _neighbours = new List<IFlammable>();
        private float _burnTimer;
        private bool _burning = false;
        private bool _burnt = false;

        private void OnEnable()
        {
            _burnTimer = _burnTime;    
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

        public void CatchFire()
        {
            _burning = true;
            transform.parent = _burningPlantsTransform;
            _meshRenderer.material = _burningMaterial;
            string layerMaskName = LayerMask.LayerToName(gameObject.layer);
            _neighbours = _neighboursSearcher.FindNeighbours(transform.position, transform.localScale.x * _firePropagationRadiusMultiplier, LayerMask.GetMask(layerMaskName));
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

        public bool IsBurning() { return _burning; }
        public bool CanBurn() { return !_burning && !_burnt; }
        public Vector3 GetPosition() { return transform.position; }
        public void SetDependencies (NeighboursSearcher newNeighboursSearcher, Transform newBurningPlantsTransform, Transform newBurntPlantsTransform, WindSystem newWindSystem)
        {
            _neighboursSearcher = newNeighboursSearcher;
            _burningPlantsTransform = newBurningPlantsTransform;
            _burntPlantsTransform = newBurntPlantsTransform;
            _windSystem = newWindSystem;
        }
    }
}

