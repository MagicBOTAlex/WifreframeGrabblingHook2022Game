using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGunBehavior : MonoBehaviour, IGrapplingGunBehavior
{
    private GrapplingGunSettings m_GrapplingGunSettings = null;
    private GrapplingGunContext m_Context = null;
    private GrapplingGunSettings GrapplingGunSettings { get { return m_GrapplingGunSettings; } set { m_GrapplingGunSettings = value; }}
    private GrapplingGunContext Context { get { return m_Context; } set { m_Context = value; }}

    public GameObject GetClosestGrabbableObject()
    {
        RaycastHit hit;
        // casts a sphereCast from the main camera and checks if it hits.
        // if the ray hits then it will check if the object that was hit has a "GrabblebleObject" tag
        if (Physics.SphereCast(Context.Camera.transform.position, GrapplingGunSettings.ScoutCastRadius, Context.Camera.transform.forward, out hit, 100.0f))
        {
            if (!hit.collider.CompareTag("GrabblebleObject"))
                return null;
            //Debug.DrawLine(MainCamera.transform.position, HitInfo.point);   // draws a debug line that shows the ray when hitting an object
            Debug.Log(hit.transform.position);
            return hit.transform.gameObject;
        }
        return null;
    }

    public void SetContext(GrapplingGunContext currentContext)
    {
        Context = currentContext;
    }

    public void SetSettings(GrapplingGunSettings currentSettings)
    {
        GrapplingGunSettings = currentSettings;
    }
}
