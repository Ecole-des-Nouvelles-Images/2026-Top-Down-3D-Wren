using System.Collections.Generic;
using UnityEngine;

namespace _Workspace.Jordan.Script.ennemi
{
    public class Spawnner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _aiPrefabs;

        public void SpawnIA()
        {
            if (_aiPrefabs.Count == 0) return;

            int index = Random.Range(0, _aiPrefabs.Count);

            Instantiate(_aiPrefabs[index], transform.position, Quaternion.identity);
        }
    }
}