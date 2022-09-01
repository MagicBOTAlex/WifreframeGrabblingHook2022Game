public class PlayerGrapplingState : PlayerBaseState
{
     /* Pass context and state factory to super class constructor,
     * to access the context and factory in this state method. */
    public PlayerGrapplingState(PlayerStateManager currentContext, PlayerStateFactory stateFactory)
    : base (currentContext, stateFactory)
    {
        IsRootState = true;
        InitSubState();
    }
    public override void Enter()
    {

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
        // Should just default to dragging state
        SetSubState(Factory.Dragging());
    }
    public override void CheckSwitchStates()
    {
        
    }
}
