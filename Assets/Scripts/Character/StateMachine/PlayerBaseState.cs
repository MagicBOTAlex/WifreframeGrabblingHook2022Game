public abstract class PlayerBaseState
{
    protected PlayerStateFactory m_Factory = null;
    protected PlayerStateManager m_Context = null;
    protected PlayerBaseState m_CurrentSubState = null;
    protected PlayerBaseState m_CurrentSuperState = null;

    public PlayerBaseState(PlayerStateManager currentContext, PlayerStateFactory stateFactory)
    {
        m_Context = currentContext;
        m_Factory = stateFactory;
    }
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Tick();
    public abstract void InitSubState();
    public abstract void CheckSwitchStates();

    /* Run tick update method on current state,
     * and on it's sub state if it exists, which
     * inturn calls it's substate and so on. */
    public void TickStates()
    {
        Tick();
        if (m_CurrentSubState != null)
        {
            m_CurrentSubState.TickStates();
        }
    }
    
}
