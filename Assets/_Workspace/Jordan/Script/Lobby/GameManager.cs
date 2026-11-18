using System.Collections.Generic;
using _Workspace.Jordan.Script.Joueur;
using _Workspace.Jordan.Script.Managers;
using UnityEngine;

namespace _Workspace.Jordan.Script.Lobby
{
    public class GameManager: MonoBehaviour {
        
        [SerializeField] private List<PlayerController> _players = new List<PlayerController>();
        [SerializeField] private UIPlayerPanel[] _uiPlayerPanels;
        public static  GameManager Instance;

        private void Awake() {
            Instance = this;
        }
        
        public void SetNewPlayer(PlayerController player) {
            _uiPlayerPanels[player.PlayerIndex].SetupUI(player);
            _uiPlayerPanels[player.PlayerIndex].gameObject.SetActive(true);
            _players.Add(player);
        }
    }
}