using UnityEngine;

public class GrapplingGunAttachedState : GrapplingGunBaseState
{
    public GrapplingGunAttachedState(GrapplingGunContext currentContext) : base(currentContext)
    {
    }

    public override void Enter()
    {
        //Context.SwitchState(new GrapplingGunScoutState(Context));
    }

    public override void Exit()
    {
        
    }

    public override void Tick()
    {
        
        CheckSwitchStates();
    }

    protected override void CheckSwitchStates()
    {
        // Switch back to scout state if the user cancels the grapple
        if (!Context.IsFireGrapplingGunPressed)
        {
            // Set grappling gun to scout
            Context.SwitchState(new GrapplingGunScoutState(Context));

            // Set player to grounded state
            if (Context.PlayerStateManager.CharacterController.isGrounded)
            {
                if (Context.PlayerStateManager.CurrentState.CurrentSubState is PlayerWalkingState)
                    Context.PlayerStateManager.CurrentState.SwitchState(Context.PlayerStateManager.CurrentState.Factory.Grounded(), Context.PlayerStateManager.CurrentState.CurrentSubState);
                else
                    Context.PlayerStateManager.CurrentState.SwitchState(Context.PlayerStateManager.CurrentState.Factory.Grounded());
            }
            else
            { // Set player to airborne root state
                if (Context.PlayerStateManager.CurrentState.CurrentSubState is PlayerWalkingState)
                    Context.PlayerStateManager.CurrentState.SwitchState(Context.PlayerStateManager.CurrentState.Factory.Airborne(), Context.PlayerStateManager.CurrentState.CurrentSubState);
                else
                    Context.PlayerStateManager.CurrentState.SwitchState(Context.PlayerStateManager.CurrentState.Factory.Airborne());
            }
            return;
        }

        // Switch to attached state if the hook got to the target position
        if (Context.Hook.transform.position == Context.GrapplingTargetPosition)
        {
            // Set grappling gun to attached state
            Context.SwitchState(new GrapplingGunAttachedState(Context));
        }
    }
}
