using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float DestroySeconds;
    public bool LetOtherDestroy;

    // Start is called before the first frame update
    void Start()
    {
        if (!LetOtherDestroy)
        {
            StartCoroutine(DestroyEfter(DestroySeconds));
        }
    }

    public IEnumerator DestroyEfter(float Seconds)
    {
        yield return new WaitForSeconds(Seconds);

        Destroy(gameObject);
    }
}
