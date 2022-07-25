using UnityEngine;
public class GrappligGunScoutState : GrappligGunBaseState
{
    public override void Enter()
    {
        Debug.Log("Entered Scout grappling gun state.");
    }
    public override void Exit()
    {
        throw new System.NotImplementedException();
    }
    
    public override void Tick()
    {
        //Debug.Log("In Scout grappling gun state.");
    }
}
