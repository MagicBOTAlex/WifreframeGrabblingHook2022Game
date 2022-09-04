using System;
using UnityEngine;

public class GrapplingGunFiringState : GrapplingGunBaseState
{
    public GrapplingGunFiringState(GrapplingGunContext currentContext) : base(currentContext) 
    {
        // Set settings from context
        m_GrapplingGunSettings = Context.GrapplingGunSettings;

        // Set up the grappling gun behaviour
        m_GrapplingGunBehavior = Context.GrapplingGun.GetComponent<GrapplingGunBehavior>();
        m_GrapplingGunBehavior.SetContext(Context);
        m_GrapplingGunBehavior.SetSettings(m_GrapplingGunSettings);
    }

    private GrapplingGunSettings m_GrapplingGunSettings = null;
    private GrapplingGunBehavior m_GrapplingGunBehavior = null;
    private float m_AttachDistance;
    public override void Enter()
    {
        //Debug.Log("Firing Hook!");
        if (Context.PlayerStateManager.CurrentState.CurrentSubState is PlayerWalkingState)
        {
            // Pass the instance of the current walking sub state to the new root state
            // to save mem i/o and lag
            Context.PlayerStateManager.CurrentState.SwitchState(Context.PlayerStateManager.CurrentState.Factory.Grappling(Context.GrapplingTargetPosition), Context.PlayerStateManager.CurrentState.CurrentSubState);
        }
        else
        {
            Context.PlayerStateManager.CurrentState.SwitchState(Context.PlayerStateManager.CurrentState.Factory.Grappling(Context.GrapplingTargetPosition));
        }

        Context.Hook.transform.parent = null;
        
    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        m_AttachDistance = Vector3.Distance(Context.Hook.transform.position, Context.GrapplingTargetPosition);
        ShootHook();
        m_GrapplingGunBehavior.DrawWireToHook();
        CheckSwitchStates();
    }

    private void ShootHook()
    {
        if (m_AttachDistance > m_GrapplingGunSettings.HookAttachThreshold)
            Context.Hook.transform.position = Vector3.Lerp(Context.Hook.transform.position, Context.GrapplingTargetPosition, m_GrapplingGunSettings.HookSpeed * Time.deltaTime);
        else
            Context.Hook.transform.position = Context.GrapplingTargetPosition;
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
