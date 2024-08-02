using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

sealed public class SC_MagicShooter : SC_BaseShooter
{
    protected override void Awake()
    {
        base.Awake();
        if (MagicShooterAnimators.Count == 0)
        {
            MagicShooterAnimators.Add((AnimatorOverrideController)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Object/Stage/Tower/MagicTower/AnimationClip/Shooter/AC_BaseMagicShooter.overrideController", typeof(AnimatorOverrideController)));
            MagicShooterAnimators.Add((AnimatorOverrideController)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Object/Stage/Tower/MagicTower/AnimationClip/Shooter/AC_ArcMage.overrideController", typeof(AnimatorOverrideController)));
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

    static readonly List<AnimatorOverrideController> MagicShooterAnimators = new List<AnimatorOverrideController>();
}
