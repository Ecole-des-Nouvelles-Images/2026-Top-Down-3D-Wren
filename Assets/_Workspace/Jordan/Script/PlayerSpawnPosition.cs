using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPosition : MonoBehaviour
{
    public static List<Transform> SpawnPositions = new List<Transform>();

    public static Vector3 GetSpawnPosition() {
        return SpawnPositions[Random.Range(0, SpawnPositions.Count)].position;
    }
    
    private void Awake() {
        SpawnPositions.Add(transform);
    }

    private void OnDestroy() {
        SpawnPositions.Remove(transform);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.4f);
    }
}