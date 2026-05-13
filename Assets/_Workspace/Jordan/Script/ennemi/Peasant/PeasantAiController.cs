using _Workspace.Jordan.Script.Joueur;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace _Workspace.Jordan.Script.ennemi.Peasant
{
    public class PeasantAIController : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        
        [Header("Attack Settings")]
        private float _attackRange;
        private float _cooldown;
        private float _damage;
        private float _nextAttackTime;
        private bool _canAttack;

        private PlayerLife _playerLife;
        private Animator _animator;
        private NavMeshAgent  _agent;
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            _playerLife = _player.gameObject.GetComponent<PlayerLife>();
        }

        private void Update()
        { 
            _cooldown += Time.deltaTime; 
            
            if (_player == null) return;

            float distance = Vector3.Distance(transform.position, _agent.destination);

            if (distance <=  _attackRange)
            {
                Attack();
            }
        }

        private void Attack()
        {
            if (_cooldown >= _nextAttackTime)
            {
                _canAttack = true;
                _cooldown = 0f;
                Debug.Log(_cooldown);
                Debug.Log("a attaqué");
            }
            
            // dégâts
            if (_canAttack)
            {
                UpdateAnimation();
                _playerLife.TakeDamage(_damage);
                _canAttack = false;
            }
        }
        private void UpdateAnimation()
        {
            if (_animator == null) return;
            
            _animator.SetTrigger("Attack");
        }
    }
}