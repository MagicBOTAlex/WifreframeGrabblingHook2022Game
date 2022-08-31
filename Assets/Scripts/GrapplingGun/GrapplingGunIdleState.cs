using UnityEngine;
public class GrapplingGunIdleState : GrapplingGunBaseState
{
    // Pass the context to the abstract super class GrapplingGunBaseState
    public GrapplingGunIdleState(GrapplingGunContext currentContext) : base(currentContext) {}
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

    protected override void CheckSwitchStates()
    {
        throw new System.NotImplementedException();
    }
}
