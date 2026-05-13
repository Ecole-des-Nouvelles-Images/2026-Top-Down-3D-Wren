using UnityEngine;

namespace _Workspace.Jordan.Script.Joueur
{
    [CreateAssetMenu(fileName = "PlayerSO", menuName = "Scriptable Objects/PlayerSO")]
    public class PlayerSo : ScriptableObject
    {
        [Header("Move Settings")]
        public float MoveSpeed;
        
        [Header("Dash Settings")]
        public float DashSpeed;
        public float DashDuration;
        
        [Header("Attack Settings")]
        public float AttackCooldown = 0.5f;
        
        [Header("Life Settings")]
        public int MaxHealth;
    }
}