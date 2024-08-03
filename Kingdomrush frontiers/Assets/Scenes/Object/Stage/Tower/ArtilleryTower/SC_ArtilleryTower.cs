using Assets.Scenes.Object.Stage.ContentsEnum;
using Assets.Scenes.Object.Stage.StageData;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public sealed class SC_ArtilleryTower : SC_BaseShootingTower
{
    private static List<AnimatorOverrideController> ArtilleryTowerAnimatorCache = new List<AnimatorOverrideController>();

    protected override void Awake()
    {
        base.Awake();
        ArtilleryTowerAnimator = gameObject.GetComponent<Animator>();
        ArtilleryTowerAnimator.runtimeAnimatorController = ArtilleryTowerAnimatorCache[0];
    }
    protected override void SpriteCaching()
    {
        if(ArtilleryTowerAnimatorCache.Count == 0)
        {
            ArtilleryTowerAnimatorCache.Add((AnimatorOverrideController)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Object/Stage/Tower/ArtilleryTower/AC_ArtilleryLv1.overrideController", typeof(AnimatorOverrideController)));
            ArtilleryTowerAnimatorCache.Add((AnimatorOverrideController)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Object/Stage/Tower/ArtilleryTower/AC_ArtilleryLv2.overrideController", typeof(AnimatorOverrideController)));
            ArtilleryTowerAnimatorCache.Add((AnimatorOverrideController)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Object/Stage/Tower/ArtilleryTower/AC_ArtilleryLv3.overrideController", typeof(AnimatorOverrideController)));
            ArtilleryTowerAnimatorCache.Add((AnimatorOverrideController)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Object/Stage/Tower/ArtilleryTower/AC_ArtilleryLv4.overrideController", typeof(AnimatorOverrideController)));
        }
    }

    protected override void InitData()
    {
        Data.SetData(TowerEnum.ArtilleryTower_Level1);
    }

    protected override void AttackAction()
    {
        ArtilleryTowerAnimator.SetInteger("TowerState", (int)ShootingTowerState.Attack);
    }

    public void AttackEvent()
    {
        SC_BaseBullet CurShotBullet = Instantiate(BulletPrefab).GetComponent<SC_BaseBullet>();
        CurShotBullet.BulletSetting(gameObject.transform.position, TargetPos);
        CurShotBullet.Data = Data;
        CurShotBullet.gameObject.SetActive(true);
    }

    public void AttackEndEvent()
    {
        ArtilleryTowerAnimator.SetInteger("TowerState", (int)ShootingTowerState.Idle);
    }

    protected override void ChangeTower(TowerEnum TowerValue)
    {
        if (!TowerData.IsArtilleryTower(TowerValue))
        {
            return;
        }

        Data.SetData(TowerValue);
        ArtilleryTowerAnimator.runtimeAnimatorController = ArtilleryTowerAnimatorCache[Data.Level - 1];
    }

    private Animator ArtilleryTowerAnimator;
    [SerializeField]
    private GameObject BulletPrefab;

}
