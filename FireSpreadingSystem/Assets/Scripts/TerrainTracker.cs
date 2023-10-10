using UnityEngine;

namespace FireSpreading
{
    public class TerrainTracker : MonoBehaviour
    {
        [SerializeField] private Terrain _terrain;

        private int width;
        private int height;
        private int length;
        private int layerNumber;

        public int TerrainWidth => width;
        public int TerrainHeight => height;
        public int TerrainLength => length;
        public int TerrainLayerNumber => layerNumber;

        private void Awake()
        {
            width = (int)_terrain.terrainData.size.x;
            height = (int)_terrain.terrainData.size.y;
            length = (int)_terrain.terrainData.size.z;
            layerNumber = _terrain.gameObject.layer;
        }
    }
}

