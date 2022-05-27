using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPlatformGen : MonoBehaviour
{
    public GameObject DebugPrefab;
    public List<GameObject> SpawnedTiles = new List<GameObject>();
    public int Size = 20;

    void Start()
    {
        for (int x = -Size; x < Size; x++)
        {
            for (int z = -Size; z < Size; z++)
            {
                var spawnedObject = Instantiate(DebugPrefab, new Vector3(x, transform.position.y, z), Quaternion.identity);
                SpawnedTiles.Add(spawnedObject);
            }
        }
    }
}
