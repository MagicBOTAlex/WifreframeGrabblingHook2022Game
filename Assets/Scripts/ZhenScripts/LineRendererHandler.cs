using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererHandler : MonoBehaviour
{
    public bool Enabled = true;
    public Transform Target;

    private LineRenderer LR;

    private void Start()
    {
        if (Target is null)
        {
            enabled = false;
            return;
        }

        if (TryGetComponent(typeof(LineRenderer), out Component component))
            LR = (LineRenderer)component;
        else
            LR = gameObject.AddComponent<LineRenderer>();
    }
 
    private void Update()
    {
        if (!Enabled) return;

        if (LR.positionCount == 2)
        {
            LR.SetPosition(0, transform.position);
            LR.SetPosition(1, Target.position);
        }
    }
}
