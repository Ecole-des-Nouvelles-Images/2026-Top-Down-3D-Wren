using UnityEngine;
using UnityEngine.InputSystem;

namespace _Workspace.Jordan.Script.Derniere_chance
{
    public class CharacterSpawner : MonoBehaviour
    {
        public GameObject[] playerPrefabs;
        

        public void OnPlayerJoined(PlayerInput playerInput)
        {
            int index = playerInput.playerIndex;

            if (index >= playerPrefabs.Length)
                return;

            Debug.Log($"Joueur {index} -> {playerPrefabs[index].name}");

            InputDevice device = playerInput.devices[0];
            Vector3 pos = playerInput.transform.position;

           // Destroy(playerInput.gameObject);

            //PlayerInput newPlayer = PlayerInput.Instantiate(playerPrefabs[index], playerIndex: index, pairWithDevice: device);

            //newPlayer.transform.position = pos;
        }
    }
}