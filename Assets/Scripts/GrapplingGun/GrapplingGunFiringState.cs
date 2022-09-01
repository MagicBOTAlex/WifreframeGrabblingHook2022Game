using UnityEngine;

public class GrapplingGunFiringState : GrapplingGunBaseState
{
    public GrapplingGunFiringState(GrapplingGunContext currentContext) : base(currentContext) 
    {

    }
    public override void Enter()
    {
        Debug.Log("Firing Hook!");
        Context.PlayerStateManager.CurrentState.SwitchState(Context.PlayerStateManager.CurrentState.Factory.Grappling());
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public override void Tick()
    {
        
    }

    protected override void CheckSwitchStates()
    {
        throw new System.NotImplementedException();
    }
}
