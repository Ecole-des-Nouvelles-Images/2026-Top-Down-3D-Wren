using System.Collections.Generic;
using _Workspace.Jordan.Script.Joueur;
using UnityEngine;

namespace _Workspace.Jordan.Script
{
    public class SpawnPlayer : MonoBehaviour {
        public static int CurrentPlayerIndex;
        [SerializeField] private List<PlayerController> _playerControllers = new List<PlayerController>();
        
        private void Start() {
            Vector3 pos = PlayerSpawnPosition.GetSpawnPosition();
            Instantiate(_playerControllers[1],pos,Quaternion.identity);
            CurrentPlayerIndex++;

            if (CurrentPlayerIndex >= _playerControllers.Count) {
                CurrentPlayerIndex = 0;
            }
        }
        
    }
}