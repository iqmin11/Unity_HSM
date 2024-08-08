using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MeleeFighter : SC_BaseFighter
{
    protected override void Awake()
    {
        if (MeleeFighterAnimatorCache.Count == 0)
        {
            MeleeFighterAnimatorCache.Add(Resources.Load<AnimatorOverrideController>("Object/Stage/Tower/MeleeTower/MeleeFighter/Lv1/AC_MeleeFighterLv1"));
            MeleeFighterAnimatorCache.Add(Resources.Load<AnimatorOverrideController>("Object/Stage/Tower/MeleeTower/MeleeFighter/Lv2/AC_MeleeFighterLv2"));
            MeleeFighterAnimatorCache.Add(Resources.Load<AnimatorOverrideController>("Object/Stage/Tower/MeleeTower/MeleeFighter/Lv3/AC_MeleeFighterLv3"));
            MeleeFighterAnimatorCache.Add(Resources.Load<AnimatorOverrideController>("Object/Stage/Tower/MeleeTower/MeleeFighter/Lv4/AC_MeleeFighterLv4"));
        }
        base.Awake();

        FighterAnimator.runtimeAnimatorController = MeleeFighterAnimatorCache[0];
        SoundManager_AwakeParentInst();
    }

    protected void OnDestroy()
    {
        SoundManager_OnDestroyParentInst();
    }

    public void ChangeTower(int TowerLevel)
    {
        Data.SetData(TowerLevel);
        CurHp = Data.Hp;
        FighterAnimator.runtimeAnimatorController = MeleeFighterAnimatorCache[TowerLevel - 1];
    }
    public override void AttackEvent()
    {
        base.AttackEvent();
        SoundManagerSetting.PlaySound(Random.Range(0,5).ToString());
    }

    protected override IEnumerator DeathAndRespawn()
    {
        SoundManagerSetting.PlaySound("Dead" + Random.Range(0,4).ToString());
        return base.DeathAndRespawn();
    }


    static List<AnimatorOverrideController> MeleeFighterAnimatorCache = new List<AnimatorOverrideController>();

    // Sound ////////////////////////////////////////////
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

        SoundManagerInst = new GameObject("MeleeFighter_SoundManager");
        SoundManagerSetting = SoundManagerInst.AddComponent<SC_SoundManager>();
    }

    private void InitSoundClips()
    {
        if (SoundManagerSetting.ClipCount > 0)
        {
            return;
        }

        SoundManagerSetting.AddSoundClip("0", "Sounds/PlayStage/Fighter/Sound_SoldiersFighting-01");
        SoundManagerSetting.AddSoundClip("1", "Sounds/PlayStage/Fighter/Sound_SoldiersFighting-02");
        SoundManagerSetting.AddSoundClip("2", "Sounds/PlayStage/Fighter/Sound_SoldiersFighting-03");
        SoundManagerSetting.AddSoundClip("3", "Sounds/PlayStage/Fighter/Sound_SoldiersFighting-04");
        SoundManagerSetting.AddSoundClip("4", "Sounds/PlayStage/Fighter/Sound_SoldiersFighting-05");

        SoundManagerSetting.AddSoundClip("Dead0", "Sounds/PlayStage/Enemies/Sound_HumanDead1");
        SoundManagerSetting.AddSoundClip("Dead1", "Sounds/PlayStage/Enemies/Sound_HumanDead2");
        SoundManagerSetting.AddSoundClip("Dead2", "Sounds/PlayStage/Enemies/Sound_HumanDead3");
        SoundManagerSetting.AddSoundClip("Dead3", "Sounds/PlayStage/Enemies/Sound_HumanDead4");
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
