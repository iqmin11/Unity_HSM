using Assets.Scenes.Object.Base;
using Assets.Scenes.Object.Stage.ContentsEnum;
using Assets.Scenes.Object.Stage.StageData;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

sealed public class SC_MagicTower : SC_ShooterTower
{
    private static List<AnimatorOverrideController> MagicTowerAnimatorCache = new List<AnimatorOverrideController>();

    protected override void Awake()
    {
        base.Awake();
        if (ShooterPosData.Count == 0)
        {
            ShooterPosData.Add(Lv1ShooterLocalPos);
            ShooterPosData.Add(Lv2ShooterLocalPos);
            ShooterPosData.Add(Lv3ShooterLocalPos);
            ShooterPosData.Add(Lv4ShooterLocalPos);
        }

        MagicTowerAnimator = gameObject.GetComponent<Animator>();
        MagicTowerAnimator.runtimeAnimatorController = MagicTowerAnimatorCache[0];

        ShooterInst = Instantiate(MagicShooterPrefab, gameObject.transform);
        ShooterInst.transform.localPosition = ShooterPosData[Data.Level - 1];
        ShooterSetting = ShooterInst.GetComponent<SC_MagicShooter>();
        ShooterSetting.Data = Data;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    protected override void SpriteCaching()
    {
        if(MagicTowerAnimatorCache.Count == 0)
        {
            MagicTowerAnimatorCache.Add((AnimatorOverrideController)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Object/Stage/Tower/MagicTower/AnimationClip/Tower/AC_MagicTowerLv1.overrideController", typeof(AnimatorOverrideController)));
            MagicTowerAnimatorCache.Add((AnimatorOverrideController)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Object/Stage/Tower/MagicTower/AnimationClip/Tower/AC_MagicTowerLv2.overrideController", typeof(AnimatorOverrideController)));
            MagicTowerAnimatorCache.Add((AnimatorOverrideController)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Object/Stage/Tower/MagicTower/AnimationClip/Tower/AC_MagicTowerLv3.overrideController", typeof(AnimatorOverrideController)));
            MagicTowerAnimatorCache.Add((AnimatorOverrideController)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Object/Stage/Tower/MagicTower/AnimationClip/Tower/AC_MagicTowerLv4.overrideController", typeof(AnimatorOverrideController)));
        }
    }
    protected override void InitData()
    {
        Data.SetData(TowerEnum.MagicTower_Level1);
    }
    protected override void ChangeTower(TowerEnum TowerValue)
    {
        if (!TowerData.IsMagicTower(TowerValue))
        {
            return;
        }

        Data.SetData(TowerValue);
        MagicTowerAnimator.runtimeAnimatorController = MagicTowerAnimatorCache[Data.Level - 1];

        ShooterSetting.Data = Data;
        ShooterSetting.ChangeShooter();
        ShooterInst.transform.localPosition = ShooterPosData[Data.Level - 1];
    }
    protected override void AttackAction()
    {
        ShooterSetting.State = ShooterState.Attack;
        MagicTowerAnimator.SetInteger("TowerState", (int)ShootingTowerState.Attack);
    }

    public void AttackEndEvent()
    {
        MagicTowerAnimator.SetInteger("TowerState", (int)ShootingTowerState.Idle);
    }

    protected override void TransmissionTargetInfoToShooter()
    {
        ShooterSetting.TargetPos = TargetPos;
    }

    [SerializeField]
    private GameObject MagicShooterPrefab;

    private GameObject ShooterInst;
    private SC_MagicShooter ShooterSetting;
    private Animator MagicTowerAnimator;

    static readonly List<Vector3> ShooterPosData = new List<Vector3>();

    static readonly Vector3 Lv1ShooterLocalPos = MyMath.CentimeterToMeter(new Vector3(1, 47, -47));
    static readonly Vector3 Lv2ShooterLocalPos = MyMath.CentimeterToMeter(new Vector3(1, 49, -49));
    static readonly Vector3 Lv3ShooterLocalPos = MyMath.CentimeterToMeter(new Vector3(1, 54, -54));
    static readonly Vector3 Lv4ShooterLocalPos = MyMath.CentimeterToMeter(new Vector3(1, 44, -34));
}
