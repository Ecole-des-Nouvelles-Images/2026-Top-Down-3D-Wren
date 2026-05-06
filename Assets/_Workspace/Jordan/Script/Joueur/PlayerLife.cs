using UnityEngine;
using UnityEngine.Serialization;

namespace _Workspace.Jordan.Script.Joueur
{
    public class PlayerLife : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;
        
        public Healthbar HealthBar;

        private int _currentHealth;

        public void Awake()
        {
            _currentHealth = _maxHealth; 
            HealthBar.SetMaxHealth(_maxHealth);
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            
            if (_currentHealth >= 0)
            {
            }
            if (_currentHealth <= 0)
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
