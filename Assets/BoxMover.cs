using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMover : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public bool Reset = false;
    public bool Enable = false;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (Reset)
        {
            Reset = Enable = false;
            transform.position = startPos;
        }

        if (Enable)
        {
            transform.position = new Vector3(0, transform.position.y + MoveSpeed * Time.deltaTime, 0);
        }
    }
}
