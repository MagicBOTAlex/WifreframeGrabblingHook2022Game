using UnityEngine;
public class PlayerGrapplingState : PlayerBaseState
{
     /* Pass context and state factory to super class constructor,
     * to access the context and factory in this state method. */
    public PlayerGrapplingState(PlayerStateManager currentContext, PlayerStateFactory stateFactory)
    : base (currentContext, stateFactory)
    {
        IsRootState = true;
        
    }

    private bool m_ShouldDrag = false;
    public bool ShouldDrag { get { return m_ShouldDrag; } set { m_ShouldDrag = value; }}
    public override void Enter()
    {
        InitSubState();
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
        if (CurrentSubState != null)
            return;
        // Should just default to walking state
        SetSubState(Factory.Walking());
    }
    public override void CheckSwitchStates()
    {
        // Switch to jump root state
        if (m_Context.IsJumpPressed)
        {
            /* If walking while transitioning, then pass the 
             * walk substate to the new root state, instead of
             * creating a new instance. */
            if (CurrentSubState is PlayerWalkingState)
                SwitchState(Factory.Jumping(), CurrentSubState);
            else
                SwitchState(Factory.Jumping());
            
            // If the grappling gun was grappling then cancel it through it's context
            m_Context.GrapplingGun.GetComponent<GrapplingGunContext>().CancelGrapple();

        }
    }
}
