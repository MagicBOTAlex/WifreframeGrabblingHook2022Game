using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour, IMovementModifier
{
    // NOTE: maybe late integrate this with state machine
    private MovementHandler m_MovementHandler = null;
    private CharacterController m_CharacterController = null;
    private PlayerStateManager m_PlayerCtx = null;

    [Header("Gravity Settings")]
    [SerializeField] private float m_GravityStrength = 9.82f;
    [SerializeField] private float m_GroundedPullStrength = 20f;

    public Vector3 MovementValue { get; private set; }
    private bool m_GravityEnabled = true;
    public bool GravityEnabled { get { return m_GravityEnabled; } set { m_GravityEnabled = value; }}

    private bool m_WasGroundedLastFrame;
    private bool m_WasGrapplingLastFrame;

    private void OnEnable() { m_MovementHandler.RegisterModifier(this); }
    private void OnDisable() { m_MovementHandler.RegisterModifier(this); }

    private void Awake()
    {
        m_MovementHandler = GetComponent<MovementHandler>();
        m_CharacterController = GetComponent<CharacterController>();
        
    }

    private void Start()
    {
        m_PlayerCtx = GameManager.Instance.Player.GetComponent<PlayerStateManager>();
    }

    private void Update()
    {
        HandleGravity();
    }

    private void HandleGravity()
    {
        
        // Apply force downwards when on the ground, this is important
        // so we go smoothly down ramps, because without this we would
        // bump into the ramp by the normal gravity strength which is visible
        bool isGrappling = (m_PlayerCtx.GrapplingGunContext.CurrentState is GrapplingGunAttachedState);
        bool isJumping = Input.GetButtonDown("Jump");


        if (m_CharacterController.isGrounded && !Input.GetButtonDown("Jump") && !isGrappling)
        {
            MovementValue = new Vector3(MovementValue.x, -m_GroundedPullStrength, MovementValue.z);
        }
        else if (isGrappling)
            MovementValue = new Vector3(MovementValue.x, -m_GravityStrength, MovementValue.z);
        else if (m_WasGroundedLastFrame)
            MovementValue = Vector3.zero;
        else if (m_GravityEnabled)
            MovementValue = new Vector3(MovementValue.x, MovementValue.y - m_GravityStrength * Time.deltaTime, MovementValue.z);
        else
            MovementValue = Vector3.zero;

        //Debug.Log($"Gravity v: {MovementValue}");
        m_WasGroundedLastFrame = m_CharacterController.isGrounded;
        m_WasGrapplingLastFrame = isGrappling;
    }
}
