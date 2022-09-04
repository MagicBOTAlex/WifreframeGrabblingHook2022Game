using System;
using UnityEngine;

public class GrapplingGunAttachedState : GrapplingGunBaseState
{
    public GrapplingGunAttachedState(GrapplingGunContext currentContext) : base(currentContext)
    {
        // Set settings from context
        m_GrapplingGunSettings = Context.GrapplingGunSettings;

        // Set the radius of the virtual swing circle for the swing mechanic
        Context.PlayerStateManager.TetherRadius = (Context.GrapplingTargetPosition - Context.Player.transform.position).magnitude;

        // Set up the grappling gun behaviour
        m_GrapplingGunBehavior = Context.GrapplingGun.GetComponent<GrapplingGunBehavior>();
        m_GrapplingGunBehavior.SetContext(Context);
        m_GrapplingGunBehavior.SetSettings(m_GrapplingGunSettings);
    }
    private GrapplingGunSettings m_GrapplingGunSettings = null;
    private GrapplingGunBehavior m_GrapplingGunBehavior = null;

    public override void Enter()
    {
        // Signal player it can now drag
        PlayerGrapplingState gState = (Context.PlayerStateManager.CurrentState as PlayerGrapplingState);
        if (gState != null)
        {
            Debug.Log("starting drag");
            gState.ShouldDrag = true;
        }
        
    }

    public override void Exit()
    {
        // Signal player it should stop dragging
        PlayerGrapplingState gState = (Context.PlayerStateManager.CurrentState as PlayerGrapplingState);
        if (gState != null)
        {
            Debug.Log("Stopping drag");
            gState.ShouldDrag = false;
        }
    }

    public override void Tick()
    {
        m_GrapplingGunBehavior.DrawWireToHook();
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
    }
}
