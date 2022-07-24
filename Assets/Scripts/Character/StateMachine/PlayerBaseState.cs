using UnityEngine;
public abstract class PlayerBaseState
{
    protected bool m_IsRootState = false;
    protected PlayerStateFactory m_Factory = null;
    protected PlayerStateManager m_Context = null;
    private PlayerBaseState m_CurrentSubState = null;
    private PlayerBaseState m_CurrentSuperState = null;

    public bool IsRootState { get { return m_IsRootState; } protected set { m_IsRootState = value; }}
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
    public void SwitchState(PlayerBaseState newState, PlayerBaseState passedSubState = null)
    {
        /* Only change the super state if the new state is a
         * root state, otherwise it's a sub state switch. */
        if (IsRootState)
        {
            m_Context.CurrentState = newState;
            if (passedSubState != null)
            {
                newState.SetSubState(passedSubState);
                Exit();
                newState.Enter();
            }
            else {
                ExitStates();
                newState.EnterStates();
            }
        }
        else if (CurrentSuperState != null)
        {
            CurrentSuperState.SetSubState(newState);
            
            newState.EnterStates();
        }

        
    }

    protected void SetSubState(PlayerBaseState newSubState)
    {
        CurrentSubState = newSubState;

        // Register the current state as the super state of the new sub state
        //Debug.Log($"Setting new super state: {this}");
        newSubState.SetSuperState(this);
    }

    protected void SetSuperState(PlayerBaseState newSuperState)
    {
        CurrentSuperState = newSuperState;
    }
}
