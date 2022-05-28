using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody playerBody;
    private Camera camera;

    [Header("Character controller settings")]
    [SerializeField] private float horizontalSensitivity;
    [SerializeField] private float verticalSensitivity;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;

    private Vector3 movementInput;
    private Vector2 mouseInput;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody>();
        camera = Camera.main;
    }
    void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;
       Cursor.visible = false; 
    }

    private void Update() 
    {
        // Update input vectors
        movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        // Debug.Log(movementInput);
        HandleMovement();
        HandleCamera();
    }

    private void HandleMovement() 
    {
        // Apply movement in players local space
        Vector3 moveVector = movementInput * movementSpeed * Time.deltaTime;
        transform.Translate(moveVector);
    }

    private void HandleCamera()
    {
        float rotationX = camera.transform.rotation.eulerAngles.x + -mouseInput.y * verticalSensitivity * Time.deltaTime;
        float rotationY = camera.transform.rotation.eulerAngles.y + mouseInput.x * horizontalSensitivity * Time.deltaTime;
        camera.transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }
}
