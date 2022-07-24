using UnityEngine;
public class PlayerSettings : MonoBehaviour
{
    [Header("Jump Settings")]
    public float JumpForce = 5f;

    [Header("Walk Settings")]
    [SerializeField] public float MovementSpeed = 5f;
    [SerializeField] public float MovementAcceleration = 0.1f;
    [SerializeField] public float MovementAccelerationResetThreshold = 0.2f;
    [SerializeField] public bool EnableWalkWhileHooking = false;
    [SerializeField] public bool EnableWalkWhileJumping = true;
}
