using Assets.Scenes.Object.Base;
using Assets.Scenes.Object.Stage.ContentsEnum;

public class SC_SandWraith : SC_BaseMonster
{
    override protected void SetData()
    {
        Data.SetData(MonsterEnum.SandWraith);
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
