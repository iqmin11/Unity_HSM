using Assets.Scenes.Object.Base;
using Assets.Scenes.Object.Base.MyInterface;
using Assets.Scenes.Object.Stage.ContentsEnum;
using UnityEngine;

public class SC_SandWraith : SC_BaseMonster, ISoundManager
{
    protected override void Awake()
    {
        base.Awake();
        SoundManager_AwakeParentInst();
    }
    protected override void OnDestroy()
    {
        SoundManager_OnDestroyParentInst();
        base.OnDestroy();
    }
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

    // Sound /////////////////////////////////////////////

    protected override void DeathStateStart()
    {
        base.DeathStateStart();
        SoundManagerSetting.PlaySound("Death");
    }

    static private GameObject SoundManagerInst;
    static private SC_SoundManager SoundManagerSetting;

    public void SoundManager_AwakeParentInst()
    {
        InitSoundManager();
        InitSoundClips();
        ++SoundManagerSetting.RefCount;
    }

    public void InitSoundManager()
    {
        if (SoundManagerInst != null)
        {
            return;
        }

        SoundManagerInst = new GameObject("SandWraith_SoundManager");
        SoundManagerSetting = SoundManagerInst.AddComponent<SC_SoundManager>();
    }

    public void InitSoundClips()
    {
        if (SoundManagerSetting.ClipCount > 0)
        {
            return;
        }

        SoundManagerSetting.AddSoundClip("Death", "Sounds/PlayStage/Enemies/KRF_sfx_vodoo_kamikazelanza");
    }

    public void SoundManager_OnDestroyParentInst()
    {
        if (--SoundManagerSetting.RefCount == 0)
        {
            Destroy(SoundManagerInst);
            SoundManagerSetting = null;
            SoundManagerInst = null; ;
        }
    }
}
