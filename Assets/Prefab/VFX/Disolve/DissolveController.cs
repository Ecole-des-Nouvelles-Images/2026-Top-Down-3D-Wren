using UnityEngine;
using System.Collections;

public class DissolveController : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMesh;

    private Material[] skinnedMaterials;

    public float dissolveRate = 0.0125f;
    public float refreshRate = 0.025f;

    void Start()
    {
        if (skinnedMesh != null)
            skinnedMaterials = skinnedMesh.materials;
    }

    void Update()
    {
        // Disparition
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DissolveOut());
        }

        // Apparition
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(DissolveIn());
        }
    }

    IEnumerator DissolveOut()
    {
        float counter = 0;

        while (counter < 1)
        {
            counter += dissolveRate;

            for (int i = 0; i < skinnedMaterials.Length; i++)
            {
                skinnedMaterials[i].SetFloat("_DissolveAmount", counter);
            }

            yield return new WaitForSeconds(refreshRate);
        }
    }

    IEnumerator DissolveIn()
    {
        float counter = 1;

        while (counter > 0)
        {
            counter -= dissolveRate;

            for (int i = 0; i < skinnedMaterials.Length; i++)
            {
                skinnedMaterials[i].SetFloat("_DissolveAmount", counter);
            }

            yield return new WaitForSeconds(refreshRate);
        }
    }
}