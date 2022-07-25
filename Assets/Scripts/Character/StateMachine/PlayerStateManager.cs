using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    private PlayerBaseState m_CurrentState;
    private PlayerStateFactory m_States;
    
    private PlayerSettings m_PlayerSettings = null;
    private ForceReciever m_ForceReciever = null;
    private CharacterController m_CharacterController = null;
    private MovementHandler m_MovementHandler = null;


    private Vector3 m_MovementInput = Vector3.zero;
    private bool m_IsJumpPressed = false;
    private bool m_IsFirePressed = false;

    public PlayerBaseState CurrentState { get { return m_CurrentState; } set { m_CurrentState = value; } }

    public Vector3 MovementInput { get { return m_MovementInput; } private set { m_MovementInput = value; } }
    public bool IsJumpPressed { get { return m_IsJumpPressed; } private set { m_IsJumpPressed = value; } }
    public bool IsFirePressed { get { return m_IsFirePressed; } private set { m_IsFirePressed = value; } }
    public PlayerSettings PlayerSettings { get { return m_PlayerSettings; } private set { m_PlayerSettings = value; } }
    public ForceReciever ForceReciever { get { return m_ForceReciever; } private set { m_ForceReciever = value; } }
    public CharacterController CharacterController { get { return m_CharacterController; } private set { m_CharacterController = value; } }
    public MovementHandler MovementHandler { get { return m_MovementHandler; } private set { m_MovementHandler = value; } }

    private void Start()
    {
        GameObject player = GameManager.Instance.Player;

        PlayerSettings = player.GetComponent<PlayerSettings>();
        ForceReciever = player.GetComponent<ForceReciever>();
        CharacterController = player.GetComponent<CharacterController>();
        MovementHandler = player.GetComponent<MovementHandler>();

        /* Create a new State Factory to generate new states,
         * with the context based on this. Meaning we will 
         * pass MonoBehaviour data and other settings/attributes
         * to the individual decoupled state methods. */
        m_States = new PlayerStateFactory(this);
        
        // Generate a default state
        CurrentState = m_States.Grounded();
        CurrentState.EnterStates();
    }

    private void Update()
    {
        PollInput();
        /* Pass the scope of update to the state method. */
        m_CurrentState.TickStates();
    }

    private void PollInput()
    {
        MovementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        IsJumpPressed = Input.GetButtonDown("Jump");
        IsFirePressed = Input.GetButtonDown("FireCannon");
    }
}
