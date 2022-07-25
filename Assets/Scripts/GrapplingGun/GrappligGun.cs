using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappligGun : MonoBehaviour
{
    private GrappligGunBaseState m_CurrentState = null;
    public GrappligGunBaseState CurrentState { get { return m_CurrentState; } private set { m_CurrentState = value; }}

    private void Start()
    {
        // Get default state
        CurrentState = new GrappligGunIdleState();
    }

    private void Update()
    {
        // Update current state
        CurrentState.Tick();
    }
    private void SwitchState(GrappligGunBaseState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
    public void Scout()
    {
        SwitchState(new GrappligGunScoutState());
    }
}
