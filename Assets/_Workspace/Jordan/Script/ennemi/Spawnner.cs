using System.Collections.Generic;
using UnityEngine;

namespace _Workspace.Jordan.Script.ennemi
{
    public class Spawnner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _aiPrefabs;
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
            if (_aiPrefabs.Count == 0) return;
            
            int index = Random.Range(0, _aiPrefabs.Count);
            Instantiate(_aiPrefabs[index], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, transform);
        }
    }
}
