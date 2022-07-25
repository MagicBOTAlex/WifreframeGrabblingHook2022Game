using UnityEngine;
public class GrappligGunIdleState : GrappligGunBaseState
{
    public GrappligGunIdleState()
    {
    }

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
