using _Workspace.Jordan.Script.ennemi;
using UnityEngine;

namespace _Workspace.Jordan.Script.Joueur
{
    public class PlayerAttack : MonoBehaviour
    {
        [Header("Attack Settings")]
        [SerializeField] private float _damage;
        [SerializeField] private float _cooldown;
        
        private EnnemyLife _ennemyLife;
        private float _time;
        
        private void OnTriggerEnter(Collider ennemy)
        {
            if (ennemy.CompareTag("Ennemy"))
            {
                Debug.Log(ennemy.name + " j'ai collidé");

                EnnemyLife ennemyLife = ennemy.GetComponent<EnnemyLife>();

                if (ennemyLife != null)
                {
                    ennemyLife.TakeDamage(_damage);
                }
            }
        }

        private void Update()
        {
            _time += Time.deltaTime;

            if (_cooldown <= _time)
            {
                gameObject.SetActive(false);
                _time = 0;
            }
        }
    }
}
