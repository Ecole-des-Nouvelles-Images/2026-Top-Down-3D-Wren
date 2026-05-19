using System;
using System.Collections.Generic;
using _Workspace.Jordan.Script.Pick_Up;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Workspace.Jordan.Script.Joueur
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _dashSpeed;
        [SerializeField] private float _dashDuration;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private GameObject _hitBox;
        
        [Header("Visual")]
        [SerializeField] private Transform _visual;
        [SerializeField] private float _rotationSpeed = 12f;
        
        private float _time;
        private float _attackTimer;
        
        private readonly Dictionary<Item, int> _inventory = new();
        private Vector2 _move;
        private CharacterController _controller;
        private bool _isControllerConnected; 
        public bool IsDashing;
        private float _dashTimer;
        private Vector3 _dashDirection;
        private float _verticalVelocity;
        private float _inputDeadZone = 0.1f;
        private Animator _animator;
        
        public Dictionary<Item, int> Inventory => _inventory;
        
        // Gravité
        private float _gravity = -9.81f;
        private float _groundedForce = -2f;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
            _visual = _animator.transform;
        }

        private void Update()
        {
            Vector3 move = new Vector3(_move.x, 0, _move.y);
            
            _attackTimer += Time.deltaTime;

            HandleGravity();

            if (IsDashing)
            {
                Vector3 dashMove = _dashDirection * _dashSpeed;
                dashMove.y = _verticalVelocity;

                RotateVisual(_dashDirection);
                
                _controller.Move(dashMove * Time.deltaTime);

                _dashTimer -= Time.deltaTime;
                if (_dashTimer <= 0f)
                {
                    IsDashing = false;
                }
                UpdateAnimation(_dashDirection.magnitude);
                return;
            }
            HandleMovement(move);
            UpdateAnimation(move.magnitude);
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
                RotateVisual(move);
            }
            
            Vector3 velocity = move * _moveSpeed;

            Vector3 finalMove = new Vector3(velocity.x, _verticalVelocity, velocity.z);
            
            _controller.Move(finalMove * Time.deltaTime);
        }
        
        private void RotateVisual(Vector3 direction)
        {
            if (_visual == null || direction.sqrMagnitude < 0.001f) return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _visual.rotation = Quaternion.Slerp(_visual.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        private void UpdateAnimation(float inputMagnitude)
        {
            if (_animator == null) return;

            float speed = inputMagnitude < _inputDeadZone ? 0f : 1f;

            _animator.SetFloat("Speed", speed);
            _animator.SetBool("IsDashing", IsDashing);
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

            IsDashing = true;
            _dashTimer = _dashDuration;

            _dashDirection = new Vector3(_move.x, 0, _move.y).normalized;
        }

        private void OnAttack()
        {
            int attackIndex = UnityEngine.Random.Range(0, 2);
            
                if (_attackTimer < _attackCooldown) return;

                _attackTimer = 0;

                _animator.SetInteger("AttackIndex", attackIndex);
                _animator.SetTrigger("Attack");
                
                _hitBox.SetActive(true);
        }

        public bool HasItemInInventory(Item item, int number)
        {
            if (_inventory.TryGetValue(item, out var value))
            {
                return value >= number;
            }
            
            return false;
        }
        
        public void AddItemToInventory(Item item, int number)
        {
            if (_inventory.ContainsKey(item))
            {
                _inventory[item]++;
            }
            else
            {
                _inventory.Add(item, number);
            }
        }

        public void RemoveItemFromInventory(Item item, int number)
        {
            if (_inventory.ContainsKey(item))
            {
                _inventory[item]--;
            }
            else
            {
                throw new Exception("Item not found");
            }
        }
    }
}