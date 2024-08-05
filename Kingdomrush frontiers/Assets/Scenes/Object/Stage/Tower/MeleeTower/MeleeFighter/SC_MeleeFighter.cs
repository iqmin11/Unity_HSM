using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SC_MeleeFighter : SC_BaseFighter
{
    protected override void Awake()
    {
        if (MeleeFighterAnimatorCache.Count == 0)
        {
            MeleeFighterAnimatorCache.Add((AnimatorOverrideController)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Object/Stage/Tower/MeleeTower/MeleeFighter/Lv1/AC_MeleeFighterLv1.overrideController", typeof(AnimatorOverrideController)));
            MeleeFighterAnimatorCache.Add((AnimatorOverrideController)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Object/Stage/Tower/MeleeTower/MeleeFighter/Lv2/AC_MeleeFighterLv2.overrideController", typeof(AnimatorOverrideController)));
            MeleeFighterAnimatorCache.Add((AnimatorOverrideController)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Object/Stage/Tower/MeleeTower/MeleeFighter/Lv3/AC_MeleeFighterLv3.overrideController", typeof(AnimatorOverrideController)));
            MeleeFighterAnimatorCache.Add((AnimatorOverrideController)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Object/Stage/Tower/MeleeTower/MeleeFighter/Lv4/AC_MeleeFighterLv4.overrideController", typeof(AnimatorOverrideController)));
        }
        base.Awake();

        FighterAnimator.runtimeAnimatorController = MeleeFighterAnimatorCache[0];
    }

    public void ChangeTower(int TowerLevel)
    {
        Data.SetData(TowerLevel);
        CurHp = Data.Hp;
        FighterAnimator.runtimeAnimatorController = MeleeFighterAnimatorCache[TowerLevel - 1];
    }

    static List<AnimatorOverrideController> MeleeFighterAnimatorCache = new List<AnimatorOverrideController>();
}
