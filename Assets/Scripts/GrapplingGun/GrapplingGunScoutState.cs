using System;
using UnityEngine;
public class GrapplingGunScoutState : GrapplingGunBaseState
{
    // Pass the context to the abstract super class GrapplingGunBaseState
    public GrapplingGunScoutState(GrapplingGunContext currentContext) : base(currentContext) 
    {
        m_GrapplingGunSettings = Context.GrapplingGunSettings;

        // Apply state specific settings

        // Set up the grappling gun behaviour
        m_GrapplingGunBehavior = Context.GrapplingGun.GetComponent<GrapplingGunBehavior>();
        m_GrapplingGunBehavior.SetContext(Context);
        m_GrapplingGunBehavior.SetSettings(m_GrapplingGunSettings);
    }
    private GrapplingGunBehavior m_GrapplingGunBehavior = null;

    // This state's local copy of Grappling Gun settings
    private GrapplingGunSettings m_GrapplingGunSettings = null;

    /* Local member variables. */
    private GameObject m_TargetObject = null;
    private Vector3 m_TargetHitPoint = Vector3.zero;

    /* Base state implementations. */
    public override void Enter()
    {
        
    }
    public override void Exit()
    {

    }

    public override void Tick()
    {
        // Scout for hookable points in the look direction
        Scout();

        // Slerp rotation of cannon and lerp hook position towards the default value
        ResetCannon();

        CheckSwitchStates();
    }

    private void ResetCannon()
    {
        // Lerp hook back to cannon
        Context.Hook.transform.position = Vector3.Lerp(Context.Hook.transform.position, Context.HookDefaultPosition.position, m_GrapplingGunSettings.HookSpeed * Time.deltaTime);
        if (Vector3.Distance(Context.Hook.transform.position, Context.HookDefaultPosition.position) <= m_GrapplingGunSettings.HookAttachThreshold)
        {
            Context.Hook.transform.position = Context.HookDefaultPosition.position;
            Context.Hook.transform.parent = Context.Cannon.transform;
        }

        // Rotate cannon back to it's default
        Context.GrapplingGunHolder.transform.rotation = Quaternion.Slerp(Context.GrapplingGunHolder.transform.rotation, Context.Camera.transform.rotation * Quaternion.identity, m_GrapplingGunSettings.CannonRotationSpeed * Time.deltaTime);
    }

    protected override void CheckSwitchStates()
    {
        if (m_TargetHitPoint != Vector3.zero)
        {
            Context.SwitchState(new GrapplingGunLockedState(Context, m_TargetHitPoint));
        }
    }

    /// <summary>
    /// Gets called every frame while GrapplingGunScoutState
    /// is the current active state.
    /// </summary>
    private void Scout()
    {
        //m_TargetObject = m_GrapplingGunBehavior.GetClosestGrabbableObject();
        m_TargetHitPoint = m_GrapplingGunBehavior.GetClosestGrabbablePoint();
    }
}
