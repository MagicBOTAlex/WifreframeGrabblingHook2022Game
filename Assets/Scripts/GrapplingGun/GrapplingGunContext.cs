/*
GrapplingGunContext.cs is a class that is the main context which controls
the grappling gun state machine. It for example holds the value of the current
state and has the implementation to switch the state. It also holds member
variables such as GrapplingGunSettings and Camera, so every state can get
access to theese values.
*/

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
    private GameObject m_GrapplingGunHolder = null;
    private GrapplingGunBaseState m_CurrentState = null;
    private Camera m_Camera = null;
    private GameObject m_Player = null;
    private PlayerStateManager m_PlayerStateManager = null;
    private bool m_IsFireGrapplingGunPressed = false;

    /* Getters and setters which the current active state can use to get references. */
    public GrapplingGunSettings GrapplingGunSettings { get { return m_GrapplingGunSettings; } private set { m_GrapplingGunSettings = value; }}
    public Vector3 GrapplingTargetPosition { get { return m_GrapplingTargetPosition; } set { m_GrapplingTargetPosition = value; }}
    public GameObject GrapplingGun { get { return m_GrapplingGun; } private set { m_GrapplingGun = value; }}
    public GameObject GrapplingGunHolder { get { return m_GrapplingGunHolder; } private set { m_GrapplingGunHolder = value; }}
    public Camera Camera { get { return m_Camera; } private set { m_Camera = value; }}
    public GrapplingGunBaseState CurrentState { get { return m_CurrentState; } private set { m_CurrentState = value; }}
    public GameObject Player { get { return m_Player; } private set { m_Player = value; }}
    public PlayerStateManager PlayerStateManager { get { return m_PlayerStateManager; } private set { m_PlayerStateManager = value; }}
    public bool IsFireGrapplingGunPressed { get { return m_IsFireGrapplingGunPressed; } private set { m_IsFireGrapplingGunPressed = value; }}

    private void Start()
    {
        GrapplingGun = GameManager.Instance.GrapplingGun;
        GrapplingGunHolder = GameManager.Instance.GrapplingGunHolder;
        Player = GameManager.Instance.Player;
        PlayerStateManager = Player.GetComponent<PlayerStateManager>();

        // Set up context members
        Camera = GameManager.Instance.Camera.GetComponent<Camera>();
        GrapplingGunSettings = m_GrapplingGun.GetComponent<GrapplingGunSettings>();
        
        // Get default state, with this as context
        CurrentState = new GrapplingGunIdleState(this);
    }

    private void Update()
    {
        PollInput();

        // Tick current state
        CurrentState.Tick();
    }
    
    private void PollInput()
    {
        IsFireGrapplingGunPressed = Input.GetButton("FireCannon");
    }

    /// <summary>
    /// Switches to a different state. First it will signal an exit to the
    /// current active state by calling Exit(). After that swicth to the new
    /// state and signal an enter on the new state by calling Enter().
    /// </summary>
    /// <param name="newState">The new state to switch to.</param>
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
