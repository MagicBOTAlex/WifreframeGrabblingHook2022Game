using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookGunHandler : MonoBehaviour
{
    public float LookingSpeed = 2f;

    private GameObject MainCamera;
    private Vector3 TargetPos;
    private Quaternion StartingRotation;

    private void Start()
    {
        // stores the non changing values
        MainCamera = GameManager.Instance.Camera;
        StartingRotation = transform.rotation;
    }

    private void Update()
    {
        // casts a ray from the main camera and checks if it hits.
        // if the ray hits then it will check if the object that was hit has a "GrabblebleObject" tag
        if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out RaycastHit HitInfo, 100.0f)
            && HitInfo.collider.CompareTag("GrabblebleObject"))
        {
            //Debug.DrawLine(MainCamera.transform.position, HitInfo.point);   // draws a debug line that shows the ray when hitting an object
            TargetPos = HitInfo.transform.position;                         // stores the position of the object that was hit
        }
        else
        {
            // if the ray didn't hit or didn't hit a correct object, then sets the TargetPos to (0, 0, 0)
            TargetPos = Vector3.zero;
        }

        // stores these values to a more readable variable name
        Quaternion targetRotation = Quaternion.LookRotation(transform.position - TargetPos);
        Quaternion currentRotation = transform.rotation;

        // lerps the rotation of the HookGun to the correct ratation
        transform.rotation = Quaternion.Lerp(
            currentRotation, 
            (TargetPos == Vector3.zero) ? StartingRotation : targetRotation, // check if there is a target. If target is zero then uses StartingRotation else uses targetRotation
            Time.deltaTime * LookingSpeed);

    }
}
