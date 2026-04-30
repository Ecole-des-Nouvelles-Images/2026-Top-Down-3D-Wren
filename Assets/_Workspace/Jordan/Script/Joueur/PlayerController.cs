using UnityEngine;
using UnityEngine.InputSystem;

namespace _Workspace.Jordan.Script.Joueur
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _dash;

        private bool _isDashing;
        private Vector2 _move;
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

            // Movement
            _rb.linearVelocity = new Vector3(_move.x, 0, _move.y) * (_moveSpeed * Time.deltaTime);
        }

        public void OnMove(InputValue value)
        {
            _move = value.Get<Vector2>();
        }

        public void OnSprint()
        {
            _rb.AddForce(new Vector3(_move.x * _dash,0,_move.y * _dash));
        }

        public void OnAttack(InputValue value)
        {
            
        }
    }
}