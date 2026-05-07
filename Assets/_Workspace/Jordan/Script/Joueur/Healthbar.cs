using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Workspace.Jordan.Script.Joueur
{
    public class Healthbar : MonoBehaviour
    { 
        [SerializeField] PlayerSo _playerSo;
        [SerializeField] private Image _image;
        
        private void Update()
        {
            _image.fillAmount = _playerSo.MaxHealth / _playerSo.CurrentHealth;
        }
    }
}
