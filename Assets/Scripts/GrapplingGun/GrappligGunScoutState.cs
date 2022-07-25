using UnityEngine;
public class GrappligGunScoutState : GrappligGunBaseState
{
    public GrappligGunScoutState(GrappligGun currentContext) : base(currentContext) {}
    public override void Enter()
    {
        //Debug.Log("Entered Scout grappling gun state.");
    }
    public override void Exit()
    {
        
    }
    
    public override void Tick()
    {
        if (!CheckSwitchStates())
            Scout();
        
    }

    private bool CheckSwitchStates()
    {
        if (Context.IsFireGrapplingGunPressed && Context.GrapplingTargetPosition != Vector3.zero)
        {
            Context.SwitchState(new GrappligGunLockedState(Context));
            return true;
        }

        return false;
    }

    private void Scout()
    {
        if (!Input.GetButton("FireCannon"))
        {
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
}
