using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Workspace.Jordan.Script.Joueur
{
    public class PlayerVisual : MonoBehaviour 
    {
        public Animator Animator;
        public List<TrailRenderer> TrailRenderer;
        public event EventHandler OnAnticipation;
        public event EventHandler OnActiveStarting;
        public event EventHandler OnActiveHitting;
        public event EventHandler OnRecoverStarting;
        public event EventHandler OnRecoverEnding;
        public event EventHandler OnEnableHitboxing;
        public event EventHandler OnDisableHitboxing;
        
        
        public void OnAnticipationStart()
        {
            OnAnticipation?.Invoke(this, EventArgs.Empty);
        }
        
        public void OnActiveStart()
        {
            OnActiveStarting?.Invoke(this, EventArgs.Empty);
        }

        public void OnActiveHit()
        {
            OnActiveHitting?.Invoke(this, EventArgs.Empty);
        }
        
        public void OnRecoveryStart()
        {
            OnRecoverStarting?.Invoke(this, EventArgs.Empty);
        }
        
        public void OnRecoveryEnd()
        {
            OnRecoverEnding?.Invoke(this, EventArgs.Empty);
        }

        public void EnableHitbox()
        {
            OnEnableHitboxing?.Invoke(this, EventArgs.Empty);
        }

        public void DisableHitbox()
        {
            OnDisableHitboxing?.Invoke(this, EventArgs.Empty);
        }
    }
}