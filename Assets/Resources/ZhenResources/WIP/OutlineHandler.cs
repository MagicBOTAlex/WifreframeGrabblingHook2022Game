using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineHandler : MonoBehaviour
{
//#TODO: Add more options to outline materials

    public bool KeepAssignedMaterial = false;
    private Material startMat;
    private Renderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<Renderer>();
        startMat = meshRenderer.material;
        TestingGameManager.OutlinedableObjects.Add(this);
    }

    public void ToWireframe()
    {
        if (KeepAssignedMaterial)
            meshRenderer.sharedMaterials = new Material[] { startMat, TestingGameManager.instance.OutlineMaterial };
        else
            meshRenderer.sharedMaterial = TestingGameManager.instance.OutlineMaterial;
    }

    public void FromWireframe()
    {
        meshRenderer.sharedMaterials = new Material[] { startMat};
    }
}
