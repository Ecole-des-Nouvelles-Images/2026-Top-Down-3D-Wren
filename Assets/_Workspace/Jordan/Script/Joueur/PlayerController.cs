using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Workspace.Jordan.Script.Joueur
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        public static event Action<bool> OnInputDeviceChanged;
        
        [Header("Settings")] 
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _dashSpeed = 12f;
        [SerializeField] private float _dashDuration = 0.2f;
        [SerializeField] private float _inputDeadZone = 0.1f;

        [Header("Gravity")]
        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private float _groundedForce = -2f;

        private Vector2 _move;
        private CharacterController _controller;
        private bool _isControllerConnected;

        private bool _isDashing;
        private float _dashTimer;
        private Vector3 _dashDirection;

        private float _verticalVelocity;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Vector3 move = new Vector3(_move.x, 0, _move.y);

            HandleGravity();

            if (_isDashing)
            {
                Vector3 dashMove = _dashDirection * _dashSpeed;
                dashMove.y = _verticalVelocity;

                _controller.Move(dashMove * Time.deltaTime);

                _dashTimer -= Time.deltaTime;
                if (_dashTimer <= 0f)
                {
                    _isDashing = false;
                }
                return;
            }
            HandleMovement(move);
        }

        private void HandleMovement(Vector3 move)
        {
            if (move.magnitude < _inputDeadZone)
            {
                move = Vector3.zero;
            }
            else
            {
                move = move.normalized;
            }
            
            Vector3 horizontalVelocity = move * _moveSpeed;

            Vector3 finalMove = new Vector3(
                horizontalVelocity.x,
                _verticalVelocity,
                horizontalVelocity.z
            );

            _controller.Move(finalMove * Time.deltaTime);
        }

        private void HandleGravity()
        {
            if (_controller.isGrounded && _verticalVelocity < 0)
            {
                _verticalVelocity = _groundedForce;
            }
            else
            {
                _verticalVelocity += _gravity * Time.deltaTime;
            }
        }

        public void OnMove(InputValue value)
        {
            _move = value.Get<Vector2>();
        }
        
        public void OnSprint()
        {
            if (_move.magnitude < _inputDeadZone) return;

            _isDashing = true;
            _dashTimer = _dashDuration;

            _dashDirection = new Vector3(_move.x, 0, _move.y).normalized;
        }
    }
}