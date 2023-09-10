using System.Collections.Generic;
using UnityEngine;

namespace FireSpreading
{
    public class PlantSpawner : MonoBehaviour
    {
        [Header ("References")]
        [SerializeField] private TerrainTracker _terrainTracker;
        [SerializeField] private GameObject _plantPrefab;
        [SerializeField] private NeighboursSearcher _neighboursSearcher;
        [SerializeField] private List<ChildCleaner> _childCleaners;
        [SerializeField] private Transform _regularPlantsTransform;
        [SerializeField] private Transform _burningPlantsTransform;
        [SerializeField] private Transform _burntPlantsTransform;
        [SerializeField] private WindSystem _windSystem;
        [SerializeField] private InteractionSystem _interactionSystem;
        [SerializeField] private UnityEngine.UI.Slider _generationPresetSlider;
        [SerializeField] private NameRepresenter _nameRepresenter;

        [Header ("Presets")]
        [SerializeField] private List<GenerationPreset> _generationPresets = new List<GenerationPreset>();

        private int _currentPresetIndex;

        private void OnEnable()
        {
            _generationPresetSlider.onValueChanged.AddListener(delegate { SetGenerationPreset(); });
            SetGenerationPreset();
        }

        public Plant SpawnPlant(Vector3 spawnPosition)
        {
            int coreSize = Random.Range(_generationPresets[_currentPresetIndex].MinPlantSize, _generationPresets[_currentPresetIndex].MaxPlantSize);
            GameObject newPlant = Instantiate(_plantPrefab, spawnPosition, Quaternion.identity, _regularPlantsTransform);
            newPlant.transform.localScale = new Vector3(coreSize, coreSize * 2, coreSize);
            Transform newPlantTransform = newPlant.transform;
            
            newPlantTransform.position = new Vector3 (newPlantTransform.position.x, 
                                                      newPlantTransform.position.y + newPlantTransform.localScale.y / 2, 
                                                      newPlantTransform.position.z); //raise plant to surface

            Plant plantComponent = newPlant.GetComponent<Plant>();
            plantComponent.SetDependenciesAndData(_neighboursSearcher, _regularPlantsTransform, _burningPlantsTransform, _burntPlantsTransform, _windSystem, _interactionSystem, _generationPresets[_currentPresetIndex].FirePropogationRadiusMultiplier);
            return plantComponent;
        }

        private Vector3 FindAvailablePosition()
        {
            RaycastHit hit = RaycastAtTerrain();
            GameObject struckObject = hit.collider.gameObject;

            while (struckObject.layer != _terrainTracker.TerrainLayerNumber)
            {
                hit = RaycastAtTerrain();
                struckObject = hit.collider.gameObject;
            }

            return hit.point;
        }

        private RaycastHit RaycastAtTerrain()
        {
            float randomX = Random.Range(_terrainTracker.TerrainWidth * 0.1f , _terrainTracker.TerrainWidth * 0.9f); // values are chosen to avoid spawning on terrain bounds
            float randomZ = Random.Range(_terrainTracker.TerrainLength * 0.1f, _terrainTracker.TerrainLength * 0.9f);
            Vector3 rayStart = new Vector3(randomX, _terrainTracker.TerrainHeight, randomZ);

            Physics.Raycast(rayStart, Vector3.down, out RaycastHit hitInfo, _terrainTracker.TerrainHeight * 2);
            
            return hitInfo;
        }

        private void SetGenerationPreset()
        {
            _currentPresetIndex = (int)_generationPresetSlider.value;
            _nameRepresenter.RepresentName(_generationPresets[_currentPresetIndex]);
        }

        public void GeneratePlants()
        {
            Clear();
            for (int i = 0; i < _generationPresets[_currentPresetIndex].AmountPerGeneration; i++)
            {
                SpawnPlant(FindAvailablePosition());
            }
        }

        public void Clear()
        {
            foreach (ChildCleaner childCleaner in _childCleaners)
                childCleaner.DestroyChildren();
        }

        private void OnDisable()
        {
            _generationPresetSlider.onValueChanged.RemoveListener(delegate { SetGenerationPreset(); });
        }
    }
}

