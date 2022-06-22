using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReciever : MonoBehaviour, IMovementModifier
{
    private MovementHandler m_MovementHandler = null;
    public Vector3 MovementValue { get; private set; }

    [Header("Force settings")]
    [SerializeField] private float m_Mass = 1f;
    [SerializeField] private float m_Drag = 1f;
    [SerializeField] private float m_ForceMagnitudeResetThreshold = 0.2f;

    private void OnEnable() { m_MovementHandler.RegisterModifier(this); }
    private void OnDisable() { m_MovementHandler.RemoveModifier(this); }

    private void Awake()
    {
        m_MovementHandler = GetComponent<MovementHandler>();
    }

    private void Update()
    {
        if (MovementValue == Vector3.zero)
            return;

        // Reset the force when its below the reset threshold
        if (MovementValue.magnitude < m_ForceMagnitudeResetThreshold) {
            Debug.Log("Reset force velocity");
            MovementValue = Vector3.zero;
        }
        
        // Slowly lerp force to zero to simulate airresistance, friction etc.
        MovementValue = Vector3.Lerp(MovementValue, Vector3.zero, m_Drag * Time.deltaTime);
    }
    public void AddForce(Vector3 force)
    {
        MovementValue += force / m_Mass;
    }
}
