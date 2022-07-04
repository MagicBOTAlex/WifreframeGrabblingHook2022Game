using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private ForceReciever m_ForceReciever = null;
    private CharacterController m_CharacterController = null;

    [Header("Jump Settings")]
    [SerializeField] private float m_JumpForce = 5f;

    private bool m_IsInJump = false;
    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_ForceReciever = GetComponent<ForceReciever>();
    }

    private void Update()
    {
        // Process jumping, and update player state
        GameManager.PlayerState.jumping = ProcessJump();
    }

    private bool ProcessJump()
    {
        if (Input.GetButtonDown("Jump") && m_CharacterController.isGrounded)
        {
            m_ForceReciever.AddForce(Vector3.up * m_JumpForce);
            m_IsInJump = true;
            return true;
        }
        // While we haven't hit the ground after a jump,
        // then report that the player is still jumping.
        if (m_IsInJump && !m_CharacterController.isGrounded)
            return true;

        return false;
    }
}
