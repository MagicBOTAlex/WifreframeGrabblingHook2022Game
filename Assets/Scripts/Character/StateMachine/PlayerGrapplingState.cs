public class PlayerGrapplingState : PlayerBaseState
{
     /* Pass context and state factory to super class constructor,
     * to access the context and factory in this state method. */
    public PlayerGrapplingState(PlayerStateManager currentContext, PlayerStateFactory stateFactory)
    : base (currentContext, stateFactory)
    {
        IsRootState = true;
        
    }
    public override void Enter()
    {
        InitSubState();
    }
    public override void Exit()
    {
        
    }
    public override void Tick()
    {
        CheckSwitchStates();
    }
    public override void InitSubState()
    {
        if (CurrentSubState != null)
            return;
        // Should just default to walking state
        SetSubState(Factory.Walking());
    }
    public override void CheckSwitchStates()
    {
        
    }
}
