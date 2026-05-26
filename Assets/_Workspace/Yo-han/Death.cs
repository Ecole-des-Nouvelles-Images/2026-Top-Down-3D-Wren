using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DissolvingController : MonoBehaviour
{public SkinnedMeshRenderer skinnedMeshRenderer;
    public float dissolveRate = 0.0125f;
    public float refreshRate = 0.025f;

    private Material[] skinnerMaterials;

    void Start()
    {
        if (skinnedMesh != null)
            skinnerMaterials = skinnedMesh.materials 
    }