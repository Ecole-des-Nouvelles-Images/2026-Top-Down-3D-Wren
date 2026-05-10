using UnityEngine;
using UnityEngine.Serialization;

namespace _Workspace.Jordan.Script.Joueur
{
    public class PlayerLife : MonoBehaviour
    {
        [SerializeField] PlayerSo _playerSo;
        
        public Healthbar HealthBar;
        
        // pour séparer la vie des joueurs du SO
        public float CurrentHealth;
        
        public void Awake()
        {
            CurrentHealth = _playerSo.MaxHealth; 
        }
        
        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;
            
            if (CurrentHealth >= 0)
            {
            }
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }
        public void Die()
        {
            Debug.Log("est mort");
        }
    }
}
