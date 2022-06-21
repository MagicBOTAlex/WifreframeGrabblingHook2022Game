using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    private CharacterController m_CharacterController = null;

    // List containing all movement modifiers that will influence the final applied movement.
    private readonly List<IMovementModifier> m_MovementModifiers = new List<IMovementModifier>();
    public void RegisterModifier(IMovementModifier modifier) { m_MovementModifiers.Add(modifier); }
    public void RemoveModifier(IMovementModifier modifier) { m_MovementModifiers.Remove(modifier); }

    private void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // Hold the sum of all movement modifiers
        Vector3 movementVelocity = Vector3.zero;

        // Sum up all the modifiers to give the final movement vector
        for (int i = 0; i < m_MovementModifiers.Count; ++i)
        {
            movementVelocity += m_MovementModifiers[i].MovementValue;
        }

        // Apply final movement on character
        m_CharacterController.Move(movementVelocity * Time.deltaTime);
    }
}
