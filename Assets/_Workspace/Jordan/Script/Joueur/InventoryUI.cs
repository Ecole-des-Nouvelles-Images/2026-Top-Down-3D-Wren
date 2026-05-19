using System;
using UnityEngine;

namespace _Workspace.Jordan.Script.Joueur
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        
        private void Update()
        {
            foreach (var keyValuePair in _playerController.Inventory)
            {

            }

            throw new NotImplementedException();
        }
    }
}