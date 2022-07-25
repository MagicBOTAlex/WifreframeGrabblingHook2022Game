public abstract class GrappligGunBaseState
{
    private GrappligGun m_Context = null;
    protected GrappligGun Context { get { return m_Context; } private set { m_Context = value; }}
    public GrappligGunBaseState(GrappligGun currentContext)
    {
        Context = currentContext;
    }
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Tick();
}
