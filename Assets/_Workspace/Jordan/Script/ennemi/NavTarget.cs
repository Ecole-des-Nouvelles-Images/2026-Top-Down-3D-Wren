using UnityEngine;
using UnityEngine.AI;

namespace _Workspace.Jordan.Script.ennemi
{
    public class NavTarget : MonoBehaviour
    {
        [SerializeField] private Transform _target; 
        [SerializeField] private float _stopDistance = 1.5f;
        
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
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
