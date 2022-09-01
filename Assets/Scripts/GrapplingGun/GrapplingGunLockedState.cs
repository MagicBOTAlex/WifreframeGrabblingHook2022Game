using System;
using UnityEngine;
public class GrapplingGunLockedState : GrapplingGunBaseState
{
    // Ctor for gameobject
    public GrapplingGunLockedState(GrapplingGunContext currentContext, GameObject currentTarget) : base(currentContext) 
    {
        // Set settings from context
        m_GrapplingGunSettings = Context.GrapplingGunSettings;

        // Share the target position to the context
        Context.GrapplingTargetPosition = currentTarget.transform.position;

        // Set up the grappling gun behaviour
        m_GrapplingGunBehavior = Context.GrapplingGun.GetComponent<GrapplingGunBehavior>();
        m_GrapplingGunBehavior.SetContext(Context);
        m_GrapplingGunBehavior.SetSettings(m_GrapplingGunSettings);
    }
    // Ctor for position
    public GrapplingGunLockedState(GrapplingGunContext currentContext, Vector3 currentTarget) : base(currentContext) 
    {
        // Set settings from context
        m_GrapplingGunSettings = Context.GrapplingGunSettings;

        // Share the target position to the context
        Context.GrapplingTargetPosition = currentTarget;

        // Set up the grappling gun behaviour
        m_GrapplingGunBehavior = Context.GrapplingGun.GetComponent<GrapplingGunBehavior>();
        m_GrapplingGunBehavior.SetContext(Context);
        m_GrapplingGunBehavior.SetSettings(m_GrapplingGunSettings);
    }

    private GrapplingGunBehavior m_GrapplingGunBehavior = null;
    // This state's local copy of Grappling Gun settings
    private GrapplingGunSettings m_GrapplingGunSettings = null;

    /* Local member variables. */
    private Vector3 m_TargetHitPoint = Vector3.zero;

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        /* Update the target grappling position, so if the closest 
        grabbable position changes, it will update to the new position.
        If there's no new position (Vector3.zero), it will go back into
        scout state. */ 
        m_TargetHitPoint = m_GrapplingGunBehavior.GetClosestGrabbablePoint();
        //Debug.Log($"Locked to: {m_TargetHitPoint}");

        LookAtTarget();

        CheckSwitchStates();
    }

    /// <summary>
    /// Rotates the grappling gun so that it points in
    /// the direction of the target position m_TargetHitPoint
    /// </summary>
    private void LookAtTarget()
    {
        // Create a rotation to make the cannon look at the target pos
        Vector3 vecToTarget = m_TargetHitPoint - Context.GrapplingGunHolder.transform.position;
        Debug.DrawLine(Context.GrapplingGunHolder.transform.position, m_TargetHitPoint);
        //vecToTarget.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(vecToTarget);
        //targetRotation.eulerAngles += m_GrapplingGunSettings.CannonForwardVecOffset;
        Debug.Log($"target pos: {m_TargetHitPoint}, cannon + vec to target: {Context.GrapplingGunHolder.transform.position + vecToTarget}");

        Context.GrapplingGunHolder.transform.rotation = Quaternion.Slerp(Context.GrapplingGunHolder.transform.rotation, targetRotation, m_GrapplingGunSettings.CannonRotationSpeed * Time.deltaTime);
        // Fix the rotation, because the model's forward vector is pointing
        // in the wrong direction
        //Context.GrapplingGun.transform.Rotate(m_GrapplingGunSettings.CannonForwardVecOffset);
    }

    protected override void CheckSwitchStates()
    {
        if (Context.GrapplingTargetPosition != m_TargetHitPoint)
        {
            // Update to target position to new position
            Context.GrapplingTargetPosition = m_TargetHitPoint;

            // If the target position came out of range, then
            // switch back to scout state.
            if (m_TargetHitPoint == Vector3.zero)
            {
                // Switch to scout state
                Context.SwitchState(new GrapplingGunScoutState(Context));
            }
        }
    }
}
