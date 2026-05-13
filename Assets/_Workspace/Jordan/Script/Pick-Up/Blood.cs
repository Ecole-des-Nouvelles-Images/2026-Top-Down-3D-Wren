using _Workspace.Jordan.Script.Joueur;
using UnityEngine;

namespace _Workspace.Jordan.Script.Pick_Up
{
    [CreateAssetMenu(menuName =  "Power-Up/Blood")]
    public class Blood : ScriptableObject
    {
        // [SerializeField] private PlayerLife _playerlife;
        public PlayerSo _playerSo;
        public float _amount;
        
        public void Apply(PlayerLife playerLife)
        {
            playerLife.CurrentHealth += _amount;
            playerLife.Healthbar.fillAmount = playerLife.CurrentHealth / _playerSo.MaxHealth;
        }
    }
}
