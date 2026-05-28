using UnityEngine;
using UnityEngine.UI;

namespace _Workspace.Jordan.Script.Joueur
{
    public class Healthbar : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Image _image;
        
        private void Update()
        {
            if (_playerController == null) return;
            _image.fillAmount = _playerController.CurrentHealth / _playerController.MaxHealth;
           
        } 
        
        public void Init(PlayerController player)
        { 
            _playerController = player;
        }
    }
}