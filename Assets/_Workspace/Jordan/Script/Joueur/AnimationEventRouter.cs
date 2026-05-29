using UnityEngine;

namespace _Workspace.Jordan.Script.Joueur
{
    public class AnimationEventRouter : MonoBehaviour
    {
        private PlayerController _playerController;

        private void Start()
        {
            _playerController = GetComponentInParent<PlayerController>();
        }
        
        // public void OnAnticipationStart()
        // {
        //     _playerController.OnAnticipationStart();
        // }
        //
        // public void OnActiveStart()
        // {
        //     _playerController.OnActiveStart();
        // }
        //
        // public void OnActiveHit()
        // {
        //     _playerController.OnActiveHit();
        // }
        //
        // public void OnRecoveryStart() {
        //     _playerController.OnRecoveryStart();
        // }
        //
        // public void OnRecoveryEnd() {
        //     _playerController.OnRecoveryEnd();
        // }
    }
}
