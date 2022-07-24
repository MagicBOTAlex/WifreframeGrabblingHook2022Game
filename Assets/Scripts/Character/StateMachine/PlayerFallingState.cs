using UnityEngine;
public class PlayerFaillingState : PlayerBaseState
{
    /* Pass context and state factory to super class constructor,
     * to access the context and factory in this state method. */
    public PlayerFaillingState(PlayerStateManager currentContext, PlayerStateFactory stateFactory)
    : base (currentContext, stateFactory) {}
    public override void Enter()
    {
        Debug.Log("Entered Falling state.");
    }
    public override void Exit()
    {
        
    }
    public override void Tick()
    {
        CheckSwitchStates();
    }
    public override void InitSubState()
    {
        
    }
    public override void CheckSwitchStates()
    {
        if (m_Context.MovementInput != Vector3.zero)
        {
            SwitchState(m_Factory.Walking());
        }
    }
}
