using UnityEngine;
using UnityEngine.Serialization;

namespace _Workspace.Jordan.Script.ennemi
{
    [CreateAssetMenu(fileName = "PriestSo", menuName = "Scriptable Objects/PriestSo")]
    public class PriestSo : ScriptableObject
    {
        [Header("Move Settings")]
        public float MoveSpeed;
        
        [Header("Attack Settings")]
        public float Cooldown;
        public float Damage;
        public float NextAttackTime;
        public float DestroyLight;
    }
}
