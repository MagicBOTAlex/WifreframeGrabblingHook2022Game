using UnityEngine;
public class GrapplingGunSettings : MonoBehaviour
{
    [Header("Grappling Gun Settings")]
    [SerializeField] public float ScoutCastRadius = 1f;
    [SerializeField] public float MaxCastDist = 200f;
    [SerializeField] public int CastLayerIdx = 6;
    // The offset in which to apply to the cannon's forward vector so the model
    // points it hook in the direction of the forward vector. Should be Vector3.zero
    // if the model is modelled correctly.
    //[SerializeField] public Vector3 CannonForwardVecOffset = new Vector3(0f, 90f, 0f);
    [SerializeField] public float CannonRotationSpeed = 50f;
    [SerializeField] public float HookSpeed = 50f;
    [SerializeField] public float HookAttachThreshold = 0.5f;

}
