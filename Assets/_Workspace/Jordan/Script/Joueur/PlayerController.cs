using System;
using System.Collections.Generic;
using _Workspace.Jordan.Script.AudioListener;
using _Workspace.Jordan.Script.Pick_Up;
using Unity.Cinemachine;
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
        // [SerializeField] private float _anticipationSpeed = 0.2f;
        // [SerializeField] private float _activeSpeed = 1;
        // [SerializeField] private float _recoverySpeed = 0.5f;
        [SerializeField] private AudioClip _attack;
        [SerializeField] private AudioClip _die;
        [SerializeField] private AudioClip _hit;
        
        [Header("Visual")]
        [SerializeField] private Transform _visual;
        [SerializeField] private float _rotationSpeed = 12f;
        
        [Header("Health")]
        [SerializeField] private int _newLife; 
        public int MaxHealth;
        public float CurrentHealth;
        public bool IsDead;
        
        [Header("Revive")]
        [SerializeField] private Item _reviveItem;
        [SerializeField] private GameObject _circleRevive;
        [SerializeField] private ReviveZone _reviveZone;
        [SerializeField] private GameObject _canvas;
        
        //attack settings
        private float _time;
        private float _attackTimer;
        
        //Dash settings
        public bool IsDashing;
        private float _dashTimer;
        private Vector3 _dashDirection;
        
        private readonly Dictionary<Item, int> _inventory = new();
        private Vector2 _move;
        private CharacterController _controller;
        private bool _isControllerConnected; 
        private float _verticalVelocity;
        private float _inputDeadZone = 0.1f;
        public Animator _animator;
        private Collider _playerLimits;
        private CinemachineTargetGroup _cinemachineTargetGroup;
        private Healthbar _healthbar;
        
        public static readonly List<PlayerController> PlayersControllers = new();
        
        // Gravité
        private float _gravity = -9.81f;
        private float _groundedForce = -2f;
        
        private void Awake()
        {
            PlayersControllers.Add(this);
            
            _cinemachineTargetGroup = FindFirstObjectByType<CinemachineTargetGroup>();
            if (_cinemachineTargetGroup == null) throw new MissingComponentException("CinemachineTargetGroup not found");
            
            _controller = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
            _playerLimits = GameObject.FindGameObjectWithTag("PlayerLimits")?.GetComponent<Collider>();
            _healthbar = GameObject.FindGameObjectWithTag("Healthbar")?.GetComponent<Healthbar>();
            
            if (_playerLimits == null) throw new MissingComponentException("PlayerLimits not found");
            if (_healthbar == null) throw new MissingComponentException("Healthbar not found");
            
            _visual = _animator.transform;
            CurrentHealth = MaxHealth;
            
            IsDead = false;
            if (_circleRevive != null)
                _circleRevive.SetActive(false);
        }

        private void OnEnable()
        {
            _cinemachineTargetGroup.AddMember(transform, 1f, 1f);
        }

        private void OnDisable()
        {
            _cinemachineTargetGroup.RemoveMember(transform);
        }
        
        private void OnDestroy()
        {
            PlayersControllers.Remove(this);
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
                float dtDash = Time.deltaTime;
                Vector3 desiredDashPos = transform.position + dashMove * dtDash;
                Vector3 clampedDashPos = ClampToMovementBounds(desiredDashPos);
                Vector3 dashDelta = clampedDashPos - transform.position;
                _controller.Move(dashDelta);

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
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ReviveZone") && other.gameObject != _circleRevive.gameObject)
            { 
                _reviveZone = other.GetComponent<ReviveZone>();
                
                _canvas.SetActive(true);
                _canvas.transform.SetParent(null, true);
                _canvas.transform.position = other.transform.position;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("ReviveZone") && other.gameObject != _circleRevive.gameObject)
            { 
                _reviveZone = null;
                
                _canvas.SetActive(false);
                _canvas.transform.parent = transform;
            }
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
            
            float dt = Time.deltaTime;
            Vector3 desiredPos = transform.position + finalMove * dt;
            Vector3 clampedPos = ClampToMovementBounds(desiredPos);
            Vector3 moveDelta = clampedPos - transform.position;

            _controller.Move(moveDelta);
        }

        private Vector3 ClampToMovementBounds(Vector3 worldPos)
        {
            if (_playerLimits == null) return worldPos;

            Bounds b = _playerLimits.bounds;
            float x = Mathf.Clamp(worldPos.x, b.min.x, b.max.x);
            float z = Mathf.Clamp(worldPos.z, b.min.z, b.max.z);
            return new Vector3(x, worldPos.y, z);
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
            if (_reviveZone)
            {
                _reviveZone.RevivePlayer();
                _reviveZone = null;
                return;
            }
            
            int attackIndex = UnityEngine.Random.Range(0, 2);
            
                if (_attackTimer < _attackCooldown) return;

                _attackTimer = 0;

                _animator.SetInteger("AttackIndex", attackIndex);
                _animator.SetTrigger("Attack");

                if (_attack != null)
                {
                    SoundFXManager.Instance.PlaySoundFXClip(_attack, SoundGroups.Sfx);
                }
                
                _hitBox.SetActive(true);
        }

        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;
            if (_hit != null)
            {
                SoundFXManager.Instance.PlaySoundFXClip(_hit, SoundGroups.Sfx);
            }
            
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }
        
        public void SetReviveTarget(ReviveZone zone)
        {
            _reviveZone = zone;
        }

        public void ClearReviveTarget()
        {
            _reviveZone = null;
        }

        [ContextMenu("Die")]
        public void Die()
        {
            IsDead = true;
            enabled = false;
            
            if (_circleRevive != null)
                _circleRevive.SetActive(true);
            
            if (_die != null)
            {
                SoundFXManager.Instance.PlaySoundFXClip(_die, SoundGroups.Sfx);
            }
            
            Debug.Log( name + "est mort");
        }

        public void Revive()
        {
            if (!IsDead) return;
    
            IsDead = false;
            enabled = true;
            
            CurrentHealth = _newLife;
            
            if (_circleRevive != null)
                _circleRevive.SetActive(false);
        }

        // public bool HasItemInInventory(Item item, int number)
        // {
        //     {
        //       //  return value >= number;
        //     } 
        //     return false;
        // }
        //
        // public void AddItemToInventory(Item item, int number)
        // {
        //     if (_inventory.ContainsKey(item))
        //     {
        //         _inventory[item]++;
        //     }
        //     else
        //     {
        //
        //         _inventory.Add(item, number);
        //     }
        // }
        
        // public void RemoveItemFromInventory(Item item, int number)
        // {
        //     if (_inventory.ContainsKey(item))
        //     {
        //         _inventory[item]--;
        //     }
        //     else
        //     {
        //         throw new Exception("Item not found");
        //     }
        // }

        // public void OnAnticipationStart()
        // {
        //     _animator.speed = _anticipationSpeed;
        // }
        //
        // public void OnActiveStart()
        // {
        //     _animator.speed = _activeSpeed;
        // }
        //
        // public void OnActiveHit()
        // {
        //
        // }
        //
        // public void OnRecoveryStart()
        // {
        //     _animator.speed = _recoverySpeed;
        // }
        //
        // public void OnRecoveryEnd()
        // {
        //     _animator.speed = 1;
        // }
    }
}