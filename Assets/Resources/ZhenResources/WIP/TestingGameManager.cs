using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingGameManager : MonoBehaviour
{
    static public TestingGameManager instance;

    static public List<OutlineHandler> OutlinedableObjects = new List<OutlineHandler>();

    public Material OutlineMaterial;

    private void Start()
    {
        if (instance is null)
            instance = this;
        else
            Destroy(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            for (int i = 0; i < OutlinedableObjects.Count; i++)
            {
                OutlinedableObjects[i].ToWireframe();
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            for (int i = 0; i < OutlinedableObjects.Count; i++)
            {
                OutlinedableObjects[i].FromWireframe();
            }
        }
    }
}
