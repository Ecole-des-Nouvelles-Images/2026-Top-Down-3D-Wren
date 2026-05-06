using System;
using UnityEngine;

namespace _Workspace.Jordan.Script.Joueur
{
    public class Player : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}