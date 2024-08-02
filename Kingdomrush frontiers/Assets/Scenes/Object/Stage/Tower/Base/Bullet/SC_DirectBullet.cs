using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

abstract public class SC_DirectBullet : SC_BaseBullet
{
    override protected void CalBulletTransform()
    {
        Vector4 Pos = Vector4.Lerp(ShooterPos, TargetPos, Ratio);
        gameObject.transform.position = Pos;
        CalBulletRot();
    }

    protected override void CalBulletRot()
    {
        float ZDeg = Mathf.Atan2(TargetPos.y - ShooterPos.y, TargetPos.x - ShooterPos.x) * Mathf.Rad2Deg;
        Vector4 V4Deg = new Vector4(0, 0, ZDeg, 1);
        gameObject.transform.eulerAngles = V4Deg;
    }
}
