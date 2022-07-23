public abstract class PlayerBaseState
{
    protected PlayerStateFactory m_Factory;
    protected PlayerStateManager m_Context;

    public PlayerBaseState(PlayerStateManager currentContext, PlayerStateFactory stateFactory)
    {
        m_Context = currentContext;
        m_Factory = stateFactory;
    }
    public abstract void Enter();
    public abstract void Exit();
    public abstract PlayerBaseState Tick(float deltaTime);
    public abstract void InitSubState();
    
}
