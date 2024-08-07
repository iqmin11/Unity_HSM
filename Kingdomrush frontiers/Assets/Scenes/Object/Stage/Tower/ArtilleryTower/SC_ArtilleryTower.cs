using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

using Assets.Scenes.Object.Stage.ContentsEnum;
using Assets.Scenes.Object.Stage.StageData;
using Assets.Scenes.Object.Base;
using Assets.Scenes.Object.Base.MyInterface;

public sealed class SC_ArtilleryTower : SC_BaseShootingTower, IEffectPlayer
{
    private static List<AnimatorOverrideController> ArtilleryTowerAnimatorCache = new List<AnimatorOverrideController>();

    protected override void Awake()
    {
        base.Awake();
        ArtilleryTowerAnimator = gameObject.GetComponent<Animator>();
        ArtilleryTowerAnimator.runtimeAnimatorController = ArtilleryTowerAnimatorCache[0];

        if (FireEffectPos.Count == 0)
        {
            FireEffectPos.Capacity = 4;
            FireEffectPos.Add(Lv1SmokeLocalPos);
            FireEffectPos.Add(Lv2SmokeLocalPos);
            FireEffectPos.Add(Lv3SmokeLocalPos);
            FireEffectPos.Add(Lv4SmokeLocalPos);
        }

        ArtilleryTowerLayerMask |= 1 << LayerMask.NameToLayer("Monster");
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
        PlayEffect();
        if(Data.Level <= 3)
        {
            SC_BaseBullet CurShotBullet = Instantiate(BulletPrefab).GetComponent<SC_BaseBullet>();
            CurShotBullet.BulletSetting(gameObject.transform.position, TargetPos);
            CurShotBullet.Data = Data;
            CurShotBullet.gameObject.SetActive(true);
        }
        else if(Data.Level == 4)
        {

            Collider2D[] Hits = Physics2D.OverlapCircleAll(transform.position, Data.Range, ArtilleryTowerLayerMask);
            for(int i = 0; i < Hits.Length; i++)
            {
                Hits[i].gameObject.GetComponent<SC_Monster2DCol>().ParentMonster.TakeDamage(CalDamage());
            }
        }
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

    public void PlayEffect()
    {
        if (Data.Level <= 3)
        {
            GameObject FireEffectInst = Instantiate(FireEffectPrefab, transform);
            FireEffectInst.transform.localPosition = FireEffectPos[Data.Level - 1];

        }
        else if(Data.Level == 4)
        {
            Instantiate(Lv4TowerEffectPrefab, transform);
        }
    }

    private float CalDamage()
    {
        return UnityEngine.Random.Range(Data.Damage_min, Data.Damage_MAX);
    }

    private LayerMask ArtilleryTowerLayerMask = 0;

    private Animator ArtilleryTowerAnimator;

    [SerializeField]
    private GameObject BulletPrefab;

    [SerializeField]
    private GameObject FireEffectPrefab;

    [SerializeField]
    private GameObject Lv4TowerEffectPrefab;

    private static readonly List<Vector4> FireEffectPos = new List<Vector4>();
    private static readonly Vector4 Lv1SmokeLocalPos = MyMath.CentimeterToMeter(new Vector4(1, 60, -60));
    private static readonly Vector4 Lv2SmokeLocalPos = MyMath.CentimeterToMeter(new Vector4(1, 62, -62));
    private static readonly Vector4 Lv3SmokeLocalPos = MyMath.CentimeterToMeter(new Vector4(1, 67, -67));
    private static readonly Vector4 Lv4SmokeLocalPos = MyMath.CentimeterToMeter(new Vector4(1, 67, -67));

}
