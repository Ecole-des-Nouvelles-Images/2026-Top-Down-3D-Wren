using _Workspace.Jordan.Script.Joueur;
using _Workspace.Jordan.Script.MenuPrincipale;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Workspace.Jordan.Script
{
    public class DefeatManagers : MonoBehaviour
    {
        private SceneChanger _sceneChanger;

        private bool _gameEnded;

        public void PlayerDied()
        {
            Debug.Log("PlayerDied appelé");

            int alivePlayers = 0;

            foreach (PlayerController player in PlayerController.PlayersControllers)
            {
                if (player != null && !player.IsDead)
                {
                    alivePlayers++;
                }
            }

            Debug.Log($"Joueurs vivants : {alivePlayers}");

            if (alivePlayers <= 0)
            {
                TriggerDefeat();
            }
        }

        public void TriggerDefeat()
        {
            _gameEnded = true;

            Debug.Log("Défaite : tous les joueurs sont morts");

            SceneManager.LoadScene("Game_Loose");
        }
    }
}