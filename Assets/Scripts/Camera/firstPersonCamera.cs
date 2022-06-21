using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstPersonCamera : MonoBehaviour
{
    /* Handles camera panning */
    private Camera m_Camera = null;

    [Header("Camera Settings")]
    [SerializeField] private bool m_NormalizeMouseInput = false;
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
    }
    private void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        // Holds the target mouse input direction in which we want to smooth towards
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        if (m_NormalizeMouseInput)
            targetDir.Normalize();
            
        // Accelerate to the target direction, to smooth the camera movement
        m_CurrentCameraDir = Vector2.SmoothDamp(m_CurrentCameraDir, targetDir, ref m_CurrentCameraDirDelta, m_MouseSmooth);

        // Calculate the pitch rotation based on the smoothed mouse input direction
        m_CameraPitch -= m_CurrentCameraDir.y * m_VerticalSensitivity * Time.deltaTime;
        m_CameraPitch = Mathf.Clamp(m_CameraPitch, m_MinViewAngle, m_MaxViewAngle);

        // Apply camera's local x rotation (vertical rotation), to camera
        m_Camera.transform.localEulerAngles = Vector3.right * m_CameraPitch;
    }
}
