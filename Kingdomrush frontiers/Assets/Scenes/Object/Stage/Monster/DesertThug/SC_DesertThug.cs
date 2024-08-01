using Assets.Scenes.Object.Base;
using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

sealed public class SC_DesertThug : SC_BaseMonster
{
    protected override void Awake()
    {
        base.Awake();
    }

    override protected void SetData()
    {
        Data.SetData(MonsterEnum.DesertThug);
    }
    override protected void SetColRadius()
    {
        MonsterCol.radius = ColRadius;
    }
    override protected void StateInit()
    {
        MoveStateInit();
    }
    private readonly float ColRadius = MyMath.CentimeterToMeter(18.0f);
}
