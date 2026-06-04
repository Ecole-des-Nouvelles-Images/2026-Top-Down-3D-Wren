using UnityEngine;

public class PlayerSpawnPointManagers : MonoBehaviour
{
    [SerializeField] private Vector3[] spawnPoint;
    public static PlayerSpawnPointManagers Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public Vector3 GetPlayersSpawnPoint()
    {
        return spawnPoint[Random.Range(0,spawnPoint.Length)];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var point in spawnPoint)
        {
            Gizmos.DrawSphere(point, 0.3f);
        }
    }
}