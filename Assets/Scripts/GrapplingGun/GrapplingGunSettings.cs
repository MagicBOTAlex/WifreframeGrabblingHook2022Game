using UnityEngine;
public class GrapplingGunSettings : MonoBehaviour
{
    [Header("Grappling Gun Settings")]
    [SerializeField] public float ScoutCastRadius = 1f;
    [SerializeField] public float MaxCastDist = 200f;
    [SerializeField] public int CastLayerIdx = 6;

}
