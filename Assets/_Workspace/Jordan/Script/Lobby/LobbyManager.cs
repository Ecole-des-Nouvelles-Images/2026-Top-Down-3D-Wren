using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Workspace.Jordan.Script.Lobby
{
    /// <summary>
    /// LobbyManager gère l'enregistrement des joueurs et persiste entre les scènes.
    /// C'est un singleton persistant qui survit au changement de scène.
    /// </summary>
    public class LobbyManager : MonoBehaviourSingletonPersistent<LobbyManager>
    {
        [SerializeField] private int _maxPlayers = 4;
        private List<PlayerInputData> _registeredPlayers = new();
        private Dictionary<InputDevice, int> _deviceToPlayerIndex = new();

        // Callbacks pour notifier les écouteurs
        public event Action<PlayerInputData> OnPlayerJoined;
        public event Action<int> OnPlayerLeft;
        public event Action OnLobbyFull;
        public event Action OnLobbyUpdated;

        public IReadOnlyList<PlayerInputData> RegisteredPlayers => _registeredPlayers.AsReadOnly();
        public int PlayerCount => _registeredPlayers.Count;
        public bool IsLobbyFull => _registeredPlayers.Count >= _maxPlayers;

        // Singleton persistant
        public override void Awake()
        {
            base.Awake();
            
            // Si c'est une nouvelle instance
            if (Instance == this)
            {
                DontDestroyOnLoad(gameObject);
                Debug.Log("[LobbyManager] Initialized and persisted across scenes");
            }
        }

        /// <summary>
        /// Enregistre un joueur avec son device et son schéma d'input.
        /// Retourne true si succès, false si échec (lobby plein, device déjà utilisé, etc).
        /// </summary>
        public bool RegisterPlayer(InputDevice device, string controlScheme)
        {
            // Vérifier si le device est déjà utilisé
            if (_deviceToPlayerIndex.ContainsKey(device))
            {
                Debug.LogWarning($"[LobbyManager] Device {device.displayName} is already registered by another player");
                return false;
            }

            // Vérifier si le lobby est plein
            if (IsLobbyFull)
            {
                Debug.LogWarning($"[LobbyManager] Lobby is full ({_maxPlayers} players max)");
                OnLobbyFull?.Invoke();
                return false;
            }

            // Créer les données du joueur
            int playerIndex = _registeredPlayers.Count;
            var playerData = new PlayerInputData(playerIndex, device, controlScheme);

            // Ajouter à la liste et au dictionnaire
            _registeredPlayers.Add(playerData);
            _deviceToPlayerIndex[device] = playerIndex;

            Debug.Log($"[LobbyManager] Player registered: {playerData}");
            OnPlayerJoined?.Invoke(playerData);
            OnLobbyUpdated?.Invoke();

            return true;
        }

        /// <summary>
        /// Retire un joueur du lobby par son device.
        /// </summary>
        public bool UnregisterPlayer(InputDevice device)
        {
            if (!_deviceToPlayerIndex.TryGetValue(device, out int playerIndex))
            {
                Debug.LogWarning($"[LobbyManager] Device {device.displayName} not found in lobby");
                return false;
            }

            var playerData = _registeredPlayers[playerIndex];
            _registeredPlayers.Remove(playerData);
            _deviceToPlayerIndex.Remove(device);

            // Réajuster les indices des autres joueurs
            for (int i = playerIndex; i < _registeredPlayers.Count; i++)
            {
                _registeredPlayers[i].PlayerIndex = i;
            }

            Debug.Log($"[LobbyManager] Player unregistered: {playerData}");
            OnPlayerLeft?.Invoke(playerIndex);
            OnLobbyUpdated?.Invoke();

            return true;
        }

        /// <summary>
        /// Marque un joueur comme prêt.
        /// </summary>
        public void SetPlayerReady(int playerIndex, bool isReady)
        {
            if (playerIndex >= 0 && playerIndex < _registeredPlayers.Count)
            {
                _registeredPlayers[playerIndex].IsReady = isReady;
                Debug.Log($"[LobbyManager] Player {playerIndex + 1} is ready: {isReady}");
                OnLobbyUpdated?.Invoke();
            }
        }

        /// <summary>
        /// Retourne true si tous les joueurs sont prêts.
        /// </summary>
        public bool AreAllPlayersReady()
        {
            if (_registeredPlayers.Count == 0) return false;

            foreach (var player in _registeredPlayers)
            {
                if (!player.IsReady) return false;
            }

            return true;
        }

        /// <summary>
        /// Réinitialialise le lobby (pour recommencer une partie).
        /// </summary>
        public void ClearLobby()
        {
            _registeredPlayers.Clear();
            _deviceToPlayerIndex.Clear();
            Debug.Log("[LobbyManager] Lobby cleared");
            OnLobbyUpdated?.Invoke();
        }

        /// <summary>
        /// Récupère les données d'un joueur par son index.
        /// </summary>
        public PlayerInputData GetPlayer(int playerIndex)
        {
            if (playerIndex >= 0 && playerIndex < _registeredPlayers.Count)
            {
                return _registeredPlayers[playerIndex];
            }

            return null;
        }

        /// <summary>
        /// Affiche l'état actuel du lobby dans la console.
        /// </summary>
        public void DebugLobbyState()
        {
            Debug.Log($"=== LOBBY STATE ===");
            Debug.Log($"Players: {_registeredPlayers.Count}/{_maxPlayers}");
            Debug.Log($"Is Full: {IsLobbyFull}");
            for (int i = 0; i < _registeredPlayers.Count; i++)
            {
                Debug.Log($"  {_registeredPlayers[i]}");
            }
        }
    }
}

