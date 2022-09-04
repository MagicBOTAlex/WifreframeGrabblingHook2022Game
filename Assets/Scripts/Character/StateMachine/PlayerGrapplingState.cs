using UnityEngine;
public class PlayerGrapplingState : PlayerBaseState, IMovementModifier, IPostModifierHook
{
     /* Pass context and state factory to super class constructor,
     * to access the context and factory in this state method. */
    public PlayerGrapplingState(PlayerStateManager currentContext, PlayerStateFactory stateFactory, Vector3 targetPosition)
    : base (currentContext, stateFactory)
    {
        IsRootState = true;
        m_TargetPosition = targetPosition;
        
        m_ForceReciever = m_Context.ForceReciever;
        m_MovementHandler = m_Context.MovementHandler;
        
    }

    private bool m_ShouldDrag = false;
    private ForceReciever m_ForceReciever = null;
    private MovementHandler m_MovementHandler = null;
    private Vector3 m_TargetPosition = Vector3.zero;
    private Gravity m_GravityHandler = null;
    Vector3 v;
    public bool ShouldDrag { get { return m_ShouldDrag; } set { m_ShouldDrag = value; }}

    public Vector3 MovementValue { get; private set; }

    public override void Enter()
    {
        m_GravityHandler = m_Context.Player.GetComponent<Gravity>();
        m_MovementHandler.RegisterModifier(this);
        m_MovementHandler.RegisterPostModifierHook(this);
        InitSubState();
    }
    public override void Exit()
    {
        m_MovementHandler.RemoveModifier(this);
        m_MovementHandler.RemovePostModifierHook();
    }
    public override void Tick()
    {
        CheckSwitchStates();
    }

    private Vector3 DragPlayer(Vector3 movementVelocity)
    {
        if (!ShouldDrag)
            return Vector3.zero;


        return Vector3.zero;
        /*
        // plus previous v or not?
        Vector3 dirToTarget = m_TargetPosition + MovementValue - m_Context.Player.transform.position;
        //MovementValue = m_Context.MovementHandler.m_PreviousVelocity.normalized * m_Context.PlayerSettings.DragBoost;

        MovementValue += m_Context.CharacterController.velocity.normalized * m_Context.PlayerSettings.DragBoost * Time.deltaTime;
        Debug.Log($"boost: {MovementValue}");

        // Check if outside of the radius of the virtual swing circle
        if (dirToTarget.magnitude > m_Context.TetherRadius)
        {
            // Update player's position to be on the swing circle
            MovementValue = dirToTarget.normalized * (dirToTarget.magnitude - m_Context.TetherRadius) * m_Context.PlayerSettings.TensionScalar;
            
            //Debug.Log($"Applying {MovementValue}");
        }
        // TODO rotate vector so it points in the player's forward ish direction, then apply dragboost

        //m_Context.CharacterController.Move((MovementValue) * Time.deltaTime);
        
        
        //MovementValue += new Vector3(Mathf.Pow(v.x, 2)/m_Context.TetherRadius, Mathf.Pow(v.y, 2)/m_Context.TetherRadius, Mathf.Pow(v.z, 2)/m_Context.TetherRadius);
        */
    }

    public override void InitSubState()
    {
        if (CurrentSubState != null)
            return;
        // Should just default to walking state
        SetSubState(Factory.Walking());
    }
    public override void CheckSwitchStates()
    {
        // Switch to jump root state
        if (m_Context.IsJumpPressed)
        {
            /* If walking while transitioning, then pass the 
             * walk substate to the new root state, instead of
             * creating a new instance. */
            if (CurrentSubState is PlayerWalkingState)
                SwitchState(Factory.Jumping(), CurrentSubState);
            else
                SwitchState(Factory.Jumping());
            
            // If the grappling gun was grappling then cancel it through it's context
            m_Context.GrapplingGun.GetComponent<GrapplingGunContext>().CancelGrapple();

        }
    }


    public Vector3 PostModifierHook(Vector3 movementVelocity)
    {
        return DragPlayer(movementVelocity);
    }
}
