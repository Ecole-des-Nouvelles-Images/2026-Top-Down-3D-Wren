using System.Diagnostics;
using System.Linq.Expressions;
using _Workspace.Jordan.Script.Pick_Up;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace _Workspace.Jordan.Script.Joueur
{
    public class PlayerLife : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Animator _animator;
        [SerializeField] private Item ReviveItem;
        
        public int MaxHealth;
        public Image Healthbar;
        public float CurrentHealth;
        
        public void Awake()
        {
            _animator = GetComponent<Animator>();
            CurrentHealth = MaxHealth; 
        }
        
        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;
            Healthbar.fillAmount = CurrentHealth / MaxHealth;
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
