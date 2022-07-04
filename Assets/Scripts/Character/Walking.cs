using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Walking : MonoBehaviour, IMovementModifier
{
    private CharacterController m_CharacterController = null;
    private MovementHandler m_MovementHandler = null;
    public Vector3 MovementValue { get; private set; }

    [Header("Walk Settings")]
    [SerializeField] private float m_MovementSpeed = 5f;
    [SerializeField] private float m_MovementAcceleration = 0.1f;
    [SerializeField] private float m_MovementAccelerationResetThreshold = 0.2f;
    [SerializeField] private bool m_EnableWalkWhileHooking = false;
    [SerializeField] private bool m_EnableWalkWhileJumping = true;

    // The actual current velocity, after applying acceleration
    private Vector3 m_CurrentMovementVelocity = Vector3.zero;
    // The velocity difference used by the sigmoid function in smoothdamp
    private Vector3 m_CurrentMovementVelocityDelta = Vector3.zero;

    private void OnEnable() { m_MovementHandler.RegisterModifier(this); }
    private void OnDisable() { m_MovementHandler.RemoveModifier(this); }

    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_MovementHandler = GetComponent<MovementHandler>();
        Assert.IsNotNull(m_CharacterController);
        Assert.IsNotNull(m_MovementHandler);
    }

    private void Update()
    {
        // Handle walk movement and update our global player state with the current state.
        GameManager.PlayerState.walking = Walk();
    }

    private bool Walk()
    {
        if (GameManager.PlayerState.hooking && !m_EnableWalkWhileHooking)
            return false;

        if (GameManager.PlayerState.jumping && !m_EnableWalkWhileJumping)
            return false;

        // Holds the target velocity we want to accelerate towards
        Vector3 targetVelocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        targetVelocity.Normalize();
        targetVelocity *= m_MovementSpeed;

        m_CurrentMovementVelocity = Vector3.SmoothDamp(m_CurrentMovementVelocity, targetVelocity, ref m_CurrentMovementVelocityDelta, m_MovementAcceleration);
        Vector3 translatedVelocity = transform.forward * m_CurrentMovementVelocity.z + transform.right * m_CurrentMovementVelocity.x;

        if (translatedVelocity.magnitude < m_MovementAccelerationResetThreshold)
            translatedVelocity = Vector3.zero;

        MovementValue = translatedVelocity;

        if (translatedVelocity == Vector3.zero)
            return false;

        return true;
    }
}
