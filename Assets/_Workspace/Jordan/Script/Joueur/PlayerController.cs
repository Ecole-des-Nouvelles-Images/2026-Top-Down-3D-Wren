using Mono.Cecil;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Workspace.Jordan
{
    public class PlayerController : MonoBehaviour
    {
        [Header("References")] 
        
        [Header("Settings")] 
        [SerializeField] private LayerMask _layerMask;

        [Header("Movement")]
        [SerializeField] private Vector2 _move;
        [SerializeField] private Vector2 _look;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        
        [SerializeField] private float _dash;

        private bool _isDashing;

        private PlayerInput _pI;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _pI = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            Vector3 move = new Vector3(_move.x, 0, _move.y);
            Vector3 look = new Vector3(_look.x, 0, _look.y);

            // Movement
            _rb.linearVelocity = new Vector3(_move.x, 0, _move.y) * (_moveSpeed * Time.deltaTime);
        }

        public void OnMove(InputValue value)
        {
            _move = value.Get<Vector2>();
        }

        public void OnSprint()
        {
            Debug.Log("Sprinting");
            _rb.AddForce(new Vector3(_move.x * _dash,0,_move.y * _dash));
        }
    }
}