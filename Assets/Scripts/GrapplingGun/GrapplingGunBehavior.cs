/*
/GrapplingGunBehavior.cs is a class that will handle generic grappling gun behaviour,
which includes things like finding the right object to either swing or zip towards.

That way every state can use this code and not duplicate generic common code.
*/
using UnityEngine;

public class GrapplingGunBehavior : MonoBehaviour, IGrapplingGunBehavior
{
    private GrapplingGunSettings m_GrapplingGunSettings = null;
    private GrapplingGunContext m_Context = null;
    private GrapplingGunSettings GrapplingGunSettings { get { return m_GrapplingGunSettings; } set { m_GrapplingGunSettings = value; }}
    private GrapplingGunContext Context { get { return m_Context; } set { m_Context = value; }}

    /// <summary>
    /// Gets the closests grabbable object in the player's look direction.
    /// Returns the GameObject to the closest object or null if no object were found.
    /// </summary>
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

    /// <summary>
    /// Gets the closests grabbable point in the player's look direction.
    /// Returns the position to the point where the hook can attach
    /// to or Vector3.zero if no point were found.
    /// </summary>
    public Vector3 GetClosestGrabbablePoint()
    {
        RaycastHit hit;

        /*
        Check if the player is directly looking at a hookable position, and if so don't extend the search.
        */
        if (Physics.Raycast(    Context.Camera.transform.position, Context.Camera.transform.forward, out hit,
                                GrapplingGunSettings.MaxCastDist, 1 << GrapplingGunSettings.CastLayerIdx))
        {
            return hit.point;
        }
        /* 
        If the player is not directly looking at a hookable position then extend the search by
        casting a sphere with a radius of GrapplingGunSettings.ScoutCastRadius and max distance of
        GrapplingGunSettings.MaxCastDist. The sphere cast starts at the camera's position and travels in
        the direction of it's facing direction (blue basevector). The sphere cast will only collide
        with objects that are on the same layer as defined by GrapplingGunSettings.CastLayerIdx. The left
        shift bitwise operation is for creating a bitmask to represent the layer(s) it should collide with.
        See: https://docs.unity3d.com/Manual/use-layers.html
        */
        if (Physics.SphereCast( Context.Camera.transform.position, GrapplingGunSettings.ScoutCastRadius, 
                                Context.Camera.transform.forward, out hit, GrapplingGunSettings.MaxCastDist, 
                                1 << GrapplingGunSettings.CastLayerIdx))
        {
            // If execution gets to here, it means that the spherecast has hit a object that's hookable
            //Debug.Log($"Locked gun to target at: {hit.point}");
            return hit.point;
        }
        return Vector3.zero;
    }

    public void DrawWireToHook()
    {
        // Enable the lr if it's disabled
        //Debug.Log(Context.LineRenderer.enabled);
        if (!Context.LineRenderer.enabled)
            Context.LineRenderer.enabled = true;

        // Set the start of the line to the grappling gun's position
        //Debug.Log($"Line length: {(Context.GrapplingGun.transform.position - Context.Hook.transform.position).magnitude}");
        Context.LineRenderer.SetPosition(0, Context.GrapplingGun.transform.position);
        Context.LineRenderer.SetPosition(1, Context.Hook.transform.position);
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
