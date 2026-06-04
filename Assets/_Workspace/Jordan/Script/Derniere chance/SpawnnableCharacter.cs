using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnnableCharacter : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    [SerializeField] private PlayerInput playerInput;
    
    private void Start()
    {
        int index = playerInput.playerIndex;

        if (index >= playerPrefabs.Length)
            return;

        Debug.Log($"Joueur {index} -> {playerPrefabs[index].name}");

        InputDevice device = playerInput.devices[0];
        Debug.Log("Joueur " + device);
        Vector3 pos = PlayerSpawnPointManagers.Instance.GetPlayersSpawnPoint();
        
        PlayerInput newPlayer = PlayerInput.Instantiate(playerPrefabs[index], index, pairWithDevice: device);

        newPlayer.transform.position = pos;
        
        Destroy(gameObject);
    }
}