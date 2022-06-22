using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour, IMovementModifier
{
    private MovementHandler m_MovementHandler = null;
    private CharacterController m_CharacterController = null;

    [Header("Gravity Settings")]
    [SerializeField] private float m_GravityStrength = 9.82f;
    [SerializeField] private float m_GroundedPullStrength = 20f;

    public Vector3 MovementValue { get; private set; }

    private bool m_WasGroundedLastFrame;

    private void OnEnable() { m_MovementHandler.RegisterModifier(this); }
    private void OnDisable() { m_MovementHandler.RegisterModifier(this); }

    private void Awake()
    {
        m_MovementHandler = GetComponent<MovementHandler>();
        m_CharacterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleGravity();
    }

    private void HandleGravity()
    {
        // Reset gravity velocity when just hitting the ground after being airborne
        if (!m_WasGroundedLastFrame && m_CharacterController.isGrounded)
        {
            Debug.Log("Resetting");
            MovementValue = new Vector3(MovementValue.x, 0f, MovementValue.z);
        }
        
        
        // Apply force downwards when on the ground, this is important
        // so we go smoothly down ramps, because without this we would
        // bump into the ramp by the normal gravity strength which is visible 
        if (m_CharacterController.isGrounded)
            MovementValue = new Vector3(MovementValue.x, -m_GroundedPullStrength, MovementValue.z);
        else if (m_WasGroundedLastFrame)
            MovementValue = Vector3.zero;
        else
            MovementValue = new Vector3(MovementValue.x, MovementValue.y - m_GravityStrength * Time.deltaTime, MovementValue.z);

        m_WasGroundedLastFrame = m_CharacterController.isGrounded;

    }
}
