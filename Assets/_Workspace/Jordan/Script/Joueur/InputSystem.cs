// using System;
// using UnityEngine;
// using UnityEngine.InputSystem;
//
// namespace _Workspace.Jordan.Script.Joueur
// {
//     public class InputSystem : MonoBehaviour
//     {
//         public static event Action<bool> OnInputDeviceChanged;
//
//         public float Attack;
//         public bool Interact;
//         public Vector2 Move;
//
//         private PlayerInput _inputSystem;
//         private PlayerController _playerControler;
//         private bool _isControllerConnected;
//
//
//         private void Awake()
//         {
//             _inputSystem = GetComponent<PlayerInput>();
//
//             if (_inputSystem == null) throw new NullReferenceException("PlayerInputManager is null");
//
//             _playerControler = GetComponent<PlayerController>();
//
//             if (_playerControler == null) throw new NullReferenceException("PlayerControler is null");
//         }
//
//         // [ContextMenu("Get Input Device")]
//         // private void DebugGamePadID()
//         // {
//         //     Debug.Log(" Player Input user Id  = " + _playerInput.user.id + "\n " + "PlayerInput player index" + _playerInput.playerIndex);
//         //
//         //     Debug.Log("Player Input has " + _playerInput.devices.Count + " devices");
//         //     
//         //     foreach (var devide in _playerInput.devices)
//         //     {
//         //         Debug.Log("Player Input device id" + devide.deviceId);
//         //     }
//         //
//         // }
//
//         private void OnEnable()
//         {
//             UnityEngine.InputSystem.InputSystem.onDeviceChange += OnDeviceChange;
//
//             _inputSystem.actions["Move"].performed += OnMove;
//             _inputSystem.actions["Move"].canceled += OnMove;
//
//             _inputSystem.actions["Dash"].performed += OnDash;
//             _inputSystem.actions["Dash"].canceled += OnDash;
//
//             // _playerInput.actions["Attack"].performed += OnAttack;
//             // _playerInput.actions["Attack"].canceled += OnAttack;
//             //
//             // _playerInput.actions["Interaction"].performed += OnInteract;
//             // _playerInput.actions["Interaction"].canceled += OnInteract;
//
//             DetectCurrentInputDevice();
//         }
//
//         private void OnDisable()
//         {
//             UnityEngine.InputSystem.InputSystem.onDeviceChange -= OnDeviceChange;
//
//             _inputSystem.actions["Move"].performed -= OnMove;
//             _inputSystem.actions["Move"].canceled -= OnMove;
//
//             _inputSystem.actions["Dash"].performed -= OnDash;
//             _inputSystem.actions["Dash"].canceled -= OnDash;
//
//             // _playerInput.actions["Attack"].performed -= OnAttack;
//             // _playerInput.actions["Attack"].canceled -= OnAttack;
//             //
//             // _playerInput.actions["Interaction"].performed -= OnInteract;
//             // _playerInput.actions["Interaction"].canceled -= OnInteract;
//         }
//
//         private void OnDeviceChange(InputDevice device, InputDeviceChange change)
//         {
//             if (change == InputDeviceChange.Added || change == InputDeviceChange.Removed)
//             {
//                 DetectCurrentInputDevice();
//             }
//         }
//
//         private void DetectCurrentInputDevice()
//         {
//             _isControllerConnected = Gamepad.all.Count > 0;
//             OnInputDeviceChanged?.Invoke(_isControllerConnected);
//
//             Debug.Log(_isControllerConnected
//                 ? "Controller connected: Switching to Gamepad controls."
//                 : "No controller connected: Switching to Keyboard/Mouse controls.");
//         }
//
//         private void OnMove(InputAction.CallbackContext context)
//         {
//             _playerControler.OnMove(context.ReadValue<Vector2>());
//         }
//
//         private void OnDash(InputAction.CallbackContext context)
//         {
//             
//         }
//
//         // private void OnAttack(InputAction.CallbackContext context)
//         // {
//         //     _playerControler.OnAttack(context.ReadValue<float>());
//         // }
//         //
//         // private void OnInteract(InputAction.CallbackContext context)
//         // {
//         //     _playerControler.OnInteract(context.ReadValue<float>());
//         // }
//
//         public void SwitchCurrentControlScheme(Gamepad gamepad)
//         {
//             if (_inputSystem != null)
//             {
//                 if (gamepad != null)
//                 {
//                     _inputSystem.SwitchCurrentControlScheme(gamepad);
//                     Debug.Log("Contrôleur associé : " + gamepad.displayName);
//                 }
//                 else
//                 {
//                     Debug.LogError("Le Gamepad fourni est nul. Impossible de changer de schéma de contrôle.");
//                 }
//             }
//             else
//             {
//                 Debug.LogError("PlayerInput est nul. Impossible de changer de schéma de contrôle.");
//             }
//         }
//     }
// }
