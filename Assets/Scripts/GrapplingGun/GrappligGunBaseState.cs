public abstract class GrappligGunBaseState
{
    public static GrappligGunBaseState Defualt()
    {
        return new GrappligGunScoutState();
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Tick();
}
