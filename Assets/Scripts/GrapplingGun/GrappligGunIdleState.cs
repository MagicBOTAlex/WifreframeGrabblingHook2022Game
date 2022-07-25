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
        throw new System.NotImplementedException();
    }
    public override void Tick()
    {
        throw new System.NotImplementedException();
    }
}
