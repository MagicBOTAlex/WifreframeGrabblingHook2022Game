public abstract class GrapplingGunBaseState
{
    private GrapplingGunContext m_Context = null;
    protected GrapplingGunContext Context { get { return m_Context; } private set { m_Context = value; }}
    public GrapplingGunBaseState(GrapplingGunContext currentContext)
    {
        Context = currentContext;
    }
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Tick();
}
