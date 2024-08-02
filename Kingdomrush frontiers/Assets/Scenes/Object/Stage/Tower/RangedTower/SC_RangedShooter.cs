using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

sealed public class SC_RangedShooter : SC_BaseShooter
{
    protected override void Awake()
    {
        base.Awake();
        if (RangedShooterAnimators.Count == 0)
        {
            RangedShooterAnimators.Add((AnimatorOverrideController)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Object/Stage/Tower/RangedTower/AC_RangedLv1.overrideController", typeof(AnimatorOverrideController)));
            RangedShooterAnimators.Add((AnimatorOverrideController)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Object/Stage/Tower/RangedTower/AC_RangedLv2.overrideController", typeof(AnimatorOverrideController)));
            RangedShooterAnimators.Add((AnimatorOverrideController)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Object/Stage/Tower/RangedTower/AC_RangedLv3.overrideController", typeof(AnimatorOverrideController)));
            RangedShooterAnimators.Add((AnimatorOverrideController)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Object/Stage/Tower/RangedTower/AC_RangedLv4.overrideController", typeof(AnimatorOverrideController)));
        }

        ShooterAnimator.runtimeAnimatorController = RangedShooterAnimators[0];
    }
    public override void ChangeShooter()
    {
        ShooterAnimator.runtimeAnimatorController = RangedShooterAnimators[Data.Level - 1];
    }

    // Start is called before the first frame update
    public void AttackEndEvent()
    {
        State = ShooterState.Idle;
    }

    static readonly List<AnimatorOverrideController> RangedShooterAnimators = new List<AnimatorOverrideController>();
}
