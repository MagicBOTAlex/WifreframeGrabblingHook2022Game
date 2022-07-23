using UnityEngine;
public class PlayerJumpingState : PlayerBaseState
{
    /* Pass context and state factory to super class constructor,
     * to access the context and factory in this state method. */
    public PlayerJumpingState(PlayerStateManager currentContext, PlayerStateFactory stateFactory)
    : base (currentContext, stateFactory) {}

    // This state's local copy of needed player settings
    private ForceReciever m_ForceReciever = null;
    private CharacterController m_CharacterController = null;
    private float m_JumpForce = 0f;

    public override void Enter()
    {
        Debug.Log("Entered Jumping State.");
        /* Get default player settings, we'll be able to change these member
         * variables and only affect this state, leaving deafults untouched. */
        m_JumpForce = m_Context.PlayerSettings.JumpForce;
        m_ForceReciever = m_Context.ForceReciever;
        m_CharacterController = m_Context.CharacterController;

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
        if (m_CharacterController.isGrounded)
            m_Context.SwitchState(m_Factory.Grounded());
    }

    private void HandleJump()
    {
        m_ForceReciever.AddForce(Vector3.up * m_JumpForce);
    }
}
