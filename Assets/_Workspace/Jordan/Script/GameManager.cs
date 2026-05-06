using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace _Workspace.Jordan.Script
{
    public class GameManager: MonoBehaviour
    {
        // [SerializeField] private GameObject _playerPrefab;
        // [SerializeField] private Transform[] _spawnpoints;
        //
        // private bool _wasJoined = false;
        // private bool _arrowsJoined = false;
        // private bool _gamepadJoined = false;

        // void Update()
        // {
        //     if (Keyboard.current == null) return;
        //
        //     // Instancie un joueur des que WASD est pressée
        //     if (!_wasJoined && Keyboard.current.spaceKey.wasPressedThisFrame)
        //     {
        //         
        //         // var player = PlayerInput.Instantiate(_playerPrefab, controlScheme: "WASD", pairWithDevice: Keyboard.current);
        //         
        //         // if (_spawnpoints.Length > 0)
        //         // {
        //         //     player.transform.position = _spawnpoints[0].position;
        //         // }
        //         // _wasJoined = true;
        //     }
        //
        //     // Instancie un joueur avec les flèches dès que  ctrldroit est pressé
        //     if (!_arrowsJoined && Keyboard.current.rightCtrlKey.wasPressedThisFrame)
        //     {
        //         // var player = PlayerInput.Instantiate(_playerPrefab, controlScheme: "Arrows", pairWithDevice: Keyboard.current);
        //         //
        //         // if (_spawnpoints.Length > 1)
        //         // {
        //         //     player.transform.position = _spawnpoints[1].position;
        //         // }
        //         // _arrowsJoined = true;
        //     }
        //
        //     // Instancie un joueur avec le gamepad des que le bouton A est pressé
        //     foreach (var gamePad in Gamepad.all)
        //     {
        //         // if (gamePad.buttonSouth.wasPressedThisFrame && !_gamepadJoined)
        //         // {
        //         //     PlayerInput.Instantiate(_playerPrefab, controlScheme: "Gamepad", pairWithDevice: gamePad);
        //         //     
        //         //     _gamepadJoined = true; 
        //         // }
        //     }
        // }
        
        public Transform Spawnpoints1, Spawnpoints2, Spawnpoints3, Spawnpoints4;
        public GameObject J1, J2, J3, J4;
        
        public void SpawnPlayers()
        {
            Instantiate(J1, Spawnpoints1.position, Spawnpoints1.rotation);
            Instantiate(J2, Spawnpoints2.position, Spawnpoints2.rotation);
            Instantiate(J3, Spawnpoints3.position, Spawnpoints3.rotation);
            Instantiate(J4, Spawnpoints4.position, Spawnpoints4.rotation);
        }
    }
}