using UnityEngine;
using UnityEngine.UI;

namespace _Workspace.Jordan.Script.Joueur
{
    public class Healthbar : MonoBehaviour
    { 
        [SerializeField] PlayerController _playerController;
        [SerializeField] private Image _image;

        private void Update()
        {
            _image.fillAmount = _playerController.CurrentHealth / _playerController.MaxHealth;
        }
    }
}
