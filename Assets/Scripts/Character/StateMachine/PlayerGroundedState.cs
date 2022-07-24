using UnityEngine;
public class PlayerGroundedState : PlayerBaseState
{
    /* Pass context and state factory to super class constructor,
     * to access the context and factory in this state method. */
    public PlayerGroundedState(PlayerStateManager currentContext, PlayerStateFactory stateFactory)
    : base(currentContext, stateFactory) 
    {
        IsRootState = true;
        InitSubState();
    }
    public override void Enter()
    {
        Debug.Log("Entered grounded state.");
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
        if (m_Context.MovementInput != Vector3.zero)
        {
            SetSubState(m_Factory.Walking());
        }
        else {
            SetSubState(m_Factory.Idle());
        }
    }
    public override void CheckSwitchStates()
    {
        // Switch to jump root state
        if (Input.GetButtonDown("Jump"))
            SwitchState(m_Factory.Jumping());
        
    }
}
