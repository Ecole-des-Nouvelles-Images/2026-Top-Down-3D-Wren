using UnityEngine;
using UnityEngine.InputSystem;

namespace _Workspace.Jordan.Script.Lobby
{
    /// <summary>
    /// Gère la détection des inputs pour rejoindre le lobby.
    /// Ce script détecte les touches appuyées et enregistre les joueurs.
    /// </summary>
    public class LobbyInputHandler : MonoBehaviour
    {
        private void Update()
        {
            // Vérifier si le clavier est disponible
            if (Keyboard.current == null) return;

            // ESPACE = Rejoindre avec WASD
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                bool success = LobbyManager.Instance.RegisterPlayer(Keyboard.current, "WASD");
                if (success)
                {
                    Debug.Log("[LobbyInput] Player joined with WASD");
                }
                else
                {
                    Debug.LogWarning("[LobbyInput] Failed to join with WASD (lobby full or device already used)");
                }
            }

            // CTRL DROIT = Rejoindre avec Flèches
            if (Keyboard.current.rightCtrlKey.wasPressedThisFrame)
            {
                bool success = LobbyManager.Instance.RegisterPlayer(Keyboard.current, "Arrows");
                if (success)
                {
                    Debug.Log("[LobbyInput] Player joined with Arrows");
                }
                else
                {
                    Debug.LogWarning("[LobbyInput] Failed to join with Arrows (lobby full or device already used)");
                }
            }

            // Pour chaque gamepad connecté
            foreach (var gamepad in Gamepad.all)
            {
                // BOUTON A (buttonSouth) = Rejoindre avec Gamepad
                if (gamepad.buttonSouth.wasPressedThisFrame)
                {
                    bool success = LobbyManager.Instance.RegisterPlayer(gamepad, "Gamepad");
                    if (success)
                    {
                        Debug.Log("[LobbyInput] Player joined with Gamepad");
                    }
                    else
                    {
                        Debug.LogWarning("[LobbyInput] Failed to join with Gamepad (lobby full or device already used)");
                    }
                }
            }
        }
    }
}

