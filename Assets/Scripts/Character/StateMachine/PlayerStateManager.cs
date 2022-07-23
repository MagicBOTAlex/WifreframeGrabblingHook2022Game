using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    BaseState m_CurrentState = new PlayerIdleState();

    private void Update()
    {
        /* Pass the update function to the state method, 
         * and give it a chance to choose a new state. */
        BaseState newState = m_CurrentState.Tick(Time.deltaTime);
        if (newState != m_CurrentState)
        {
            Debug.Log("Switching state.");
            SwitchState(newState);
        }
    }

    private void SwitchState(BaseState newState)
    {
        m_CurrentState.Exit();
        m_CurrentState = newState;
        m_CurrentState.Enter();
    }
}
