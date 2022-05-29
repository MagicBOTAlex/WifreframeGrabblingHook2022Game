using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Player;
    public GameObject Camera;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance is null) Instance = this; else Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
