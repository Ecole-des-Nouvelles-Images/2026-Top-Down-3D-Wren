using UnityEngine;

namespace _Workspace.Jordan.Script.ennemi.Priest
{
    [CreateAssetMenu(fileName = "PriestSo", menuName = "Scriptable Objects/PriestSo")]
    public class PriestSo : ScriptableObject
    {
        [Header("Move Settings")]
        public float MoveSpeed;

        [Header("Attack Settings")]
        public float Damage;
        public float NextAttackTime;
    }
}
