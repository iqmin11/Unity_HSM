using Assets.Scenes.Object.Base;
using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_DuneTerror : SC_BaseMonster
{
    override protected void SetData()
    {
        Data.SetData(MonsterEnum.DuneTerror);
    }
    override protected void SetColRadius()
    {
        Monster2DCol.radius = ColRadius;
        Monster3DCol.radius = ColRadius;
    }
    override protected void StateInit()
    {
        IdleStateInit();
        MoveStateInit();
        DeathStateInit();
        AttackStateInit();
    }

    private readonly float ColRadius = MyMath.CentimeterToMeter(18.0f);
}
