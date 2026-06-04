using System.Collections;
using System.Collections.Generic;
using _Workspace.Jordan.Script.AudioListener;
using _Workspace.Jordan.Script.Joueur;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace _Workspace.Jordan.Script.ennemi.Priest
{
    public class PriestAIController : MonoBehaviour
    {
        private Transform _player;
        [SerializeField] private PriestAttack _priestAttack;
        [SerializeField] private GameObject _lightPrefab;
        [SerializeField] private GameObject _warningPrefab;
        
        [Header("Attack Settings")]
        [SerializeField] private float _castTime = 2f;
        private bool _isCasting;
        [SerializeField] private float _nextAttackTime;
        [SerializeField] private float _warningTime;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _destroyLight;
        [SerializeField] private float _lifetime;
        [SerializeField] private AudioClip _cast;
        
        private Animator _animator;
        private NavMeshAgent  _agent;
        
        public bool IsCasting => _isCasting;
        
        private void Start()
        { 
            _animator = GetComponentInChildren<Animator>(); 
            _agent = GetComponent<NavMeshAgent>();
            
            var playerController = FindObjectOfType<PlayerController>();
            if (playerController != null) _player = playerController.transform;
            else
                Debug.LogError("Aucun joueur trouvé !");
        }

        private void Update()
        {
                if (_player == null || _isCasting)
                    return;

                _cooldown += Time.deltaTime;

                if (_cooldown >= _nextAttackTime)
                {
                    _cooldown = 0f;
                    CastLight();
                }
        }
        
        // permet de choisir un joueur au hasard et d'invoquer la lumiere
        private void CastLight()
        {
            if (PlayerController.PlayersControllers.Count == 0) return;

            PlayerController target = PlayerController.PlayersControllers[Random.Range(0, PlayerController.PlayersControllers.Count)];

            if (target == null) return;

            if (_cast != null)
            {
                SoundFXManager.Instance.PlaySoundFXClip(_cast, SoundGroups.Sfx);
            }
            StartCoroutine(UseRoutine(target.transform.position));
        }
        
        // permet d'instancier un gameobject pendant x temps puis de le detruire ( en gros mettre pause au timer avant destruction)
        private IEnumerator UseRoutine(Vector3 pos)
        {
            
            _isCasting = true;
            _agent.isStopped = true;

            _animator.SetTrigger("Attack");

            GameObject warning = SpawnVfx(_warningPrefab, pos);

            yield return new WaitForSeconds(_castTime);

            Destroy(warning);

            GameObject light = SpawnVfx(_lightPrefab, pos);

            yield return new WaitForSeconds(_destroyLight);

            Destroy(light);

            _agent.isStopped = false;
            _isCasting = false;

            _animator.SetTrigger("Walk");
            
        }
        private GameObject SpawnVfx(GameObject vfxPrefab, Vector3 position)
        {
            if (vfxPrefab == null)
                return null;

            GameObject vfx = Instantiate(vfxPrefab, position, Quaternion.identity);
            Destroy(vfx, _lifetime);

            return vfx;
        }
    }
}
