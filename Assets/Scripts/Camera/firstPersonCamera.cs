using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstPersonCamera : MonoBehaviour
{
    /* Handles camera panning */
    
    [Header("References")]
    [SerializeField] private Transform m_PlayerTransform;
    private Camera m_Camera = null;

    [Header("Camera Settings")]
    [SerializeField] private float m_HorizontalSensitivity = 400f;
    [SerializeField] private float m_VerticalSensitivity = 400f;
    [SerializeField] private float m_MouseSmooth = 0.1f;
    [SerializeField] private float m_MaxViewAngle = 90f;
    [SerializeField] private float m_MinViewAngle = -90f;

    // The actual current camera direction
    private Vector2 m_CurrentCameraDir = Vector2.zero;
    
    // The direction difference used by sigmoid function in smoothdamp
    private Vector2 m_CurrentCameraDirDelta = Vector2.zero;
    private float m_CameraPitch = 0f;

    private void Start()
    {
        m_Camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        // Holds the target mouse input direction in which we want to smooth towards
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            
        // Accelerate to the target direction, to smooth the camera movement
        m_CurrentCameraDir = Vector2.SmoothDamp(m_CurrentCameraDir, targetDir, ref m_CurrentCameraDirDelta, m_MouseSmooth);

        // Calculate the pitch rotation based on the smoothed mouse input direction
        m_CameraPitch -= m_CurrentCameraDir.y * m_VerticalSensitivity * Time.deltaTime;
        m_CameraPitch = Mathf.Clamp(m_CameraPitch, m_MinViewAngle, m_MaxViewAngle);

        // Apply camera's local x rotation (vertical rotation), to camera
        m_Camera.transform.localEulerAngles = Vector3.right * m_CameraPitch;

        // Calculate the yaw rotation of the player model based on the smoothed mouse input direction
        float yawRotation = m_PlayerTransform.localEulerAngles.z + m_CurrentCameraDir.x * m_HorizontalSensitivity * Time.deltaTime;

        // Apply player model's local z rotation (horizontal rotation), to player model
        m_PlayerTransform.Rotate(Vector3.up * yawRotation);
    }
}
