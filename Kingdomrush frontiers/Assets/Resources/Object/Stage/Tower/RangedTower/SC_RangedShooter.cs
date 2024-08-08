using System.Collections.Generic;
using UnityEngine;

sealed public class SC_RangedShooter : SC_BaseShooter
{
    protected override void Awake()
    {
        base.Awake();
        if (RangedShooterAnimators.Count == 0)
        {
            RangedShooterAnimators.Add(Resources.Load<AnimatorOverrideController>("Object/Stage/Tower/RangedTower/AC_RangedLv1"));
            RangedShooterAnimators.Add(Resources.Load<AnimatorOverrideController>("Object/Stage/Tower/RangedTower/AC_RangedLv2"));
            RangedShooterAnimators.Add(Resources.Load<AnimatorOverrideController>("Object/Stage/Tower/RangedTower/AC_RangedLv3"));
            RangedShooterAnimators.Add(Resources.Load<AnimatorOverrideController>("Object/Stage/Tower/RangedTower/AC_RangedLv4"));
        }

        ShooterAnimator.runtimeAnimatorController = RangedShooterAnimators[0];
        SoundManager_AwakeParentInst();
    }

    private void OnDestroy()
    {
        SoundManager_OnDestroyParentInst();
    }
    public override void ChangeShooter()
    {
        ShooterAnimator.runtimeAnimatorController = RangedShooterAnimators[Data.Level - 1];
    }

    protected override void Attack()
    {
        base.Attack();
        SoundManagerSetting.PlaySound(Random.Range(0,2).ToString());
    }

    // Start is called before the first frame update

    static readonly List<AnimatorOverrideController> RangedShooterAnimators = new List<AnimatorOverrideController>();

    // Sound /////////////////////////////////////////////

    private GameObject SoundManagerInst;
    private SC_SoundManager SoundManagerSetting;

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

        SoundManagerInst = new GameObject("RangedShooter_SoundManager");
        SoundManagerSetting = SoundManagerInst.AddComponent<SC_SoundManager>();
    }

    private void InitSoundClips()
    {
        if (SoundManagerSetting.ClipCount > 0)
        {
            return;
        }

        SoundManagerSetting.AddSoundClip("0", "Sounds/PlayStage/Tower/Ranged/Sound_ArrowRelease2");
        SoundManagerSetting.AddSoundClip("1", "Sounds/PlayStage/Tower/Ranged/Sound_ArrowRelease3");
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
