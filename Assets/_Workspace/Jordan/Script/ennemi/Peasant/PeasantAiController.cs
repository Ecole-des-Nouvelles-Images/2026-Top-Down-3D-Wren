using _Workspace.Jordan.Script.Joueur;
using UnityEngine;
using UnityEngine.AI;

namespace _Workspace.Jordan.Script.ennemi.Peasant
{
    public class PeasantAIController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float _moveSpeed = 3.5f;

        [Header("Player")]
        [SerializeField] private Transform _player;

        [Header("Attack Settings")]
        [SerializeField] private float _attackRange = 2f;
        [SerializeField] private float _cooldown = 1f;
        [SerializeField] private float _damage = 10f;

        private float _nextAttackTime;

        private PlayerLife _playerLife;
        private Animator _animator;
        private NavMeshAgent _agent;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();

            // Vérifie si le joueur est assigné
            if (_player != null)
            {
                _playerLife = _player.GetComponent<PlayerLife>();
            }

            // Configure la vitesse du NavMeshAgent
            if (_agent != null)
            {
                _agent.speed = _moveSpeed;
            }
        }

        private void Update()
        {
            // Sécurité anti-erreur
            if (_player == null) return;
            if (_agent == null) return;

            // L'ennemi suit le joueur
            _agent.SetDestination(_player.position);

            // Distance réelle avec le joueur
            float distance = Vector3.Distance(transform.position, _player.position);

            // Si proche → attaque
            if (distance <= _attackRange)
            {
                Attack();
            }
        }

        private void Attack()
        {
            // Vérifie que le script de vie existe
            if (_playerLife == null) return;

            // Cooldown
            if (Time.time >= _nextAttackTime)
            {
                _nextAttackTime = Time.time + _cooldown;

                // Stop le déplacement pendant l'attaque
                _agent.ResetPath();

                // Animation
                UpdateAnimation();

                // Dégâts
                _playerLife.TakeDamage(_damage);

                Debug.Log("L'ennemi a attaqué !");
            }
        }

        private void UpdateAnimation()
        {
            if (_animator == null) return;

            _animator.SetTrigger("Attack");
        }

        // Dessine la portée d'attaque dans la scène Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }
    }
}