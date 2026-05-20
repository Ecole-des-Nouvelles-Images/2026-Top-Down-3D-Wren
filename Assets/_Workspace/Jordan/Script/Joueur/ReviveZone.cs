using System.Collections.Generic;
using UnityEngine;

namespace _Workspace.Jordan.Script.Joueur
{
    public class ReviveZone : MonoBehaviour
    {
        List<PlayerController> playersInZone = new List<PlayerController>();
        private void OnTriggerEnter(Collider other)
        {
                if(other.CompareTag("Player"))
                {
                    PlayerController player = other.GetComponent<PlayerController>();

                    playersInZone.Add(player);
                }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                PlayerController player = other.GetComponent<PlayerController>();

                playersInZone.Remove(player);
            }
        }
    }
}