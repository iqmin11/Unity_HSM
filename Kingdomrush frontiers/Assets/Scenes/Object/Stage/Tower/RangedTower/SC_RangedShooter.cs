using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_RangedShooter : SC_BaseShooter
{
    // Start is called before the first frame update

    protected override void Attack()
    {
        Debug.Log("Shooter Attack");
    }

    public Vector4 TargetPos
    {
        set
        {
            TargetPosition = value;
        }
    }

    private Vector4 TargetPosition;
    public void AttackEndEvent()
    {
        State = ShooterState.Idle;
    }

}
