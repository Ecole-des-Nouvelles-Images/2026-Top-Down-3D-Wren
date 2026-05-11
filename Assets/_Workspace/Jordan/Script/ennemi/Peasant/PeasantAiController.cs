using _Workspace.Jordan.Script.Joueur;
using UnityEngine;
using UnityEngine.AI;

namespace _Workspace.Jordan.Script.ennemi.Peasant
{
    [RequireComponent(typeof(PeasantSo))]
    public class AIController : MonoBehaviour
    {
        [SerializeField] private PeasantSo _peasantSo;
        [SerializeField] private Transform _player;
       
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
            _peasantSo.Cooldown += Time.deltaTime; 
            
            if (_player == null) return;

            float distance = Vector3.Distance(transform.position, _agent.destination);

            if (distance <= _peasantSo.AttackRange)
            {
                Attack();
            }
        }

        private void Attack()
        {
            if (_peasantSo.Cooldown >= _peasantSo.NextAttackTime)
            {
                _peasantSo.CanAttack = true;
                _peasantSo.Cooldown = 0f;
                Debug.Log(_peasantSo.Cooldown);
                Debug.Log("a attaqué");
            }
            
            // dégâts
            if (_peasantSo.CanAttack)
            {
                _playerLife.TakeDamage(_peasantSo.Damage);
                _peasantSo.CanAttack = false;
            }
        }
    }
}