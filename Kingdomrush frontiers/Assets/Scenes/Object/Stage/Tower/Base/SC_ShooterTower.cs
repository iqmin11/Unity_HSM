using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class SC_ShooterTower : SC_BaseShootingTower
{
    abstract protected void TransitionTargetInfoToShooter();
    protected override void CalTargetPos()
    {
        base.CalTargetPos();
        TransitionTargetInfoToShooter();
    }

}
