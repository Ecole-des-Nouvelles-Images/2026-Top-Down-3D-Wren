using _Workspace.Jordan.Script.ennemi.Priest;
using UnityEngine;
using UnityEngine.AI;

namespace _Workspace.Jordan.Script.ennemi
{
    public class NavTarget : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _stopDistance;
        [SerializeField] private float _moveSpeed;

        private NavMeshAgent _agent;
        private PriestAIController _priest;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _priest = GetComponent<PriestAIController>();

            _target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            if (_target == null) return;

            
            if (_priest != null && _priest.IsCasting)
            {
                _agent.isStopped = true;
                return;
            }

            _agent.isStopped = false;
            _agent.speed = _moveSpeed;

            float distance = Vector3.Distance(transform.position, _target.position);

            if (distance <= _stopDistance)
            {
                _agent.isStopped = true;
                return;
            }

            _agent.SetDestination(_target.position);
        }
    }
}