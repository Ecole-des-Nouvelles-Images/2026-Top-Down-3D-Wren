using UnityEngine;

namespace _Workspace.Jordan.Script.ennemi
{
    [CreateAssetMenu(fileName = "PriestSo", menuName = "Scriptable Objects/PriestSo")]
    public class PriestSo : ScriptableObject
    {
        [Header("Attack Settings")]
        public float AttackRange;
        public float Cooldown;
        public float Damage;
        public float NextAttackTime;

        [Header("Attack Settings")]
        public float truc;
    }
}
