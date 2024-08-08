using Assets.Scenes.Object.Base;
using Assets.Scenes.Object.Stage.ContentsEnum;
using UnityEngine;

public class SC_Immortal : SC_BaseMonster
{
    protected override void Awake()
    {
        HpBarLocalPos = new Vector3(0f, 0.4f, 0f);
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

    // Sound /////////////////////////////////////////////
    protected override void DeathStateStart()
    {
        base.DeathStateStart();
        SoundManagerSetting.PlaySound("Death" + Random.Range(0, 4).ToString());
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

        SoundManagerInst = new GameObject("Immortal_SoundManager");
        SoundManagerSetting = SoundManagerInst.AddComponent<SC_SoundManager>();
    }

    private void InitSoundClips()
    {
        if (SoundManagerSetting.ClipCount > 0)
        {
            return;
        }

        SoundManagerSetting.AddSoundClip("Death0", "Sounds/PlayStage/Enemies/Sound_HumanDead1");
        SoundManagerSetting.AddSoundClip("Death1", "Sounds/PlayStage/Enemies/Sound_HumanDead2");
        SoundManagerSetting.AddSoundClip("Death2", "Sounds/PlayStage/Enemies/Sound_HumanDead3");
        SoundManagerSetting.AddSoundClip("Death3", "Sounds/PlayStage/Enemies/Sound_HumanDead4");
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
