using _Workspace.Jordan.Script.AudioListener;
using _Workspace.Jordan.Script.Joueur;
using UnityEngine;
using UnityEngine.AI;

namespace _Workspace.Jordan.Script.ennemi.Peasant
{
    public class PeasantAIController : MonoBehaviour
    {
        [Header("Attack Settings")]
        [SerializeField] private float _attackRange;
        [SerializeField] private float _damage;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private AudioClip _attack;

        private PlayerController _currentTarget;
        private Animator _animator;
        private NavMeshAgent  _agent;
        private float _lastAttackTime;
        
        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        { 
                UpdateNearestTarget();

                if (_currentTarget == null) return;

                float distance = Vector3.Distance(transform.position, _currentTarget.transform.position);

                if (distance <= _attackRange)
                {
                    Debug.Log("Dans la range");
                    
                    _agent.isStopped = true;
                    Attack();
                }
                else
                {
                    Debug.Log("Hors range");
                    
                    _agent.isStopped = false;
                    _agent.SetDestination(_currentTarget.transform.position);
                }
        }

        private void UpdateNearestTarget()
        {
            Debug.Log(PlayerController.PlayersControllers.Count);
            
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
            Debug.Log("Attaque appeler");
            if (Time.time >= _lastAttackTime + _attackCooldown)
            {
                Debug.Log("Attaque lancer");
                _lastAttackTime = Time.time;

                _animator.SetTrigger("Attack");

                SoundFXManager.Instance.PlaySoundFXClip(_attack, SoundGroups.Sfx);
                _currentTarget.TakeDamage(_damage);

                Debug.Log("attaque");
            }
        }
    }
}