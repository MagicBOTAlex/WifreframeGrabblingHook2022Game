public abstract class PlayerBaseState
{
    protected PlayerStateFactory m_Factory = null;
    protected PlayerStateManager m_Context = null;
    private PlayerBaseState m_CurrentSubState = null;
    private PlayerBaseState m_CurrentSuperState = null;

    public PlayerBaseState CurrentSubState { get { return m_CurrentSubState; } set { m_CurrentSubState = value; }}
    public PlayerBaseState CurrentSuperState { get { return m_CurrentSuperState; } set { m_CurrentSuperState = value; }}

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
        if (CurrentSubState != null)
            CurrentSubState.TickStates();
    }

    public void ExitStates()
    {
        Exit();
        if (CurrentSubState != null)
            CurrentSubState.ExitStates();
    }
    
    public void EnterStates()
    {
        Enter();
        if (CurrentSubState != null)
            CurrentSubState.EnterStates();
    }



    
}
