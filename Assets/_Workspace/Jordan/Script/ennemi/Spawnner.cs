using System.Collections.Generic;
using _Workspace.Jordan.Script.AudioListener;
using UnityEngine;

namespace _Workspace.Jordan.Script.ennemi
{
    public class Spawnner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _aiPrefabs;

        [Header("Wave Settings")]
        [SerializeField] private int _waveRequired = 1;

        public int WaveRequired => _waveRequired;

        public void SpawnIA()
        {
            if (_aiPrefabs.Count == 0) return;

            int index = Random.Range(0, _aiPrefabs.Count);

            Instantiate(_aiPrefabs[index], transform.position, Quaternion.identity);
        }
    }
}