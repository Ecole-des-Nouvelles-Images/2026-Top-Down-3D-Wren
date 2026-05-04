using UnityEngine;
using UnityEngine.InputSystem;

namespace _Workspace.Jordan.Script.Joueur
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerControllerV2 : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _dashSpeed = 12f;
        [SerializeField] private float _dashDuration = 0.2f;

        private Vector2 _move;
        private PlayerInput _pI;
        private CharacterController _controller;

        private bool _isDashing;
        private float _dashTimer;
        private Vector3 _dashDirection;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _pI = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            Vector3 move = new Vector3(_move.x, 0, _move.y);

            if (_isDashing)
            {
                _controller.Move(_dashDirection * (_dashSpeed * Time.deltaTime));

                _dashTimer -= Time.deltaTime;
                if (_dashTimer <= 0f)
                {
                    _isDashing = false;
                }

                return;
            }

            // Mouvement normal
            _controller.Move(move * (_moveSpeed * Time.deltaTime));
        }

        public void OnMove(InputValue value)
        {
            _move = value.Get<Vector2>();
        }

        public void OnSprint()
        {
            if (_move == Vector2.zero) return;

            _isDashing = true;
            _dashTimer = _dashDuration;

            _dashDirection = new Vector3(_move.x, 0, _move.y).normalized;
        }

        public void OnAttack(InputValue value)
        {
            // TODO
        }
    }
}