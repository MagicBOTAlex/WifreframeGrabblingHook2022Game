using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody m_PlayerBody;
    private Camera m_Camera;

    [Header("Character controller settings")]
    [SerializeField] private float m_MovementSpeed;
    [SerializeField] private float m_SprintSpeed;
    [SerializeField] private string m_SprintButtonName;
    [SerializeField] private float m_JumpForce;

    [Header("Camera settings")]
    [SerializeField] private float m_HorizontalSensitivity;
    [SerializeField] private float m_VerticalSensitivity;
    [SerializeField] private float m_MaxViewAngle;
    [SerializeField] private float m_MinViewAngle;

    private Vector3 m_MovementInput;
    private Vector2 m_MouseInput;
    private bool m_IsGrounded;
    private bool m_IsRunning;
    private bool m_IsWalking;
    private bool m_IsAirborne;
    private bool m_IsIdle;
    private bool m_LockCamera;
    private float m_CameraPitch;

    private void Awake()
    {
        m_PlayerBody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        m_Camera = GameManager.Instance.Camera.GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    private void Update() 
    {
        // Update input vectors
        m_MovementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        m_MovementInput.Normalize();
        m_MouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        m_IsGrounded = CheckGroundState();

        // Debug.Log(m_IsRunning);

        HandleState();
        HandleMovement();
        HandleRotation();
    }

    /// <summary>
    /// Handles all physical movement, such as walking, jumping etc.
    /// </summary>
    private void HandleMovement() 
    {
        // Vary speed if running
        float moveSpeed = m_MovementSpeed;
        if (m_IsRunning)
            moveSpeed = m_SprintSpeed;

        Debug.Log(moveSpeed);
        // Apply movement relative to players local axis
        Vector3 moveVector = m_MovementInput * moveSpeed * Time.deltaTime;
        transform.Translate(moveVector);

        HandleJump();
    }

    /// <summary>
    /// Handles x and y rotation of the character. It will rotate the player's mesh around it's local y-axis (yaw),
    /// and it will rotate the camera's local x-axis (pitch). It's all based on the input mousedelta vector in 'm_mouseInput'
    /// </summary>
    private void HandleRotation()
    {
        // Toggle if the camera should rotate, based on escape key
        if (Input.GetKeyDown(KeyCode.Escape))
            m_LockCamera = !m_LockCamera;

        // Toggle if the camera should rotate, when clicking in-game and if the camera's rotation is locked
        if (Input.GetButtonDown("Fire1") && m_LockCamera == true)
            m_LockCamera = !m_LockCamera;

        // Don't rotate if it shouldn't
        if (m_LockCamera)
            return;

        // Apply camera's local x rotation (vertical rotation), to camera
        m_CameraPitch -= m_MouseInput.y * m_VerticalSensitivity * Time.deltaTime;
        m_CameraPitch = Mathf.Clamp(m_CameraPitch, m_MaxViewAngle, m_MinViewAngle);

        m_Camera.transform.localEulerAngles = Vector3.right * m_CameraPitch;

        // Apply player model's local y rotation (horizontal rotation), to player model
        float yawRotation = m_MouseInput.x * m_HorizontalSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * yawRotation);

    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && m_IsGrounded)
        {
            m_PlayerBody.AddForce(transform.up * m_JumpForce, ForceMode.Impulse);
        }
    }

    private bool CheckGroundState()
    {
        // TODO reimplement this (shoot ray or something)
        if (m_PlayerBody.velocity.y > 0.01f || m_PlayerBody.velocity.y < -0.01f)
            return false;
        return true;
    }

    /// <summary>
    /// Handles the players different movement state i.e runnning, walking etc.
    /// </summary>
    private void HandleState()
    {
        // Running
        if (m_MouseInput.x != 0 || m_MouseInput.y != 0)
        {
            m_IsIdle = false;
            m_IsWalking = true;
        }
        else
        {
            m_IsAirborne = false;
            m_IsIdle = true;
        }
        if (!m_IsGrounded)
        {
            m_IsAirborne = true;
            m_IsIdle = false;
        }
        if (Input.GetButton(m_SprintButtonName))
        {
            m_IsRunning = true;
            m_IsWalking = false;
        }
        else
        {
            m_IsRunning = false;
        }
    }
}
