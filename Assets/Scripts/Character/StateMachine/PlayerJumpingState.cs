using UnityEngine;
public class PlayerJumpingState : PlayerBaseState
{
    /* Pass context and state factory to super class constructor,
     * to access the context and factory in this state method. */
    public PlayerJumpingState(PlayerStateManager currentContext, PlayerStateFactory stateFactory)
    : base (currentContext, stateFactory) {}

    // This state's local copy of needed player settings
    private ForceReciever m_ForceReciever = null;
    private float m_JumpForce = 0f;
    public override void Enter()
    {
        /* Get default player settings, we'll be able to change these member
         * variables and only affect this state, leaving deafults untouched. */
        m_JumpForce = m_Context.PlayerSettings.JumpForce;
        m_ForceReciever = m_Context.ForceReciever;
        Debug.Log(m_ForceReciever.MovementValue);

        HandleJump();
    }
    public override void Exit()
    {
        
    }
    public override void Tick()
    {
        CheckSwitchStates();
    }
    public override void InitSubState()
    {
        
    }
    public override void CheckSwitchStates()
    {
        
    }

    private void HandleJump()
    {
        Debug.Log("Jumping");
        m_ForceReciever.AddForce(Vector3.up * m_JumpForce);
        Debug.Log($"Adding {m_JumpForce} of force upwards");
    }
}
