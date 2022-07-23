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


    public PlayerSettings PlayerSettings { get { return m_PlayerSettings; } private set { m_PlayerSettings = value; } }
    public ForceReciever ForceReciever { get { return m_ForceReciever; } private set { m_ForceReciever = value; } }

    private void Start()
    {
        GameObject player = GameManager.Instance.Player;

        PlayerSettings = player.GetComponent<PlayerSettings>();
        ForceReciever = player.GetComponent<ForceReciever>();
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
        m_CurrentState.Tick();
    }

    public void SwitchState(PlayerBaseState newState)
    {
        if (m_CurrentState != null)
            m_CurrentState.Exit();
        m_CurrentState = newState;
        m_CurrentState.Enter();
    }
}
