using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_RangedShooter : SC_BaseShooter
{
    // Start is called before the first frame update
    override protected void Start()
    {

    }

    protected override void Attack()
    {
        Debug.Log("Shooter Attack");
    }
}
