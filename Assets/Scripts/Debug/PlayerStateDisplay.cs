using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateDisplay : MonoBehaviour
{
    public Text m_RootStateTextField;
    public Text m_SubStateTextField;

    PlayerStateManager m_PSM;

    void Start()
    {
        m_PSM = GameManager.Instance.Player.GetComponent<PlayerStateManager>();
    }
    void Update()
    {
        string rootState = m_PSM.CurrentState.GetType().Name;
        string subState = m_PSM.CurrentState.CurrentSubState.GetType().Name;

        m_RootStateTextField.text = rootState;
        m_SubStateTextField.text = subState;
    }
}
