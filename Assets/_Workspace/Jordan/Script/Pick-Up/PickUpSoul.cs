using _Workspace.Jordan.Script.Joueur;
using UnityEngine;

namespace _Workspace.Jordan.Script.Pick_Up
{
    public class PickUpSoul : MonoBehaviour
    {
        public Soul Soul;
        private PlayerController _playerController;
    
        public void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //DoVFX();
                Destroy(gameObject);
                _playerController.AddItemToInventory(Soul,1);
                Debug.Log("Pick Up Soul");
            }
        }
    }
}
