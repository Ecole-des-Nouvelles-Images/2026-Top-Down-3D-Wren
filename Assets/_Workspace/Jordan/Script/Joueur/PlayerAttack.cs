using System;
using _Workspace.Jordan.Script.ennemi;
using UnityEngine;

namespace _Workspace.Jordan.Script.Joueur
{
    public class PlayerAttack : MonoBehaviour
    {
        [Header("Attack Settings")]
        [SerializeField] private float _damage;
        [SerializeField] private float _cooldown;
        
        private EnemyLife _enemyLife;
        private float _time;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ennemy"))
            {
                Debug.Log(other.name + " j'ai collidé");

                EnemyLife enemyLife = other.GetComponent<EnemyLife>();

                if (enemyLife != null)
                {
                    enemyLife.TakeDamage(_damage);
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
