using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrapplingGunStateDisplay : MonoBehaviour
{
    public Text m_StateTextField;

    GrapplingGunContext m_GrapplingGun;

    void Start()
    {
        m_GrapplingGun = GameManager.Instance.GrapplingGun.GetComponent<GrapplingGunContext>();
    }
    void Update()
    {
        string state = m_GrapplingGun.CurrentState.GetType().Name;

        m_StateTextField.text = state;
    }
}
