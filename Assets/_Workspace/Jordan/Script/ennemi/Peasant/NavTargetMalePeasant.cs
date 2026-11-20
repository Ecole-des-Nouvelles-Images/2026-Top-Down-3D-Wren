using UnityEngine;
using UnityEngine.AI;

namespace _Workspace.Jordan.Script.ennemi.Peasant
{
    public class NavTargetMalePeasant : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _stopDistance;
        [SerializeField] private float _moveSpeed;

        [Header("Visual")]
        [SerializeField] private Transform _visual;

        [Header("Rotation Offset")]
        [SerializeField] private float _rotationOffset = -90f;
        [SerializeField] private float _rotationSpeed = 8f;

        private NavMeshAgent _agent;
        private Animator _animator;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();
            
            if (_visual == null && _animator != null)
                _visual = _animator.transform;
            
            _agent.updateRotation = false;
        }

        private void Update()
        {
            if (_target == null) return;
            
            _agent.speed = _moveSpeed;

            float distance = Vector3.Distance(transform.position, _target.position);
            if (distance <= _stopDistance)
            {
                _agent.isStopped = true;
                _agent.ResetPath();

                if (_animator != null)
                    _animator.SetBool("Walk", false);
                return;
            }

            _agent.isStopped = false;
            _agent.SetDestination(_target.position);

            if (_animator != null)
                _animator.SetBool("Walk", true);
            RotateVisual();
        }

        private void RotateVisual()
        {
            if (_visual == null) return;

            Vector3 dir = _agent.destination - transform.position;
            Debug.DrawLine(_agent.destination, _target.position, Color.yellow);
            dir.y = 0;

            if (dir.sqrMagnitude < 0.01f)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(dir);

            // Offset pour corriger l'anim de marche de travers
            targetRotation *= Quaternion.Euler(0, _rotationOffset, 0);
            _visual.rotation = Quaternion.Slerp(_visual.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
        }
        public void SetNewTarget(Transform newTarget) {
            _target = newTarget;
            _agent.SetDestination(_target.position);
        }
    }
}
