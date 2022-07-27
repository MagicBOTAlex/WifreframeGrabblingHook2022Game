using UnityEngine;
public class GrapplingGunScoutState : GrapplingGunBaseState
{
    public GrapplingGunScoutState(GrapplingGun currentContext) : base(currentContext) {}
    public override void Enter()
    {
        //Debug.Log("Entered Scout grappling gun state.");
    }
    public override void Exit()
    {

    }
    
    public override void Tick()
    {
        CheckSwitchStates();
        Scout();
    }

    private void CheckSwitchStates()
    {
        if (Context.IsFireGrapplingGunPressed && Context.GrapplingTargetPosition != Vector3.zero)
        {
            Context.SwitchState(new GrapplingGunLockedState(Context));
        }
    }

    private void Scout()
    {
        // TODO: move generic grappling gun code to context
        // casts a sphereCast from the main camera and checks if it hits.
        // if the ray hits then it will check if the object that was hit has a "GrabblebleObject" tag
        if (Physics.SphereCast(Context.Camera.transform.position, Context.ScoutCastRadius, Context.Camera.transform.forward, out RaycastHit HitInfo, 100.0f)
            && HitInfo.collider.CompareTag("GrabblebleObject"))
        {
            //Debug.DrawLine(MainCamera.transform.position, HitInfo.point);   // draws a debug line that shows the ray when hitting an object
            Context.GrapplingTargetPosition = HitInfo.transform.position;                         // stores the position of the object that was hit
            Debug.Log(Context.GrapplingTargetPosition);
        }
        else
        {
            // if the ray didn't hit or didn't hit a correct object, then sets the TargetPos to (0, 0, 0)
            Context.GrapplingTargetPosition = Vector3.zero;
        }
    }
}
