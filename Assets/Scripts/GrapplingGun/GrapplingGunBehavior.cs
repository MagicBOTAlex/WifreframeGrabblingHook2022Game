using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GrapplingGunBehavior : GrapplingGunBaseState 
{
    protected GrapplingGunBehavior(GrapplingGun currentContext) : base(currentContext) {}
}
