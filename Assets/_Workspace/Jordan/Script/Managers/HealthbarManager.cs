using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Workspace.Jordan.Script.Joueur
{
    public class HealthbarManager : MonoBehaviour
    {
        [SerializeField] private Image[] _healthbarImages; // Array de 4 barres max
        private List<PlayerController> _players = new();

        private void Start()
        {
            // Récupérer tous les joueurs de la liste statique
            _players = new List<PlayerController>(PlayerController.PlayersControllers);
            
            // Lier chaque barre au joueur correspondant
            for (int i = 0; i < _players.Count && i < _healthbarImages.Length; i++)
            {
                if (_healthbarImages[i] != null)
                    Debug.Log($"[HealthbarManager] Barre {i} liée à {_players[i].gameObject.name}");
            }
        }

        private void Update()
        {
            // Mettre à jour chaque barre avec le joueur correspondant
            for (int i = 0; i < _players.Count && i < _healthbarImages.Length; i++)
            {
                var player = _players[i];
                var healthbar = _healthbarImages[i];
                
                if (player != null && healthbar != null)
                {
                    healthbar.fillAmount = player.CurrentHealth / player.MaxHealth;
                } 
            }
        }
    }
}