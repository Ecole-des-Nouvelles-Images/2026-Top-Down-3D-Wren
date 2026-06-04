using _Workspace.Jordan.Script.ennemi.Peasant;
using UnityEngine;

namespace _Workspace.Jordan.Script.ennemi
{
    public class AnimationEventAIRouter : MonoBehaviour
    {
        private PeasantAIController _peasantAIController;

        private void Start()
        {
            _peasantAIController = GetComponentInParent<PeasantAIController>();
        }
        
        // public void OnActiveHit()
        // {
        //     _peasantAIController.OnActiveHit();
        // }
    }
}
