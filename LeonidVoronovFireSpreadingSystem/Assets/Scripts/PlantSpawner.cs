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

        [Header ("Values")]
        [SerializeField] private int _amountPerGeneration; 
        [SerializeField] private int _minPlantSize;
        [SerializeField] private int _maxPlantSize;

        private void GeneratePlant()
        {
            int coreSize = Random.Range(_minPlantSize, _maxPlantSize);
            GameObject newPlant = Instantiate(_plantPrefab, FindAvailablePosition(), Quaternion.identity, _regularPlantsTransform);
            newPlant.transform.localScale = new Vector3(coreSize, coreSize * 2, coreSize);
            Transform newPlantTransform = newPlant.transform;
            
            newPlantTransform.position = new Vector3 (newPlantTransform.position.x, 
                                                      newPlantTransform.position.y + newPlantTransform.localScale.y / 2, 
                                                      newPlantTransform.position.z); //raise plant to surface

            Plant plantComponent = newPlant.GetComponent<Plant>();
            plantComponent.SetDependencies(_neighboursSearcher, _burningPlantsTransform, _burntPlantsTransform, _windSystem);
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

        public void GeneratePlants()
        {
            Clear();
            for (int i = 0; i < _amountPerGeneration; i++)
            {
                GeneratePlant();
            }
        }

        public void Clear()
        {
            foreach (ChildCleaner childCleaner in _childCleaners)
                childCleaner.DestroyChildren();
        }
    }
}

