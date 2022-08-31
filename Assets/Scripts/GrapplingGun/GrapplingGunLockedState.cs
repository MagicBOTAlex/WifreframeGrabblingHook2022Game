using UnityEngine;
public class GrapplingGunLockedState : GrapplingGunBaseState
{
    public GrapplingGunLockedState(GrapplingGunContext currentContext, GameObject currentTarget) : base(currentContext) 
    {
        Context.GrapplingTargetPosition = currentTarget.transform.position;
    }
    public GrapplingGunLockedState(GrapplingGunContext currentContext, Vector3 currentTarget) : base(currentContext) 
    {
        Context.GrapplingTargetPosition = currentTarget;
    }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        
    }

    protected override void CheckSwitchStates()
    {
        throw new System.NotImplementedException();
    }
}
