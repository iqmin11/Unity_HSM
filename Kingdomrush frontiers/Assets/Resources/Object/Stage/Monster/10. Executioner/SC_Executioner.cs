using Assets.Scenes.Object.Base;
using Assets.Scenes.Object.Stage.ContentsEnum;
using UnityEngine;

public class SC_Executioner : SC_BaseMonster
{
    protected override void Awake()
    {
        HpBarLocalPos = new Vector3(0f, 0.8f, 0f);
        base.Awake();
        SoundManager_AwakeParentInst();
    }
    protected override void OnDestroy()
    {
        SoundManager_OnDestroyParentInst();
        base.OnDestroy();
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

    // Sound /////////////////////////////////////////////

    protected override void AttackAction()
    {
        base.AttackAction();
        SoundManagerSetting.PlaySound("Attack");
    }

    protected override void DeathStateStart()
    {
        base.DeathStateStart();
        SoundManagerSetting.PlaySound("Death");
    }

    static private GameObject SoundManagerInst;
    static private SC_SoundManager SoundManagerSetting;

    private void SoundManager_AwakeParentInst()
    {
        InitSoundManager();
        InitSoundClips();
        ++SoundManagerSetting.RefCount;
    }

    private void InitSoundManager()
    {
        if (SoundManagerInst != null)
        {
            return;
        }

        SoundManagerInst = new GameObject("Executioner_SoundManager");
        SoundManagerSetting = SoundManagerInst.AddComponent<SC_SoundManager>();
    }

    private void InitSoundClips()
    {
        if (SoundManagerSetting.ClipCount > 0)
        {
            return;
        }

        SoundManagerSetting.AddSoundClip("Death", "Sounds/PlayStage/Enemies/Sound_EnemyBigDead");
        SoundManagerSetting.AddSoundClip("Attack", "Sounds/PlayStage/Enemies/Sound_CommonAreaHit");
    }

    private void SoundManager_OnDestroyParentInst()
    {
        if (--SoundManagerSetting.RefCount == 0)
        {
            Destroy(SoundManagerInst);
            SoundManagerSetting = null;
            SoundManagerInst = null; ;
        }
    }
}
