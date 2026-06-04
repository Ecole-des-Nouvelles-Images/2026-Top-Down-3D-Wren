using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance;

    [SerializeField] private GameObject _bloodVFX;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnBlood(Vector3 position, Quaternion rotation)
    {
        Instantiate(_bloodVFX, position, rotation);
    }
}