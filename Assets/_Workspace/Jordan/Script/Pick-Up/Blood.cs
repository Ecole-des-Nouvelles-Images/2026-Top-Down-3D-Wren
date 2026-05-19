using _Workspace.Jordan.Script.Joueur;
using UnityEngine;

namespace _Workspace.Jordan.Script.Pick_Up
{
    [CreateAssetMenu(menuName =  "Power-Up/Blood")]
    public class Blood : Item
    {
        [SerializeField] private PlayerLife _playerLife;
        public float _amount;
        
        public void Apply(PlayerLife playerLife)
        {
            playerLife.CurrentHealth += _amount;
            playerLife.Healthbar.fillAmount = playerLife.CurrentHealth / _playerLife.MaxHealth;
        }
    }
}
