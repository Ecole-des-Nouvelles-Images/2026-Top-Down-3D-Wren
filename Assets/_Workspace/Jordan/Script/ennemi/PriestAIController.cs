using _Workspace.Jordan.Script.Joueur;
using UnityEngine;
using UnityEngine.AI;

namespace _Workspace.Jordan.Script.ennemi
{
    [RequireComponent(typeof(PriestSo))]
    public class PriestAIController : MonoBehaviour
    {
        [SerializeField] private PeasantSo _priestSo;
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
            _priestSo.Cooldown += Time.deltaTime; 
            
            if (_player == null) return;

            float distance = Vector3.Distance(transform.position, _agent.destination);

            if (distance <= _priestSo.AttackRange)
            {
                Attack();
            }
        }

        private void Attack()
        {
            if (_priestSo.Cooldown >= _priestSo.NextAttackTime)
            {
                _priestSo.CanAttack = true;
                _priestSo.Cooldown = 0f;
                Debug.Log(_priestSo.Cooldown);
                Debug.Log("a attaqué");
            }
            
            // dégâts
            if (_priestSo.CanAttack)
            {
                _playerLife.TakeDamage(_priestSo.Damage);
                _priestSo.CanAttack = false;
            }
        }
    }
}
