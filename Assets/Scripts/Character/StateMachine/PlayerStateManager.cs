using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    private PlayerBaseState m_CurrentState;
    private PlayerStateFactory m_States;

    

    private void Awake()
    {
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
        /* Pass the scope of update to the state method, 
         * and give it a chance to choose a new state. */
        PlayerBaseState newState = m_CurrentState.Tick();
        if (newState != m_CurrentState)
        {
            Debug.Log("Switching state.");
            SwitchState(newState);
        }
    }

    private void SwitchState(PlayerBaseState newState)
    {
        if (m_CurrentState != null)
            m_CurrentState.Exit();
        m_CurrentState = newState;
        m_CurrentState.Enter();
    }
}
