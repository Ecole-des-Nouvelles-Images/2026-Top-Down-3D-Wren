using _Workspace.Jordan.Script.ennemi.Peasant;
using _Workspace.Jordan.Script.ennemi.Priest;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace _Workspace.Jordan.Script.ennemi
{
    public class NavTarget : MonoBehaviour
    {
        [SerializeField] private Transform _target; 
        [SerializeField] private float _stopDistance;
        
        [SerializeField] private PriestSo _priestSo;
        
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            _agent.speed = _priestSo.MoveSpeed;
            float distance = Vector3.Distance(transform.position, _target.position);

            if (distance <= _stopDistance)
            {
                _agent.isStopped = true;
                _agent.ResetPath(); // évite les micro-corrections qui poussent le joueur
                return;
            }

            _agent.isStopped = false;
            _agent.SetDestination(_target.position);
        }
    }
}
