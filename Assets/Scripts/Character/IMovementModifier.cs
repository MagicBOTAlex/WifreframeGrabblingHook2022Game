using UnityEngine;

/* Every class that extends with this interface, 
 * can contribe to the movement of the character
 * by setting the 'MovementValue' vector to a
 * velocity. The character will take the sum of 
 * all the movement modifiers and apply it.
*/
public interface IMovementModifier 
{
    Vector3 MovementValue { get; }
}

