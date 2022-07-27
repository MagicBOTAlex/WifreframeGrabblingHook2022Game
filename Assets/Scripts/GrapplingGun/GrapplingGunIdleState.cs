using UnityEngine;
public class GrapplingGunIdleState : GrapplingGunBaseState
{
    public GrapplingGunIdleState(GrapplingGun currentContext) : base(currentContext) {}
    public override void Enter()
    {
        Debug.Log("Entered Idle grappling gun state.");
    }

    public override void Exit()
    {
        Debug.Log("Leaving Idle grapple gun state.");
    }
    public override void Tick()
    {
    }
}
