using UnityEngine.InputSystem;

namespace _Workspace.Jordan.Script.Lobby
{
    /// <summary>
    /// Stocke les données d'input d'un joueur (device utilisé, schéma de contrôle, index).
    /// </summary>
    public class PlayerInputData
    {
        public int PlayerIndex { get; set; }
        public InputDevice Device { get; set; }
        public string ControlScheme { get; set; } // "WASD", "Arrows", "Gamepad"
        public bool IsReady { get; set; }

        public PlayerInputData(int playerIndex, InputDevice device, string controlScheme)
        {
            PlayerIndex = playerIndex;
            Device = device;
            ControlScheme = controlScheme;
            IsReady = false;
        }

        public override string ToString()
        {
            return $"Player {PlayerIndex + 1} | Device: {Device.displayName} | Scheme: {ControlScheme} | Ready: {IsReady}";
        }
    }
}

