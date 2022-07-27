using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGunContext : MonoBehaviour
{
    /* Grappling Gun Settings. */
    private GrapplingGunSettings m_GrapplingGunSettings = null;

    private Vector3 m_GrapplingTargetPosition = Vector3.zero;

    /* Context member variables. */
    private GameObject m_GrapplingGun = null;
    private GrapplingGunBaseState m_CurrentState = null;
    private Camera m_Camera = null;
    private bool m_IsFireGrapplingGunPressed = false;

    /* Getters and setters for the active state. */
    public GrapplingGunSettings GrapplingGunSettings { get { return m_GrapplingGunSettings; } private set { m_GrapplingGunSettings = value; }}
    public Vector3 GrapplingTargetPosition { get { return m_GrapplingTargetPosition; } set { m_GrapplingTargetPosition = value; }}
    public GameObject GrapplingGun { get { return m_GrapplingGun; } private set { m_GrapplingGun = value; }}
    public Camera Camera { get { return m_Camera; } private set { m_Camera = value; }}
    public GrapplingGunBaseState CurrentState { get { return m_CurrentState; } private set { m_CurrentState = value; }}
    public bool IsFireGrapplingGunPressed { get { return m_IsFireGrapplingGunPressed; } private set { m_IsFireGrapplingGunPressed = value; }}

    private void Start()
    {
        m_GrapplingGun = GameManager.Instance.GrapplingGun;

        // Set up context members
        Camera = GameManager.Instance.Camera.GetComponent<Camera>();
        GrapplingGunSettings = m_GrapplingGun.GetComponent<GrapplingGunSettings>();
        
        // Get default state, with this as context
        CurrentState = new GrapplingGunIdleState(this);
    }

    private void Update()
    {
        PollInput();
        // Update current state
        CurrentState.Tick();
    }
    
    private void PollInput()
    {
        IsFireGrapplingGunPressed = Input.GetButton("FireCannon");
    }
    public void SwitchState(GrapplingGunBaseState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
    public void Scout()
    {
        SwitchState(new GrapplingGunScoutState(this));
    }
}
