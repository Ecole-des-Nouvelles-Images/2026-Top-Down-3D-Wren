using System.Collections;
using System.Collections.Generic;
using _Workspace.Jordan.Script.Joueur;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Workspace.Jordan.Script.ennemi.Priest
{
    public class PriestAttack : MonoBehaviour
    {
        [SerializeField] private PlayerLife _playerLife;
        [SerializeField] private float _damage;
        
        private float _time;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
               other.GetComponent<PlayerLife>().TakeDamage(_damage);
            }
        }
    }
}