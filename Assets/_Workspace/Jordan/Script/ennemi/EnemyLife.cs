using System.Collections.Generic;
using _Workspace.Jordan.Script.AudioListener;
using _Workspace.Jordan.Script.Joueur;
using UnityEngine;


namespace _Workspace.Jordan.Script.ennemi
{
    public class EnemyLife : MonoBehaviour
    {
        [SerializeField] private float _health;
        [SerializeField] private AudioClip _hit;
        [SerializeField] private AudioClip _death;
        
        [SerializeField] private float _dropChance;
        [SerializeField] private GameObject _healPrefab;
        
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
            if (_hit != null)
            {
                SoundFXManager.Instance.PlaySoundFXClip(_hit, SoundGroups.Sfx);
            }
           
            Debug.Log("Enemy Hit");

            if (_currentHealth <= 0)
            {
                TryDropHeal();
                Debug.Log("drop heal activé");
                Die();
            }
        }

        private void Die()
        {
            Debug.Log("Enemy Dead");
            //_animator.SetBool("Dead", true);
            if (_death != null)
            {
                SoundFXManager.Instance.PlaySoundFXClip(_death, SoundGroups.Sfx);
            }
            WaveManager.Instance.EnemyKilled();
            Destroy(gameObject);
        }
        
        void TryDropHeal()
        {
            float randomValue = Random.value; // nombre entre 0 et 1

            if (randomValue <= _dropChance)
            {
                Instantiate(_healPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
