public abstract class GrapplingGunBaseState
{
    private GrapplingGun m_Context = null;
    protected GrapplingGun Context { get { return m_Context; } private set { m_Context = value; }}
    public GrapplingGunBaseState(GrapplingGun currentContext)
    {
        Context = currentContext;
    }
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Tick();
}
