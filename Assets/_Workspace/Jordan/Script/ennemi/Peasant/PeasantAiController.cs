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
        [SerializeField] private GameObject _slash;
        
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
                _agent.isStopped = true;
                Attack();
            }
            else
            { 
                _agent.isStopped = false; 
                _agent.SetDestination(_currentTarget.transform.position);
            }
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
                if (Time.time >= _lastAttackTime + _attackCooldown)
                {
                    _lastAttackTime = Time.time;

                    _animator.SetTrigger("Attack");

                    SpawnVfx(_slash, transform.position, transform.rotation);

                    SoundFXManager.Instance.PlaySoundFXClip(_attack, SoundGroups.Sfx);

                    _currentTarget.TakeDamage(_damage);
                }
        }
        
        private void SpawnVfx(GameObject vfxPrefab, Vector3 position, Quaternion rotation)
        {
            if (vfxPrefab == null)
                return;

            GameObject vfx = Instantiate(vfxPrefab, position, rotation);

            Destroy(vfx, 2f);
        }
    }
}