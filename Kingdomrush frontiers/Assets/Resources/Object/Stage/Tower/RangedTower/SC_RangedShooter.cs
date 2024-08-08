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
    }

    public override void ChangeShooter()
    {
        ShooterAnimator.runtimeAnimatorController = RangedShooterAnimators[Data.Level - 1];
    }

    protected override void Attack()
    {
        base.Attack();
        PlaySound(Random.Range(0,2).ToString());
    }

    // Start is called before the first frame update

    static readonly List<AnimatorOverrideController> RangedShooterAnimators = new List<AnimatorOverrideController>();

    // Sound /////////////////////////////////////////////

    public override void InitSoundClips()
    {
        AddAudioClip("0", "Sounds/PlayStage/Tower/Ranged/Sound_ArrowRelease2");
        AddAudioClip("1", "Sounds/PlayStage/Tower/Ranged/Sound_ArrowRelease3");
    }
}
