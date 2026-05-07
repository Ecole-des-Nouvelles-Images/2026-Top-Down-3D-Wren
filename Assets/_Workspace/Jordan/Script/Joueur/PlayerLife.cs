using UnityEngine;

namespace _Workspace.Jordan.Script.Joueur
{
    public class PlayerLife : MonoBehaviour
    {
        [SerializeField] PlayerSo _playerSo;
        
        public Healthbar HealthBar;

        private float _currentHealth;

        public void Awake()
        {
            _currentHealth = _playerSo.MaxHealth; 
        }
        
        public void TakeDamage(float damage)
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
