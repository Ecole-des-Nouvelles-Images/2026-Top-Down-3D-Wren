using _Workspace.Jordan.Script.Joueur;
using UnityEngine;


namespace _Workspace.Jordan.Script.ennemi
{
    public class EnnemyLife : MonoBehaviour
    {
        [SerializeField] private float _health = 100f;
        
        private Healthbar _healthBar;
        private float _currentHealth;
        private Animator _animator;
        
        private void Awake()
        {
            //_animator = GetComponent<Animator>();
            _currentHealth = _health;
        }
        
        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            //_animator.SetTrigger("Hit");
            Debug.Log("Enemy Hit");

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log("Enemy Dead");
            //_animator.SetBool("Dead", true);
            
            Destroy(gameObject);
        }
    }
}
