using UnityEngine;
public class PlayerJumpingState : PlayerBaseState
{
    /* Pass context and state factory to super class constructor,
     * to access the context and factory in this state method. */
    public PlayerJumpingState(PlayerStateManager currentContext, PlayerStateFactory stateFactory)
    : base (currentContext, stateFactory) 
    {
        IsRootState = true;
        InitSubState();
    }

    private float m_CheckSwitchDelay = 0.1f;
    private float m_CurrentCheckSwitchDelay = 0f;
    private ForceReciever m_ForceReciever = null;
    private CharacterController m_CharacterController = null;

    private GrappligGun m_GrapplingGun = null;
    protected GrappligGun GrappligGun { get { return m_GrapplingGun; } private set { m_GrapplingGun = value; }}

    // This state's local copy of needed player settings
    private float m_JumpForce = 0f;

    public override void Enter()
    {
        Debug.Log("Entered Jumping State.");

        GrappligGun = GameManager.Instance.GrapplingGun.GetComponent<GrappligGun>();
        if (GrappligGun.CurrentState is GrappligGunIdleState)
            GrappligGun.Scout();

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
        /* Wait for m_CheckSwitchDelay before checking to switch states,
         * because isgounded() will return true little after jump. */
        if (m_CurrentCheckSwitchDelay < m_CheckSwitchDelay)
        {
            m_CurrentCheckSwitchDelay += Time.deltaTime;
        }
        else {
            CheckSwitchStates();
        }
        
    }
    public override void InitSubState()
    {
        if (m_Context.MovementInput != Vector3.zero)
        {
            SetSubState(m_Factory.Walking());
        }
        else
        {
            SetSubState(m_Factory.Falling());
        }
    }
    public override void CheckSwitchStates()
    {
        if (m_CharacterController.isGrounded)
        {
            /* If walking while transitioning, then pass the 
             * walk substate to the new root state, instead of
             * creating a new instance. */
            if (CurrentSubState is PlayerWalkingState)
                SwitchState(m_Factory.Grounded(), CurrentSubState);
            else
                SwitchState(m_Factory.Grounded());
        }
    }

    private void HandleJump()
    {
        m_ForceReciever.AddForce(Vector3.up * m_JumpForce);
    }
}
