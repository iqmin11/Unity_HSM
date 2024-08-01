using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_RangedShooter : SC_BaseShooter
{
    // Start is called before the first frame update
    public void AttackEndEvent()
    {
        State = ShooterState.Idle;
    }

}
