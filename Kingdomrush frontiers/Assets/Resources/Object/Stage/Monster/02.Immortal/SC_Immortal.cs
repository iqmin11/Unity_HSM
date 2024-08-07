using Assets.Scenes.Object.Base;
using Assets.Scenes.Object.Stage.ContentsEnum;
using UnityEngine;

public class SC_Immortal : SC_BaseMonster
{
    protected override void Awake()
    {
        HpBarLocalPos = new Vector3(0f, 0.4f, 0f);
        base.Awake();
    }
    override protected void SetData()
    {
        Data.SetData(MonsterEnum.Immortal);
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
