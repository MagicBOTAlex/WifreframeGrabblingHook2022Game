using UnityEngine;

public class PlayerAirborneState : PlayerBaseState
{
    public PlayerAirborneState(PlayerStateManager currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
    {
        IsRootState = true;
    }
    public override void Enter()
    {
        InitSubState();
    }

    public override void Exit()
    {
        
    }

    public override void InitSubState()
    {
        // If sub state already set, fx if the previous root state
        // passed a reference to the instance of asub state.
        if (CurrentSubState != null)
            return;

        if (m_Context.MovementInput != Vector3.zero)
        {
            SetSubState(Factory.Walking());
        }
        else
        {
            SetSubState(Factory.Falling());
        }
    }

    public override void Tick()
    {
        
        CheckSwitchStates();
    }
    public override void CheckSwitchStates()
    {
        // If grounded then switch to grounded root state
        if (m_Context.CharacterController.isGrounded)
        {
            if (CurrentSubState is PlayerWalkingState)
                SwitchState(Factory.Grounded(), CurrentSubState);
            else
                SwitchState(Factory.Grounded());
        }
    }
}
