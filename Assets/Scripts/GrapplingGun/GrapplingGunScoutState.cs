using UnityEngine;
public class GrapplingGunScoutState : GrapplingGunBaseState, IGrapplingGunBehavior
{
    public GrapplingGunScoutState(GrapplingGunContext currentContext) : base(currentContext) 
    {
        m_GrapplingGunSettings = Context.GrapplingGunSettings;

        // Apply state specific settings

        m_GrapplingGunBehavior = Context.GrapplingGun.GetComponent<GrapplingGunBehavior>();
        m_GrapplingGunBehavior.SetContext(Context);
        m_GrapplingGunBehavior.SetSettings(m_GrapplingGunSettings);
    }

    /* Redirect to behavior class implementation. */
    private GrapplingGunBehavior m_GrapplingGunBehavior = null;
    public GameObject GetClosestGrabbableObject() { return m_GrapplingGunBehavior.GetClosestGrabbableObject(); }

    // This state's local copy of Grappling Gun settings
    private GrapplingGunSettings m_GrapplingGunSettings = null;

    /* Local member variables. */
    private GameObject m_TargetObject = null;

    /* Base state implementations. */
    public override void Enter()
    {
        //Debug.Log("Entered Scout grappling gun state.");
    }
    public override void Exit()
    {

    }

    public override void Tick()
    {
        CheckSwitchStates();
        Scout();
    }

    private void CheckSwitchStates()
    {
        if (m_TargetObject)
        {
            Context.SwitchState(new GrapplingGunLockedState(Context, m_TargetObject));
        }
    }

    private void Scout()
    {
        m_TargetObject = GetClosestGrabbableObject();

    }
}
