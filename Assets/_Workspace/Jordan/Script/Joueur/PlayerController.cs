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
        [Header("References")]
        [SerializeField] private Transform _pivot;
        [SerializeField] private GameObject _hitBox;
        
        [Header("Settings")] 
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _dashSpeed;
        [SerializeField] private float _dashDuration;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private float _anticipationSpeed;
        [SerializeField] private float _activeSpeed;
        [SerializeField] private float _recoverySpeed;
        [SerializeField] private float _hitboxDuration;
        [SerializeField] private float _dashCooldown;
        [SerializeField] private AudioClip _attack;
        [SerializeField] private AudioClip _die;
        [SerializeField] private AudioClip _hit;
        
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
        private int _attackIndex;
        
        //Dash settings
        private bool _isDashing;
        private float _dashTimer;
        private float _dashCooldownTimer;
        private Vector3 _dashDirection;
        
        private readonly Dictionary<Item, int> _inventory = new();
        private Vector2 _move;
        private CharacterController _controller;
        private bool _isControllerConnected; 
        private float _verticalVelocity;
        private float _inputDeadZone = 0.1f;
        private Animator _animator;
        private Collider _playerLimits;
        private CinemachineTargetGroup _cinemachineTargetGroup;
        private float _hitboxTimer;
        
        public static readonly List<PlayerController> PlayersControllers = new();
        
        // Gravité
        private float _gravity = -9.81f;
        private float _groundedForce = -2f;
        
        private void Awake()
        {
            PlayersControllers.Add(this);
            _hitBox.SetActive(false);
            
            _dashCooldownTimer = _dashCooldown;
            
            _cinemachineTargetGroup = FindFirstObjectByType<CinemachineTargetGroup>();
            if (_cinemachineTargetGroup == null) throw new MissingComponentException("CinemachineTargetGroup not found");
            
            _controller = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
            _playerLimits = GameObject.FindGameObjectWithTag("PlayerLimits")?.GetComponent<Collider>();
           
            
            if (_playerLimits == null) throw new MissingComponentException("PlayerLimits not found");
            
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
            _dashCooldownTimer += Time.deltaTime;
            
            
            HandleGravity();
            
            if (_isDashing)
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
                    _isDashing = false;
                }
                UpdateAnimation(_dashDirection.magnitude);
                return;
            }
            
            if (_hitBox.activeSelf)
            {
                _hitboxTimer -= Time.deltaTime;

                if (_hitboxTimer <= 0f)
                {
                    _hitBox.SetActive(false);
                }
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
            if (_pivot == null || direction.sqrMagnitude < 0.001f) return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _pivot.rotation = Quaternion.Slerp(_pivot.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        private void UpdateAnimation(float inputMagnitude)
        {
            if (_animator == null) return;

            float speed = inputMagnitude < _inputDeadZone ? 0f : 1f;

            _animator.SetFloat("Speed", speed);
            _animator.SetBool("IsDashing", _isDashing);
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
            if (_isDashing) return;

            if (_move.magnitude < _inputDeadZone) return;

            if (_dashCooldownTimer < _dashCooldown) return;

            _dashCooldownTimer = 0f;

            _isDashing = true;
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

            _attackIndex++;
            if (_attackIndex >= 2) _attackIndex = 0;
            
            if (_attackTimer < _attackCooldown) return;

            _attackTimer = 0;

            _animator.SetInteger("AttackIndex", _attackIndex);
            _animator.SetTrigger("Attack");

            if (_attack != null)
            {
                SoundFXManager.Instance.PlaySoundFXClip(_attack, SoundGroups.Sfx);
            }
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

        public void OnAnticipationStart()
        {
            _animator.speed = _anticipationSpeed;
        }
        
        public void OnActiveStart()
        {
            _animator.speed = _activeSpeed;
        }
        
        public void OnActiveHit()
        { }
        
        public void OnRecoveryStart()
        {
            _animator.speed = _recoverySpeed;
        }
        
        public void OnRecoveryEnd()
        {
            _animator.speed = 1;
        }
        
        public void EnableHitbox()
        {
            _hitBox.SetActive(true);
            _hitboxTimer = _hitboxDuration;
        }
        
        public void DisableHitbox()
        {
            _hitBox.SetActive(false);
        }
    }
}