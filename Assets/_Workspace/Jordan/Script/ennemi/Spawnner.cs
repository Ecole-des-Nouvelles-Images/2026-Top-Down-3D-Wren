using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Workspace.Jordan.Script.ennemi
{
    public class Spawnner : MonoBehaviour
    {
        [SerializeField] private GameObject _peasants;
        [SerializeField] private GameObject _priest;
        [SerializeField] private List<Transform> _spawns;
        [SerializeField] private float _spawnInterval;
        private float _time;
        
        private void Update()
        {
            _time += Time.deltaTime;
            if (_time >= _spawnInterval)
            {
                SpawnIA();
                _time = 0;
            }
        }

        private void SpawnIA()
        {
            if (_spawns == null || _spawns.Count == 0) 
            {
                return;
            }

            Transform spawnPoint = _spawns[UnityEngine.Random.Range(0, _spawns.Count)];
            Instantiate(_peasants, spawnPoint.position, spawnPoint.rotation);
            Instantiate(_priest, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
