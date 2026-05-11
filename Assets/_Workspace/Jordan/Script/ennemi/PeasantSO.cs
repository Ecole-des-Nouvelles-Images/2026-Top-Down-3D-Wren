using UnityEngine;
using UnityEngine.Serialization;

namespace _Workspace.Jordan.Script.ennemi
{
    [CreateAssetMenu(fileName = "PeasantSo", menuName = "Scriptable Objects/PeasantSo")]
    public class PeasantSo : ScriptableObject
    {
        [Header("Attack Settings")]
        public float AttackRange;
        public float Cooldown;
        public float Damage;
        public float NextAttackTime;
        public bool CanAttack;
    }
}
