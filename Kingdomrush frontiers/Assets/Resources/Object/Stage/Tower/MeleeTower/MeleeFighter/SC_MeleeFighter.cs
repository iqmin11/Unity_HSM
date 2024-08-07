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
    }

    public void ChangeTower(int TowerLevel)
    {
        Data.SetData(TowerLevel);
        CurHp = Data.Hp;
        FighterAnimator.runtimeAnimatorController = MeleeFighterAnimatorCache[TowerLevel - 1];
    }

    static List<AnimatorOverrideController> MeleeFighterAnimatorCache = new List<AnimatorOverrideController>();
}
