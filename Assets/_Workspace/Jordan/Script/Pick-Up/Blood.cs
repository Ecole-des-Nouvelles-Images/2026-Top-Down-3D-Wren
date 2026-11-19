using _Workspace.Jordan.Script.Joueur;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Workspace.Jordan.Script.Pick_Up
{
    [CreateAssetMenu(menuName =  "Power-Up/Blood")]
    public class Blood : Item
    {
        public float _amount;
        
        public void Apply(PlayerController playerLife)
        {
            playerLife.Health(_amount);
        }
    }
}
