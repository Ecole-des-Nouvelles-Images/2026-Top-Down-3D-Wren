using _Workspace.Jordan.Script.Joueur;
using UnityEngine;
using UnityEngine.AI;

namespace _Workspace.Jordan.Script.ennemi.Peasant
{
    public class PeasantAIController : MonoBehaviour
    {
        [Header("Attack Settings")]
        [SerializeField] private float _attackRange;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _damage;
        [SerializeField] private float _nextAttackTime;
        [SerializeField] private bool _canAttack;

        private PlayerController _currentTarget;
        private Animator _animator;
        private NavMeshAgent  _agent;
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        { 
            UpdateNearestTarget();
            
            _cooldown += Time.deltaTime; 
            
            if (_currentTarget == null) return;

            float distance = Vector3.Distance(transform.position, _agent.destination);

            if (distance <=  _attackRange)
            {
                _agent.isStopped = true;
                Attack();
            }

            _agent.isStopped = false;
        }

        private void UpdateNearestTarget()
        {
            float nearestDistance = Mathf.Infinity;
            foreach (PlayerController playerController in PlayerController.PlayersControllers)
            {
                if (Vector3.Distance(transform.position, playerController.transform.position) < nearestDistance)
                {
                    nearestDistance = Vector3.Distance(transform.position, playerController.transform.position);
                    _currentTarget = playerController;
                }
            }
        }
        
        private void UpdateWeakestTarget()
        {
            float nearestDistance = Mathf.Infinity;
            foreach (PlayerController playerController in PlayerController.PlayersControllers)
            {
                if (Vector3.Distance(transform.position, playerController.transform.position) < nearestDistance)
                {
                    nearestDistance = Vector3.Distance(transform.position, playerController.transform.position);
                    _currentTarget = playerController;
                }
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
                _currentTarget.TakeDamage(_damage);
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