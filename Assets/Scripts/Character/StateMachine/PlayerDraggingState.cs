using UnityEngine;

public class PlayerDraggingState : PlayerBaseState
{
    /* Pass context and state factory to super class constructor,
     * to access the context and factory in this state method. */
    public PlayerDraggingState(PlayerStateManager currentContext, PlayerStateFactory stateFactory)
    : base (currentContext, stateFactory) {}

    public override void CheckSwitchStates()
    {
        throw new System.NotImplementedException();
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public override void InitSubState()
    {
        throw new System.NotImplementedException();
    }

    public override void Tick()
    {
        
    }
}
