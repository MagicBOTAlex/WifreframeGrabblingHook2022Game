using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappligGun : MonoBehaviour
{
    GrappligGunBaseState m_CurrentState = null;

    private void Start()
    {
        // Get default state
        m_CurrentState = new GrappligGunIdleState();
    }

    private void SwitchState(GrappligGunBaseState newState)
    {
        m_CurrentState.Exit();
        m_CurrentState = newState;
        m_CurrentState.Enter();
    }
}
