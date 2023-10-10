using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FireSpreading 
{
    public class GameMode : MonoBehaviour
    {
        [SerializeField] private PlantSpawner _plantSpawner;
        [SerializeField] private Transform _regularPlantsTransform;
        [SerializeField] private Transform _burningPlantsTransform;
        [SerializeField] private Transform _burntPlantsTransform;
        [SerializeField] private GameObject _restartScreen;
        [SerializeField] private UnityEngine.UI.Button _restartButton;
        [SerializeField] private List<GameStatsRepresenter> _gameStatsRepresenters;

        private bool _gameStarted = false;
        private float _startTimer = 5f;

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(delegate { Restart(); });
        }

        public void StartGame()
        {
            _plantSpawner.GeneratePlants();
            _gameStarted = true;
        }

        private void Update()
        {
            if(_gameStarted)
            {
                int firedTrees = _burningPlantsTransform.childCount + _burntPlantsTransform.childCount;
                int allTreesCount = firedTrees + _regularPlantsTransform.childCount;
                float firedTreesNormalized = (float)firedTrees / (float)allTreesCount * 100f;

                foreach (GameStatsRepresenter gameStatsRepresenter in _gameStatsRepresenters)
                    gameStatsRepresenter.RepresentGameStats(firedTreesNormalized);

                if (_startTimer > 0)
                    _startTimer -= Time.deltaTime;
                else if (_burningPlantsTransform.childCount == 0)
                    _restartScreen.SetActive(true);
            }
        }

        private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnDisable()
        {
            _restartButton.onClick.AddListener(delegate { Restart(); });
        }
    }
}

