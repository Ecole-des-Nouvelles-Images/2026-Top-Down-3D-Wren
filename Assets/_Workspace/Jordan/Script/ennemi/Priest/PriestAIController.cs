using System.Collections;
using System.Collections.Generic;
using _Workspace.Jordan.Script.Joueur;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace _Workspace.Jordan.Script.ennemi.Priest
{
    [RequireComponent(typeof(PriestSo))]
    public class PriestAIController : MonoBehaviour
    {
        [SerializeField] private PriestSo _priestSo;
        [SerializeField] private Transform _player;
        [SerializeField] private PriestAttack _priestAttack;
        [SerializeField] private GameObject _lightPrefab;
        [SerializeField] private GameObject _warningPrefab;
        [SerializeField] private float _warningTime;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _destroyLight;
        [SerializeField] private List<GameObject> _pLayers;
       
        private PlayerLife _playerLife;
        private Animator _animator;
        private NavMeshAgent  _agent;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            _playerLife = _player.gameObject.GetComponent<PlayerLife>();
        }

        private void Update()
        {
            if (_player == null) return;

            _cooldown += Time.deltaTime;

            if (_cooldown >= _priestSo.NextAttackTime)
            {
                _cooldown = 0f;
                CastLight();
            }
        }
        // permet de choisir un joueur au hasard et d'invoquer la lumiere
        private void CastLight()
        {
            if (_pLayers == null || _pLayers.Count == 0) return;

            _animator.SetTrigger("Attack");
            GameObject target = _pLayers[Random.Range(0, _pLayers.Count)];
            Vector3 pos = target.transform.position;

            StartCoroutine(UseRoutine(pos));
        }
        
        // permet d'instancier un gameobject pendant x temps puis de le detruire ( en gros mettre pause au timer avant destruction)
        private IEnumerator UseRoutine(Vector3 pos)
        {
            GameObject warning = Instantiate(_warningPrefab, pos, Quaternion.identity);
            yield return new WaitForSeconds(_warningTime);
            Destroy(warning);
            
            GameObject light = Instantiate(_lightPrefab, pos, Quaternion.identity);
            yield return new WaitForSeconds(_destroyLight);
            Destroy(light);
        }
    }
}
