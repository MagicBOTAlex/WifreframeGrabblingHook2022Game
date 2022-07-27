using UnityEngine;

public class PlayerWalkingState : PlayerBaseState, IMovementModifier
{
    private CharacterController m_CharacterController = null;
    private MovementHandler m_MovementHandler = null;
    private Transform m_Player = null;
    public Vector3 MovementValue { get; private set; }

    // This state's local copy of player settings
    private PlayerSettings m_PlayerSettings;

    // The actual current velocity, after applying acceleration
    private Vector3 m_CurrentMovementVelocity = Vector3.zero;
    // The velocity difference used by the sigmoid function in smoothdamp
    private Vector3 m_CurrentMovementVelocityDelta = Vector3.zero;

    private float m_CheckSwitchDelay = 0.1f;
    private float m_CurrentCheckSwitchDelay = 0f;

    /* Pass context and state factory to super class constructor,
     * to access the context and factory in this state method. */
    public PlayerWalkingState(PlayerStateManager currentContext, PlayerStateFactory stateFactory)
    : base (currentContext, stateFactory) 
    {
        m_MovementHandler = m_Context.MovementHandler;
        m_CharacterController = m_Context.CharacterController;
        m_Player = GameManager.Instance.Player.transform;

        m_PlayerSettings = m_Context.PlayerSettings;
    }
    public override void Enter()
    {
        // Register this IMovementModifier to affect final movement vector
        m_MovementHandler.RegisterModifier(this);
        //Debug.Log("Entered walk state.");
    }
    public override void Exit()
    {
        m_MovementHandler.RemoveModifier(this);
    }
    public override void Tick()
    {
        HandleWalk();
        /* Wait for m_CheckSwitchDelay before checking to switch states,
         * because it will sometimes take some time to not be zero. */
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
        
    }
    public override void CheckSwitchStates()
    {
        // Fully deaccelerated, then switch to idle
        if (MovementValue == Vector3.zero)
            SwitchState(m_Factory.Idle());
    }

    private void HandleWalk()
    {
        // Holds the target velocity we want to accelerate towards
        Vector3 targetVelocity = m_Context.MovementInput;

        targetVelocity.Normalize();
        targetVelocity *= m_PlayerSettings.MovementSpeed;

        m_CurrentMovementVelocity = Vector3.SmoothDamp(m_CurrentMovementVelocity, targetVelocity, ref m_CurrentMovementVelocityDelta, m_PlayerSettings.MovementAcceleration);
        Vector3 translatedVelocity = m_Player.forward * m_CurrentMovementVelocity.z + m_Player.right * m_CurrentMovementVelocity.x;

        if (translatedVelocity.magnitude < m_PlayerSettings.MovementAccelerationResetThreshold)
            translatedVelocity = Vector3.zero;

        MovementValue = translatedVelocity;
    }
}
