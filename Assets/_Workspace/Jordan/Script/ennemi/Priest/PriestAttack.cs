using System.Collections;
using System.Collections.Generic;
using _Workspace.Jordan.Script.Joueur;
using UnityEngine;

namespace _Workspace.Jordan.Script.ennemi.Priest
{
    public class PriestAttack : MonoBehaviour
    {
        [SerializeField] private PriestSo _priestSo;
        [SerializeField] private PlayerLife _playerLife;

        private float _time;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
               other.GetComponent<PlayerLife>().TakeDamage(_priestSo.Damage);
            }
        }
    }
}