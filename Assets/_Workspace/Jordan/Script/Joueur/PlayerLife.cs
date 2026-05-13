using System.Diagnostics;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace _Workspace.Jordan.Script.Joueur
{
    public class PlayerLife : MonoBehaviour
    {
        [SerializeField] PlayerSo _playerSo;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Animator _animator;
        
        public Image Healthbar;
        public float CurrentHealth;
        
        public void Awake()
        {
            _animator = GetComponent<Animator>();
            CurrentHealth = _playerSo.MaxHealth; 
        }
        
        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;
            Healthbar.fillAmount = CurrentHealth / _playerSo.MaxHealth;
            //_animator.SetTrigger("Hit");
            
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
            _playerController.enabled = false;
            
            //_animator.SetBool("Dead", true);
            Debug.Log("est mort");
        }
    }
}
