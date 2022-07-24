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


    public PlayerSettings PlayerSettings { get { return m_PlayerSettings; } private set { m_PlayerSettings = value; } }
    public ForceReciever ForceReciever { get { return m_ForceReciever; } private set { m_ForceReciever = value; } }
    public CharacterController CharacterController { get { return m_CharacterController; } private set { m_CharacterController = value; } }

    private void Start()
    {
        GameObject player = GameManager.Instance.Player;

        PlayerSettings = player.GetComponent<PlayerSettings>();
        ForceReciever = player.GetComponent<ForceReciever>();
        m_CharacterController = player.GetComponent<CharacterController>();
        //Debug.Log(ForceReciever.MovementValue);

        /* Create a new State Factory to generate new states,
         * with the context based on this. Meaning we will 
         * pass MonoBehaviour data and other settings/attributes
         * to the individual decoupled state methods. */
        m_States = new PlayerStateFactory(this);
        
        // Generate a default state
        SwitchState(m_States.Grounded());
        
    }

    private void Update()
    {
        /* Pass the scope of update to the state method. */
        m_CurrentState.TickStates();
    }

    public void SwitchState(PlayerBaseState newState)
    {
        if (m_CurrentState != null)
            m_CurrentState.ExitStates();
        m_CurrentState = newState;
        m_CurrentState.EnterStates();
    }
    public void SetSubState(PlayerBaseState newSubState)
    {
        m_CurrentState.CurrentSubState = newSubState;
        // Register the current state as the super state of the new sub state
        SetSuperState(m_CurrentState);
    }

    public void SetSuperState(PlayerBaseState newSuperState)
    {
        m_CurrentState.CurrentSuperState = newSuperState;
    }
}
