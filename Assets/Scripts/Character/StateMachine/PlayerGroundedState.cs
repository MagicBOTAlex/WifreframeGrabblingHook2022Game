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

    private GrapplingGunContext m_GrapplingGun = null;
    protected GrapplingGunContext GrappligGun { get { return m_GrapplingGun; } private set { m_GrapplingGun = value; }}

    public override void Enter()
    {
        //Debug.Log("Entered grounded state.");
        GrappligGun = GameManager.Instance.GrapplingGun.GetComponent<GrapplingGunContext>();
        if (GrappligGun.CurrentState is GrapplingGunIdleState)
            GrappligGun.Scout();
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
            SetSubState(Factory.Walking());
        }
        else
        {
            SetSubState(Factory.Idle());
        }
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
        }
        
    }
}
