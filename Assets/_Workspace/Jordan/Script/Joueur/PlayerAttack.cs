using _Workspace.Jordan.Script.ennemi;
using UnityEngine;

namespace _Workspace.Jordan.Script.Joueur
{
    public class PlayerAttack : MonoBehaviour
    {
        [Header("Attack Settings")]
        [SerializeField] private float _damage;
        [SerializeField] private float _cooldown;
        
        [Header("Damage Upgrade")]
        [SerializeField] private int _damageLevel;

        private const int LevelMaxDamage = 4;
        
        private EnemyLife _enemyLife;
        private float _time;
        
        private void OnTriggerEnter(Collider ennemy)
        {
            if (ennemy.CompareTag("Ennemy"))
            {
                Debug.Log(ennemy.name + " j'ai collidé");

                EnemyLife enemyLife = ennemy.GetComponent<EnemyLife>();

                if (enemyLife != null)
                {
                    enemyLife.TakeDamage(_damage);
                }
            }
        }

        public void UpgradeDamage(float amount)
        {
            if (_damageLevel >= LevelMaxDamage) return;

            _damageLevel++;

            _damage += amount;

            Debug.Log("Damage upgraded : " + _damage);

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
