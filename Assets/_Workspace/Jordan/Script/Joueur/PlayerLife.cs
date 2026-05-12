using UnityEngine;
using UnityEngine.Serialization;

namespace _Workspace.Jordan.Script.Joueur
{
    public class PlayerLife : MonoBehaviour
    {
        [SerializeField] PlayerSo _playerSo;
        [SerializeField] private Animator _animator;
        
        public Healthbar HealthBar;
        
        // pour séparer la vie des joueurs du SO
        public float CurrentHealth;
        
        public void Awake()
        {
            _animator = GetComponent<Animator>();
            CurrentHealth = _playerSo.MaxHealth; 
        }
        
        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;
            _animator.SetTrigger("Hit");
            
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
            _animator.SetBool("Dead", true);
            Debug.Log("est mort");
        }
    }
}
