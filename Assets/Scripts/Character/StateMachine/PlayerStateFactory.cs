public class PlayerStateFactory
{
    private PlayerStateManager m_Context;
    public PlayerStateFactory(PlayerStateManager context)
    {
        m_Context = context;
    }

    /* Factory for creating new states. */
    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(m_Context, this);
    }
    public PlayerBaseState Walking()
    {
        return new PlayerWalkingState(m_Context, this);
    }
    public PlayerBaseState Jumping()
    {
        return new PlayerJumpingState(m_Context, this);
    }
    public PlayerBaseState Grounded()
    {
        return new PlayerGroundedState(m_Context, this);
    }
    public PlayerBaseState Grappling()
    {
        return new PlayerGrapplingState(m_Context, this);
    }
    public PlayerBaseState Airborne()
    {
        return new PlayerAirborneState(m_Context, this);
    }
    public PlayerBaseState Falling()
    {
        return new PlayerFaillingState(m_Context, this);
    }
}
