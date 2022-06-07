using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HookGunHandler : MonoBehaviour
{
    public float LookingSpeed = 2f;
    public float CastRadius = 1f;
    public GameObject Hook;
    public GameObject HookStartObject;
    public float HookSpeed = 10f;
    public float HookBackSpeed = 10f;
    [Range(0, 500)]
    public float PullForce = 1f;

    private GameObject MainCamera;
    private GameObject Player;
    private CharacterHandler PlayerCH;
    private Rigidbody PlayerRB;
    private Vector3 TargetPos;
    private Quaternion StartingRotation;
    private Transform FrontHookOffset, BackHookOffset;
    Vector3 hookingPosition = new Vector3();

    private void Start()
    {
        // stores the non changing values
        MainCamera = GameManager.Instance.Camera;
        Player = GameManager.Instance.Player;
        PlayerCH = Player.GetComponent<CharacterHandler>();
        PlayerRB = Player.GetComponent<Rigidbody>();
        StartingRotation = transform.rotation;
        FrontHookOffset = Hook.transform.GetChild(0);
        BackHookOffset = Hook.transform.GetChild(1);

        // unparenting the hook so it's not affected by the player's movements
        Hook.transform.parent = null;
    }
    Vector3 maxPosition = new Vector3();
    private void Update()
    {
        #region Ratating the cannon

        if (!Input.GetButton("FireCannon"))
        {
            // casts a sphereCast from the main camera and checks if it hits.
            // if the ray hits then it will check if the object that was hit has a "GrabblebleObject" tag
            if (Physics.SphereCast(MainCamera.transform.position, CastRadius, MainCamera.transform.forward, out RaycastHit HitInfo, 100.0f)
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
        }

        // stores these values to a more readable variable name
        Quaternion normalRotation = GameManager.Instance.Camera.transform.rotation * StartingRotation;
        Quaternion targetRotation = Quaternion.LookRotation(transform.position - TargetPos);
        Quaternion currentRotation = transform.rotation;

        // lerps the rotation of the HookGun to the correct ratation
        transform.rotation = Quaternion.Lerp(
            currentRotation,
            (TargetPos == Vector3.zero) ? normalRotation : targetRotation, // check if there is a target. If target is zero then uses StartingRotation else uses targetRotation
            Time.deltaTime * LookingSpeed);
        #endregion

        #region Shooting part
        if (Input.GetButton("FireCannon") && TargetPos != Vector3.zero)
        {
            PlayerCH.SetGravity(false);

            if (hookingPosition == Vector3.zero)
            {
                // this shoots a ray through everything and it retreives the closest object from the ray origin
                //Debug.DrawRay(transform.position, transform.forward * -1);
                RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward * -1, 1000);
                if (hits.Where(x => x.collider.CompareTag("GrabblebleObject")).Count() > 0)
                {
                    #region spread out peice of code
                    var


          something






                                         =
                    hits

                                                        .

         Where
                            (


                 x


                                =>
                x.

                                      collider
                       .

                         CompareTag

                    (

             "GrabblebleObject"





   )
                            )
                        .

                                           Aggregate



                (
                                               (
                                x


                              ,

                               y


                              )
                                    =>

                    (


                               Vector3


                        .


                                           Distance(
                            y
                                .
                          point

                  ,
                           Hook

                                     .


                     transform

                                   .
                          position)

                                        <=



         Vector3
                            .

                                     Distance

                            (


                      x

                                        .


                           point

                                      ,


                              Hook

              .



                                      transform
                       .



                        position


                                        )


                          )

                            ?

                            y
                            :
                            x
                            )
                        ;
                    #endregion

                    hookingPosition = something.point + (Hook.transform.position - FrontHookOffset.position); // stores the surface point of the object that the player is trying to grab
                }
            }

            if (Hook.transform.parent == transform)
                Hook.transform.parent = null;

            if (Vector3.Distance(Hook.transform.position, hookingPosition) > 0.05f)
                Hook.transform.position = Vector3.Lerp(Hook.transform.position, hookingPosition, HookSpeed * Time.deltaTime);
            else
            {
                PlayerCH.AddForce((BackHookOffset.position - Player.transform.position).normalized * PullForce * Time.deltaTime);
            }
        }
        else
        {
            PlayerCH.SetGravity(true);

            hookingPosition = Vector3.zero;

            Vector3 backTarget = HookStartObject.transform.position + (Hook.transform.position - BackHookOffset.position);
            float dist = Vector3.Distance(transform.position, BackHookOffset.position);
            if (dist > 0.1f)
            {

                if (Hook.transform.parent == transform)
                    Hook.transform.parent = null;

                Hook.transform.rotation = transform.rotation;

                Hook.transform.position = Vector3.Lerp(Hook.transform.position, backTarget, GetBackSpeed());
            }
            else
            {
                if (Hook.transform.parent != transform)
                    Hook.transform.parent = transform;

                loopAmount = 0f;
            }
        }
        #endregion
    }

    float loopAmount = 0f;
    float GetBackSpeed()
    {
        loopAmount += (HookBackSpeed / 2) * Time.deltaTime;
        return loopAmount;
    }
}
