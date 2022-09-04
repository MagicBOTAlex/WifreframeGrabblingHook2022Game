using UnityEngine;
public interface IPostModifierHook
{
    /// <summary>
    /// This method will get called when MovementHandler,
    /// have added all movement modifiers. This method
    /// have the abillity to change the final applied velocity
    /// based on what would have been applied without the hook.
    /// </summary>
    /// <param name="movementVelocity">The velocity which will
    /// be applied in this frame.</param>
    /// <returns>A Vector3 which will be added to movementVelocity.</returns>
    public Vector3 PostModifierHook(Vector3 movementVelocity);
}
