using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    private CharacterController m_CharacterController = null;
    public Vector3 m_PreviousVelocity = Vector3.zero;
    public Vector3 m_TargetPosition = Vector3.zero;
    public float m_TetherRadius = 0f;

    // List containing all movement modifiers that will influence the final applied movement.
    private readonly List<IMovementModifier> m_MovementModifiers = new List<IMovementModifier>();
    private IPostModifierHook m_PCHook = null;

    /// <summary>
    /// Registers an instance that implement IMovementModifier.
    /// It will add the the instance's MovementValue to the
    /// final movement velocity.
    /// </summary>
    /// <param name="modifier">The instance of the class that should be added.</param>
    public void RegisterModifier(IMovementModifier modifier) { m_MovementModifiers.Add(modifier); }
    
    /// <summary>
    /// Removes an instance that implement IMovementModifier.
    /// It will remove the the instance's MovementValue 
    /// from the final movement velocity.
    /// </summary>
    /// <param name="modifier">The instance of the class that should be removed.</param>
    public void RemoveModifier(IMovementModifier modifier) { m_MovementModifiers.Remove(modifier); }

    /// <summary>
    /// Registers a single instance that implements IPostModifierHook.
    /// It will call the instance PostModifierHook() method when the
    /// final velocity is about to be applied. This way the instance,
    /// of the hook has a chance of changing the final applied velocity
    /// based on the velocity that was supposed to be applied. Only
    /// one PostModifierHook can be active at a time.
    /// </summary>
    /// <param name="hook">The instance of IPostModifierHook, that will
    /// get it's PostModifierHook() method called.</param>
    public bool RegisterPostModifierHook(IPostModifierHook hook)
    {
        if (m_PCHook != null)
        {
            Debug.LogError("Tried to register a post modifier hook, while one is currently active!");
            return false;
        }
        m_PCHook = hook;
        return true;
    }

    /// <summary>
    /// Removes the Post modifier hook.
    /// </summary>
    public void RemovePostModifierHook()
    {
        m_PCHook = null;
    }

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
            movementVelocity += m_MovementModifiers[i].MovementValue * Time.deltaTime;
        }
        
        // If hook registered, then run and modify final movement velocity
        if (m_PCHook != null)
            movementVelocity += m_PCHook.PostModifierHook(movementVelocity);

        //Debug.Log($"Final movement: {movementVelocity}");

        // Apply final movement on character
        m_CharacterController.Move(movementVelocity);
        m_PreviousVelocity = movementVelocity;
    }
}
