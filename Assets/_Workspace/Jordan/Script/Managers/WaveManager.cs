
using UnityEngine;
using TMPro;

namespace _Workspace.Jordan.Script.ennemi
{
    public class WaveManager : MonoBehaviour
    {
        public static WaveManager Instance;

        [Header("Wave")]
        [SerializeField] private int _currentWave;

        [Header("Enemies")]
        [SerializeField] private int _enemiesAlive;
        [SerializeField] private int _enemiesToSpawn;

        private int _spawnedEnemies;

        [Header("Spawn")]
        [SerializeField] private Spawnner[] _spawners;
        [SerializeField] private float _spawnInterval;

        private float _spawnTimer;

        [Header("Intermission")]
        [SerializeField] private float _timeBetweenWaves;

        private float _waveTimer;

        private bool _isSpawning;
        private bool _waveFinished;

        [Header("UI")]
        [SerializeField] private TMP_Text _enemiesText;
        [SerializeField] private TMP_Text _waveText;
        [SerializeField] private float _waveTextDuration = 3f;

        private float _waveTextTimer;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            UpdateEnemiesUI();
            StartNextWave();
        }

        private void Update()
        {
            HandleSpawning();
            HandleWaveEnd();
            HandleWaveUI();
        }

        private void HandleSpawning()
        {
            if (!_isSpawning) return;

            _spawnTimer += Time.deltaTime;

            if (_spawnTimer >= _spawnInterval)
            {
                _spawnTimer = 0f;

                SpawnEnemy();

                _spawnedEnemies++;

                if (_spawnedEnemies >= _enemiesToSpawn)
                {
                    _isSpawning = false;
                }
            }
        }

        private void SpawnEnemy()
        {
            int attempts = 0;
            Spawnner chosenSpawner = null;

            while (chosenSpawner == null && attempts < 50)
            {
                int index = Random.Range(0, _spawners.Length);

                if (_currentWave >= _spawners[index].WaveRequired)
                {
                    chosenSpawner = _spawners[index];
                }

                attempts++;
            }

            if (chosenSpawner == null)
            {
                return;
            }

            chosenSpawner.SpawnIA();
            _enemiesAlive++;

            UpdateEnemiesUI();
        }

        private void HandleWaveEnd()
        {
            if (_enemiesAlive <= 0 && !_isSpawning && !_waveFinished)
            {
                _waveFinished = true;

                _waveTimer = _timeBetweenWaves;
            }

            if (_waveFinished)
            {
                _waveTimer -= Time.deltaTime;

                if (_waveTimer <= 0f)
                {
                    StartNextWave();
                }
            }
        }

        private void StartNextWave()
        {
            _currentWave++;
            
            _enemiesToSpawn = 5 + _currentWave * 3;
            _spawnedEnemies = 0;

            _waveFinished = false;
            _isSpawning = true;

            _waveText.text = "Manche " + _currentWave;

            _waveTextTimer = _waveTextDuration;

            UpdateEnemiesUI();
        }

        private void HandleWaveUI()
        {
            if (_waveTextTimer > 0)
            {
                _waveTextTimer -= Time.deltaTime;

                if (_waveTextTimer <= 0)
                {
                    _waveText.gameObject.SetActive(false);
                }
            }
        }

        public void EnemyKilled()
        {
            _enemiesAlive--;

            UpdateEnemiesUI();
        }

        private void UpdateEnemiesUI()
        {
            _enemiesText.text = _enemiesAlive.ToString();
        }
    }
}
