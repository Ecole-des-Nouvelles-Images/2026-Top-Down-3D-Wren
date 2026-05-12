using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Workspace.Jordan.Script.ennemi
{
    public class PriestAttack : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _pLayers;
        [SerializeField] private PriestSo _priestSo;
        [SerializeField] private GameObject _lightPrefab;
        [SerializeField] private GameObject _telegraphPrefab;
        [SerializeField] private float _telegraphTime;

        private float _time;

        private void Update()
        {
            _time += Time.deltaTime;

            if (_time >= _priestSo.Cooldown)
            {
                CastLight();
                _time = 0f;
            }
        }

        private void CastLight()
        {
            if (_pLayers == null || _pLayers.Count == 0) return;

            GameObject target = _pLayers[Random.Range(0, _pLayers.Count)];
            Vector3 pos = target.transform.position;

            // sert a mettre un temps d'attente car unity fais tout en meme temps 
            StartCoroutine(UseRoutine(pos));
        }

        private IEnumerator UseRoutine(Vector3 pos)
        {
            GameObject warning = Instantiate(_telegraphPrefab, pos, Quaternion.identity);
            
            yield return new WaitForSeconds(_telegraphTime);
            
            Destroy(warning);
            Instantiate(_lightPrefab, pos, Quaternion.identity);
        }
    }
}