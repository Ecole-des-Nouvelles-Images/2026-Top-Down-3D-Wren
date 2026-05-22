using System.Collections.Generic;
using _Workspace.Jordan.Script.Joueur;
using UnityEngine;


namespace _Workspace.Jordan.Script.ennemi
{
    public class EnemyLife : MonoBehaviour
    {
        [SerializeField] private float _health;
        
        private Healthbar _healthBar;
        private float _currentHealth;
        private Animator _animator;
        
        public static readonly List<EnemyLife> EnemyLives = new();
        
        private void Awake()
        {
            //_animator = GetComponent<Animator>();
            _currentHealth = _health;
        }
        
        private void OnEnable()
        {
            EnemyLives.Add(this);
        }

        private void OnDisable()
        {
            EnemyLives.Remove(this);
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
            WaveManager.Instance.EnemyKilled();
            Destroy(gameObject);
        }
    }
}
