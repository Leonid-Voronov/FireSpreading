using UnityEngine;

namespace FireSpreading
{
    [System.Serializable]
    public class PlantDependencies
    {
        [SerializeField] private NeighboursSearcher _neighboursSearcher;
        [SerializeField] private Transform _regularPlantsTransform;
        [SerializeField] private Transform _burningPlantsTransform;
        [SerializeField] private Transform _burntPlantsTransform;
        [SerializeField] private WindSystem _windSystem;
        [SerializeField] private InteractionSystem _interactionSystem;

        public NeighboursSearcher NeighboursSearcher => _neighboursSearcher;
        public Transform RegularPlantsTransform => _regularPlantsTransform;
        public Transform BurningPlantsTransform => _burningPlantsTransform;
        public Transform BurntPlantsTransform => _burntPlantsTransform;
        public WindSystem WindSystem => _windSystem;
        public InteractionSystem InteractionSystem => _interactionSystem;
    }
}

