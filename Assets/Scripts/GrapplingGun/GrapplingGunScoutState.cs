using UnityEngine;
public class GrapplingGunScoutState : GrapplingGunBaseState
{
    // Pass the context to the abstract super class GrapplingGunBaseState
    public GrapplingGunScoutState(GrapplingGunContext currentContext) : base(currentContext) 
    {
        m_GrapplingGunSettings = Context.GrapplingGunSettings;

        // Apply state specific settings

        m_GrapplingGunBehavior = Context.GrapplingGun.GetComponent<GrapplingGunBehavior>();
        m_GrapplingGunBehavior.SetContext(Context);
        m_GrapplingGunBehavior.SetSettings(m_GrapplingGunSettings);
    }
    private GrapplingGunBehavior m_GrapplingGunBehavior = null;

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

    protected override void CheckSwitchStates()
    {
        if (m_TargetObject)
        {
            Context.SwitchState(new GrapplingGunLockedState(Context, m_TargetObject));
        }
    }

    /// <summary>
    /// Gets called every frame while GrapplingGunScoutState
    /// is the current active state.
    /// </summary>
    private void Scout()
    {
        m_TargetObject = m_GrapplingGunBehavior.GetClosestGrabbableObject();
    }
}
