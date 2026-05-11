using UnityEngine;

namespace _Workspace.Jordan.Script.ennemi.Peasant
{
    [CreateAssetMenu(fileName = "PeasantSo", menuName = "Scriptable Objects/PeasantSo")]
    public class PeasantSo : ScriptableObject
    {
        [Header("Move Settings")]
        public float MoveSpeed;
        
        [Header("Attack Settings")]
        public float AttackRange;
        public float Cooldown;
        public float Damage;
        public float NextAttackTime;
        public bool CanAttack;
    }
}
