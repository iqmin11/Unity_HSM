using System;
using System.Collections;
using System.Collections.Generic;

using Unity.Mathematics;
using UnityEngine;

using Assets.Scenes.Object.Base;

abstract public class SC_HowitzerBullet : SC_BaseBullet
{
    override public void BulletSetting(Vector4 StartPos, Vector4 DestPos)
    {
        base.BulletSetting(StartPos, DestPos);
        CalBezierMid();
    }

    override protected void CalBulletTransform()
    {
        Vector4 M0 = Vector4.Lerp(ShooterPos, Mid0, Ratio);
        Vector4 M1 = Vector4.Lerp(Mid0, Mid1, Ratio);
        Vector4 M2 = Vector4.Lerp(Mid1, TargetPos, Ratio);

        B0 = Vector4.Lerp(M0, M1, Ratio);
        B1 = Vector4.Lerp(M1, M2, Ratio);

        Vector4 Pos = Vector4.Lerp(B0, B1, Ratio);

        gameObject.transform.position = Pos;
        CalRotBulletRot();
    }
    private void CalBezierMid()
    {
        Mid0.x = ShooterPos.x + ((TargetPos.x - ShooterPos.x) / 4);
        Mid0.y = Math.Max(TargetPos.y, ShooterPos.y) + MyMath.CentimeterToMeter(150.0f);
        Mid0.z = ShooterPos.z + ((TargetPos.z - ShooterPos.z) / 4);

        Mid1.x = TargetPos.x - ((TargetPos.x - ShooterPos.x) / 4);
        Mid1.y = Math.Max(TargetPos.y, ShooterPos.y) + MyMath.CentimeterToMeter(150.0f);
        Mid1.z = ShooterPos.z + ((TargetPos.z - ShooterPos.z) / 4);
    }

    private Vector3 Mid0 = Vector3.zero;
    private Vector3 Mid1 = Vector3.zero;

    protected Vector4 B0;
    protected Vector4 B1;
}
