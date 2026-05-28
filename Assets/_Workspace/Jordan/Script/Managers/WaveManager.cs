using UnityEngine;
using UnityEngine.Serialization;

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

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            StartNextWave();
        }

        private void Update()
        {
            HandleSpawning();
            HandleWaveEnd();
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
            int randomSpawner = Random.Range(0, _spawners.Length);

            _spawners[randomSpawner].SpawnIA();

            _enemiesAlive++;
        }

        private void HandleWaveEnd()
        {
            if (_enemiesAlive <= 0 && !_isSpawning && !_waveFinished)
            {
                _waveFinished = true;

                _waveTimer = _timeBetweenWaves;

                Debug.Log("Wave Finished");
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
            Debug.Log("Wave : " + _currentWave);
            _enemiesToSpawn = 5 + _currentWave * 3;
            _spawnedEnemies = 0;

            _waveFinished = false;
            _isSpawning = true;
        }

        public void EnemyKilled()
        {
            _enemiesAlive--;
            Debug.Log("Enemies Left : " + _enemiesAlive);
        }
    }
}