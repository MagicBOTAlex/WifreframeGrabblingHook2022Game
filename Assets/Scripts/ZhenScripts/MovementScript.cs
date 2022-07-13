using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public CharacterController controller;

    public float MoveSpeed;
    public float jumpForce;
    public float gravity;

    private Vector3 Velosity;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    public bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        #region isGrounded checker
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        #endregion

        #region Gravity maker
        if (isGrounded && Velosity.y < 0)
        {
            Velosity.y = -2;
        }
        #endregion

        #region Jumping handler
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Velosity.y += jumpForce;
        }
        #endregion

        #region Moving part of the script
        float X = Input.GetAxis("Horizontal");
        float Z = Input.GetAxis("Vertical");

        Vector3 Move = transform.right * X + transform.forward * Z;

        controller.Move(Move * MoveSpeed * Time.deltaTime);

        Velosity.y -= gravity * 10 * Time.deltaTime;
        controller.Move(Velosity * Time.deltaTime);
        #endregion

        #region Crouch control
        if (Input.GetKey(KeyCode.LeftControl))
        {

        }
        #endregion
    }
}
