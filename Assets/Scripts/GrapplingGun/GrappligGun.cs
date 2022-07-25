using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappligGun : MonoBehaviour
{
    /* Grappling Gun Settings. */
    [Header("Grappling Gun Settings")]
    [SerializeField] private float m_ScoutCastRadius = 1f;

    private Vector3 m_GrapplingTargetPosition = Vector3.zero;

    /* Context member variables. */
    private GrappligGunBaseState m_CurrentState = null;
    private Camera m_Camera = null;
    private bool m_IsFireGrapplingGunPressed = false;

    /* Getters and setters for the active state. */
    public float ScoutCastRadius { get { return m_ScoutCastRadius; } set { m_ScoutCastRadius = value; }}
    public Vector3 GrapplingTargetPosition { get { return m_GrapplingTargetPosition; } set { m_GrapplingTargetPosition = value; }}
    public Camera Camera { get { return m_Camera; } private set { m_Camera = value; }}
    public GrappligGunBaseState CurrentState { get { return m_CurrentState; } private set { m_CurrentState = value; }}
    public bool IsFireGrapplingGunPressed { get { return m_IsFireGrapplingGunPressed; } private set { m_IsFireGrapplingGunPressed = value; }}

    private void Start()
    {
        // Set up context members
        Camera = GameManager.Instance.Camera.GetComponent<Camera>();

        // Get default state, with this as context
        CurrentState = new GrappligGunIdleState(this);
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
    public void SwitchState(GrappligGunBaseState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
    public void Scout()
    {
        SwitchState(new GrappligGunScoutState(this));
    }
}
