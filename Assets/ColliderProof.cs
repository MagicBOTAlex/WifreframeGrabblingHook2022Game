using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderProof : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collided with: {collision.gameObject.name}");
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log($"Collided with: {collision.gameObject.name}");
    }
}
