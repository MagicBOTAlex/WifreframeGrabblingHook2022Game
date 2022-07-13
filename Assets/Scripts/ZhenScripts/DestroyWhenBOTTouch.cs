using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenBOTTouch : MonoBehaviour
{
    public static DestroyWhenBOTTouch thereCanOnlyBeOne { get; private set; }

    public float TimeoutDestroy;

    private void Start()
    {
        StartCoroutine(DestroySoon());

        thereCanOnlyBeOne = this;
    }

    private void Update()
    {
        if (thereCanOnlyBeOne != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BOTs"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroySoon()
    {
        yield return new WaitForSeconds(TimeoutDestroy);

        Destroy(gameObject);
    }
}
