using System.Collections;
using System.Collections.Generic;
using _Workspace.Jordan.Script.Joueur;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Workspace.Jordan.Script.ennemi.Priest
{
    public class PriestAttack : MonoBehaviour
    {
        [SerializeField] private float _damagePerSecond;

        private void OnTriggerStay(Collider other)
        {
            Debug.Log("Quelqu'un est dans la lumière");

            if (other.CompareTag("Player"))
            {
                Debug.Log("Le joueur prend des dégâts" +_damagePerSecond);

                PlayerLife playerLife = other.GetComponent<PlayerLife>();

                if (playerLife != null)
                {
                    playerLife.TakeDamage(_damagePerSecond * Time.deltaTime);
                }
            }
        }
    }
}