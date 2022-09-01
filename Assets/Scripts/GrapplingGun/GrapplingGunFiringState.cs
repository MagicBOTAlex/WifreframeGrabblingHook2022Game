using UnityEngine;

public class GrapplingGunFiringState : GrapplingGunBaseState
{
    public GrapplingGunFiringState(GrapplingGunContext currentContext) : base(currentContext) 
    {

    }
    public override void Enter()
    {
        //Debug.Log("Firing Hook!");
        if (Context.PlayerStateManager.CurrentState.CurrentSubState is PlayerWalkingState)
        {
            // Pass the instance of the current walking sub state to the new root state
            // to save mem i/o and lag
            Debug.Log("Passing inst");
            Context.PlayerStateManager.CurrentState.SwitchState(Context.PlayerStateManager.CurrentState.Factory.Grappling(), Context.PlayerStateManager.CurrentState.CurrentSubState);
        }
        else
        {
            Context.PlayerStateManager.CurrentState.SwitchState(Context.PlayerStateManager.CurrentState.Factory.Grappling());
        }
        
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

            // Set player to airborne root state
            if (Context.PlayerStateManager.CurrentState.CurrentSubState is PlayerWalkingState)
                Context.PlayerStateManager.CurrentState.SwitchState(Context.PlayerStateManager.CurrentState.Factory.Airborne(), Context.PlayerStateManager.CurrentState.CurrentSubState);
            else
                Context.PlayerStateManager.CurrentState.SwitchState(Context.PlayerStateManager.CurrentState.Factory.Airborne());
        }
    }
}
