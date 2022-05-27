using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance is null) Instance = this; else Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
