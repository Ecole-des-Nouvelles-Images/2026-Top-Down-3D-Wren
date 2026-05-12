using _Workspace.Jordan.Script.ennemi;
using UnityEngine;

namespace _Workspace.Jordan.Script.Joueur
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private PlayerSo _playerSo;
        
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
                    ennemyLife.TakeDamage(_playerSo.Damage);
                }
            }
        }

        private void Update()
        {
            _time += Time.deltaTime;

            if (_playerSo.Cooldown <= _time)
            {
                gameObject.SetActive(false);
                _time = 0;
            }
        }
    }
}
