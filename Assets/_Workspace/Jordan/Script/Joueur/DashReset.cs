using UnityEngine;
using UnityEngine.UI;

namespace _Workspace.Jordan.Script.Joueur
{
    public class DashReset : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Image _image;
        
        private void Update()
        {
            if (_playerController == null) return;
            
            _image.fillAmount = _playerController.DashCooldownTimer / _playerController.DashCooldown;
        }
        
        public void Init(PlayerController player)
        { 
            _playerController = player;
        }
    }
}
