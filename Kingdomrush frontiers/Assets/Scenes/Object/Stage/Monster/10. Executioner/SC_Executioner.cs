using Assets.Scenes.Object.Base;
using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Executioner : SC_BaseMonster
{
    protected override void Awake()
    {
        HpBarLocalPos = new Vector3(0f, 0.8f, 0f);
        base.Awake();
    }
    protected override void SetData()
    {
        Data.SetData(MonsterEnum.Executioner);
    }
    protected override void SetColRadius()
    {
        Monster2DCol.radius = ColRadius;
        Monster3DCol.radius = ColRadius;
    }
    protected override void StateInit()
    {
        IdleStateInit();
        MoveStateInit();
        DeathStateInit();
        AttackStateInit();
    }

    private readonly float ColRadius = MyMath.CentimeterToMeter(18.0f);
}
