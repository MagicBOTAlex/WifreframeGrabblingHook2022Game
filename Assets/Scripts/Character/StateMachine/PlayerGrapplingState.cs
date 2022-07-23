public class PlayerGrapplingState : PlayerBaseState
{
     /* Pass context and state factory to super class constructor,
     * to access the context and factory in this state method. */
    public PlayerGrapplingState(PlayerStateManager currentContext, PlayerStateFactory stateFactory)
    : base (currentContext, stateFactory) {}
    public override void Enter()
    {

    }
    public override void Exit()
    {
        
    }
    public override PlayerBaseState Tick()
    {
        return this;
    }
    public override void InitSubState()
    {
        
    }
    public override void CheckSwitchStates()
    {
        throw new System.NotImplementedException();
    }
}
