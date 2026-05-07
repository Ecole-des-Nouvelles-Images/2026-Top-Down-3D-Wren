using _Workspace.Jordan.Script.Joueur;
using UnityEngine;


namespace _Workspace.Jordan.Script.ennemi
{
    public class EnnemyLife : MonoBehaviour
    {
        [SerializeField] private float _health = 100f;
        
        public Healthbar HealthBar;
        
        private float _currentHealth;
        
        private void Awake()
        {
            _currentHealth = _health;
        }
        
        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;

            Debug.Log("Enemy Hit");

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log("Enemy Dead");

            Destroy(gameObject);
        }
    }
}
