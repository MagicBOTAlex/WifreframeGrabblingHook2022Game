public abstract class PlayerBaseState
{
    public virtual void Enter() { return; }
    public virtual void Exit() { return; }
    public virtual PlayerBaseState Tick(float deltaTime) { return this; }
    
}
