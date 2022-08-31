/*
GrapplingGunBaseState.cs is an abstract class which means
that is holds some code which all derived sub-classes need.

every grappling gun state sub-class for example needs a way
access the context, and an Enter(), Exit() and Tick() method.
*/
public abstract class GrapplingGunBaseState
{
    private GrapplingGunContext m_Context = null;
    protected GrapplingGunContext Context { get { return m_Context; } private set { m_Context = value; }}
    public GrapplingGunBaseState(GrapplingGunContext currentContext)
    {
        Context = currentContext;
    }
    /// <summary>
    /// Gets called when the state this abstract method
    /// belongs to gets activated.
    /// </summary>
    public abstract void Enter();

    /// <summary>
    /// Gets called when the state this abstract method
    /// belongs to gets deactivated.
    /// </summary>
    public abstract void Exit();

    /// <summary>
    /// Gets called every frame while the state this 
    /// abstract method belongs to is active.
    /// </summary>
    public abstract void Tick();

    /// <summary>
    /// Checks if the current state should switch to a
    /// different state. Calling this method depends on
    /// the implementation in the state. Often it will
    /// get called in the active state's Tick().
    /// </summary>
    protected abstract void CheckSwitchStates();
}
