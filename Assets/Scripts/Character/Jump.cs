using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private ForceReciever m_ForceReciever = null;
    private CharacterController m_CharacterController = null;

    [Header("Jump Settings")]
    [SerializeField] private float m_JumpForce = 5f;
    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_ForceReciever = GetComponent<ForceReciever>();
    }

    private void Update()
    {
        ProcessJump();
    }

    private void ProcessJump()
    {
        if (Input.GetButtonDown("Jump") && m_CharacterController.isGrounded)
        {
            m_ForceReciever.AddForce(Vector3.up * m_JumpForce);
        }
    }
}
