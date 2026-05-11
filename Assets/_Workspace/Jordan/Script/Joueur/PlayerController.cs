using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace _Workspace.Jordan.Script.Joueur
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private PlayerSo _playerSo;
        [SerializeField] private GameObject _hitBox;
        
        private Vector2 _move;
        private CharacterController _controller;
        private bool _isControllerConnected;

        private bool _isDashing;
        private float _dashTimer;
        private Vector3 _dashDirection;
        private float _verticalVelocity;
        private float _inputDeadZone = 0.1f;
        private float _gravity = -9.81f;
        private float _groundedForce = -2f;

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
                Vector3 dashMove = _dashDirection * _playerSo.DashSpeed;
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
            
            Vector3 velocity = move * _playerSo.MoveSpeed;

            Vector3 finalMove = new Vector3(
                velocity.x,
                _verticalVelocity,
                velocity.z
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
        
        private void OnMove(InputValue value)
        {
            _move = value.Get<Vector2>();
        }
        
        private void OnSprint()
        {
            if (_move.magnitude < _inputDeadZone) return;

            _isDashing = true;
            _dashTimer = _playerSo.DashDuration;

            _dashDirection = new Vector3(_move.x, 0, _move.y).normalized;
        }

        private void OnAttack()
        {
            _hitBox.SetActive(true);
        }
    }
}