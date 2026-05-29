using UnityEngine;

namespace _Workspace.Jordan.Script.Joueur
{
    public class EventAnimation : MonoBehaviour
    {
        private PlayerController _playerController;
        [SerializeField] private float _anticipationSpeed = 0.2f;
        [SerializeField] private float _activeSpeed = 1;
        [SerializeField] private float _recoverySpeed = 0.5f;
        public void OnAnticipationStart()
        {
            _playerController._animator.speed = _anticipationSpeed;
        }
        
        public void OnActiveStart()
        {
            _playerController._animator.speed = _activeSpeed;
        }

        public void OnActiveHit()
        {

        }

        public void OnRecoveryStart()
        {
            _playerController._animator.speed = _recoverySpeed;
        }

        public void OnRecoveryEnd()
        {
            _playerController._animator.speed = 1;
        }
    }
}
