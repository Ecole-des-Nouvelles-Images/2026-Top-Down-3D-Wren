using System.Collections.Generic;
using _Workspace.Jordan.Script.AudioListener;
using _Workspace.Jordan.Script.Joueur;
using UnityEngine;

namespace _Workspace.Jordan.Script.ennemi
{
    public class EnemyLife : MonoBehaviour
    {
        [SerializeField] private float _health;
        [SerializeField] private AudioClip _hit;
        [SerializeField] private AudioClip _death;

        [SerializeField] private float _dropChance;
        [SerializeField] private GameObject _healPrefab;
        
        [SerializeField] private SkinnedMeshRenderer _renderer;
        [SerializeField] private Color _flashColor = Color.white;
        [SerializeField] private float _flashDuration = 0.08f;

        [SerializeField] private GameObject _bloodHitPrefab;
        [SerializeField] private Transform _bloodSpawn;
        
        private Material _material;
        private Color _originalColor;

        private float _currentHealth;
        private Animator _animator;
        private bool _isDead;

        private static readonly int HitTrigger = Animator.StringToHash("Hit");
        private static readonly int DeadBool = Animator.StringToHash("IsDead");

        public static readonly List<EnemyLife> EnemyLives = new();

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _currentHealth = _health;

            if (_renderer == null)
                _renderer = GetComponentInChildren<SkinnedMeshRenderer>();

            if (_renderer != null)
            {
                _material = _renderer.material;

                if (_material.HasProperty("_BaseColor"))
                {
                    _originalColor = _material.GetColor("_BaseColor");
                }
            }
        }

        private void OnEnable()
        {
            EnemyLives.Add(this);
        }

        private void OnDisable()
        {
            EnemyLives.Remove(this);
        }

        private void SpawnBloodHit()
        {
            if (_bloodHitPrefab == null) return;

            Transform spawnPoint = _bloodSpawn != null ? _bloodSpawn : transform;

            Instantiate(
                _bloodHitPrefab,
                spawnPoint.position,
                spawnPoint.rotation
            );
        }
        public void TakeDamage(float damage)
        {
            if (_isDead) return;

            _currentHealth -= damage;

            SpawnBloodHit();

            Flash();

            if (_currentHealth <= 0)
            {
                Die();
                return;
            }

            if (_animator != null)
            {
                _animator.ResetTrigger(HitTrigger);
                _animator.SetTrigger(HitTrigger);
            }

            if (_hit != null)
            {
                SoundFXManager.Instance.PlaySoundFXClip(_hit, SoundGroups.Sfx);
            }

            Debug.Log("Enemy Hit");
        }

        private void Die()
        {
            if (_isDead) return;

            _isDead = true;
            Debug.Log("Enemy Dead");

            TryDropHeal();

            if (_animator != null)
            {
                // Empêche le HitEffect de se relancer pendant la mort
                _animator.ResetTrigger(HitTrigger);

                // Coupe le layer HitEffect
               
                if (_animator.layerCount > 1)
                {
                    _animator.SetLayerWeight(1, 0f);
                }

                // Active le bool de mort
                _animator.SetBool(DeadBool, true);
                
            }

            if (_death != null)
            {
                SoundFXManager.Instance.PlaySoundFXClip(_death, SoundGroups.Sfx);
            }

            WaveManager.Instance.EnemyKilled();

            Destroy(gameObject, 2f);
        }
        private void Flash()
        {
            if (_material == null) return;

            _material.SetColor("_BaseColor", _flashColor);

            CancelInvoke(nameof(ResetFlash));
            Invoke(nameof(ResetFlash), _flashDuration);
        }

        private void ResetFlash()
        {
            if (_material == null) return;

            _material.SetColor("_BaseColor", _originalColor);
        }
        private void TryDropHeal()
        {
            if (_healPrefab == null) return;

            if (Random.value <= _dropChance)
            {
                Instantiate(_healPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}