using System.Collections.Generic;
using _Workspace.Jordan.Script.Joueur;
using UnityEngine;

namespace _Workspace.Jordan.Script
{
    
    public class GameManager: MonoBehaviour {

        
        [SerializeField] private List<PlayerController> _players = new List<PlayerController>();
        public static  GameManager Instance;

        private void Awake() {
            Instance = this;
        }
        
        
        public void SetNewPlayer(PlayerController player) {
            _players.Add(player);
        }
        
        
    }
}