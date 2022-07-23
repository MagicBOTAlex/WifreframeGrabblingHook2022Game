public abstract class BaseState
{
    public virtual void Enter() { return; }
    public virtual void Exit() { return; }
    public virtual BaseState Tick(float deltaTime) { return this; }
    
}
