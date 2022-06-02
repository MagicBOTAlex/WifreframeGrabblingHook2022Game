using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{
    // private Rigidbody m_PlayerBody;
    private Camera m_Camera;
    private CharacterController m_CharacterController;

    [Header("Character controller settings")]
    [SerializeField] private float m_MovementSpeed = 5f;
    [SerializeField] private float m_MoveSmoothDelta = 0.1f;
    [SerializeField] private float m_JumpForce = 5f;
    [SerializeField] private float m_Gravity = 9.82f;

    [Header("Camera settings")]
    [SerializeField] private float m_HorizontalSensitivity = 400f;
    [SerializeField] private float m_VerticalSensitivity = 400f;
    [SerializeField] private float m_MouseSmoothDelta = 0.01f;
    [SerializeField] private float m_MaxViewAngle = -90f;
    [SerializeField] private float m_MinViewAngle = 90f;

    private Vector3 m_MovementVelocity = Vector3.zero;
    private Vector3 m_MovementVelocityDelta = Vector3.zero;
    private Vector3 m_CurrentMovementDir = Vector3.zero;
    private Vector2 m_MouseVelocityDelta = Vector3.zero;
    private Vector2 m_CurrentMouseDir = Vector3.zero;
    private float m_CameraPitch = 0f;
    private bool m_IsGrounded = false;
    private bool m_LockCamera = false;
    private bool m_GravityEnabled = true;

    private void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_Camera = GameManager.Instance.Camera.GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    private void Update() 
    {
        m_IsGrounded = CheckGroundState();

        // HandleState();
        HandleMovement();
        HandleRotation();
    }

    public void AddForce(Vector3 velocity) {
        velocity = transform.TransformDirection(velocity);
        m_CharacterController.Move(velocity);
    }

    public bool SetGravity(bool value) {
        m_GravityEnabled = value;
        return value;
    }

    /// <summary>
    /// Handles all physical movement, such as walking, jumping etc.
    /// </summary>
    private void HandleMovement() 
    {
        // Get unit vector representing the target direction in which the player should move in it's local axis
        Vector3 targetDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        // Gradually smooth the the current movement dir vector to the target dir, specified by 'm_MoveSmoothDelta'
        m_CurrentMovementDir = Vector3.SmoothDamp(m_CurrentMovementDir, targetDir, ref m_MovementVelocityDelta, m_MoveSmoothDelta);

        // Gravity
        if (m_IsGrounded)
        {
            m_MovementVelocity.y = 0f;
            // Jump
            if (Input.GetButton("Jump"))
            {
                m_MovementVelocity.y += m_JumpForce;
            }
        }
        if (m_GravityEnabled)
            m_MovementVelocity.y -= m_Gravity * Time.deltaTime;

        // Add movement scalar to x- and z-axises, and gravity scalar to y-axis and apply movement on the characters local axis
        Vector3 smoothedMoveVelocity = (transform.forward * m_CurrentMovementDir.z + transform.right * m_CurrentMovementDir.x) * m_MovementSpeed + Vector3.up * m_MovementVelocity.y;
        m_MovementVelocity.x = smoothedMoveVelocity.x;
        m_MovementVelocity.z = smoothedMoveVelocity.z;
        m_CharacterController.Move(smoothedMoveVelocity * Time.deltaTime);

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

        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        m_CurrentMouseDir = Vector2.SmoothDamp(m_CurrentMouseDir, targetDir, ref m_MouseVelocityDelta, m_MouseSmoothDelta);

        // Apply camera's local x rotation (vertical rotation), to camera
        m_CameraPitch -= m_CurrentMouseDir.y * m_VerticalSensitivity * Time.deltaTime;
        m_CameraPitch = Mathf.Clamp(m_CameraPitch, m_MaxViewAngle, m_MinViewAngle);
        m_Camera.transform.localEulerAngles = Vector3.right * m_CameraPitch;

        // Apply player model's local y rotation (horizontal rotation), to player model
        float yawRotation = m_CurrentMouseDir.x * m_HorizontalSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * yawRotation);

    }

    private bool CheckGroundState()
    {
        return m_CharacterController.isGrounded;
    }

    /// <summary>
    /// Handles the players different movement state i.e runnning, walking etc.
    /// </summary>
    private void HandleState()
    {

    }

    private void OnCollisionEnter(Collision collider) {
        Debug.Log("hit");
        Debug.Log(collider.gameObject.name);
    }
}
