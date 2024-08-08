using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

sealed public class SC_MagicShooter : SC_BaseShooter
{
    protected override void Awake()
    {
        base.Awake();
        if (MagicShooterAnimators.Count == 0)
        {
            MagicShooterAnimators.Add(Resources.Load<AnimatorOverrideController>("Object/Stage/Tower/MagicTower/AnimationClip/Shooter/AC_BaseMagicShooter"));
            MagicShooterAnimators.Add(Resources.Load<AnimatorOverrideController>("Object/Stage/Tower/MagicTower/AnimationClip/Shooter/AC_ArcMage"));
        }

        ShooterAnimator.runtimeAnimatorController = MagicShooterAnimators[0];
    }
    public override void ChangeShooter()
    {
        if (Data.Level <= 3)
        {
            ShooterAnimator.runtimeAnimatorController = MagicShooterAnimators[0];
        }
        else
        {
            ShooterAnimator.runtimeAnimatorController = MagicShooterAnimators[1];
        }
    }

    protected override void Attack()
    {
        base.Attack();
        if(Data.Level < 4)
        {
            PlaySound("Normal" + Random.Range(0,2).ToString());
        }
        else
        {
            PlaySound("ArcMage");
        }
    }

    static readonly List<AnimatorOverrideController> MagicShooterAnimators = new List<AnimatorOverrideController>();

    //Sound
    public override void InitSoundClips()
    {
        AddAudioClip("Normal0", "Sounds/PlayStage/Tower/Magic/Sound_MageShot");
        AddAudioClip("Normal1", "Sounds/PlayStage/Tower/Magic/Sound_Sorcerer");
        AddAudioClip("ArcMage", "Sounds/PlayStage/Tower/Magic/archmage_attack");
    }
}
