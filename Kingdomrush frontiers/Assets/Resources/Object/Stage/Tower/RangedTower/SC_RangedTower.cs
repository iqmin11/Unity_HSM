using Assets.Scenes.Object.Stage.StageData;
using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scenes.Object.Base;

public sealed class SC_RangedTower : SC_ShooterTower
{
    private static List<Sprite> RangedTowerSpriteCache = new List<Sprite>();

    protected override void Awake()
    {
        base.Awake();
        if (ShooterPosData.Count == 0)
        {
            ShooterPosData.Add(new List<Vector3>());
            ShooterPosData[0].Add(Lv1Shooter0LocalPos);
            ShooterPosData[0].Add(Lv1Shooter1LocalPos);

            ShooterPosData.Add(new List<Vector3>());
            ShooterPosData[1].Add(Lv2Shooter0LocalPos);
            ShooterPosData[1].Add(Lv2Shooter1LocalPos);

            ShooterPosData.Add(new List<Vector3>());
            ShooterPosData[2].Add(Lv3Shooter0LocalPos);
            ShooterPosData[2].Add(Lv3Shooter1LocalPos);

            ShooterPosData.Add(new List<Vector3>());
            ShooterPosData[3].Add(Lv4Shooter0LocalPos);
            ShooterPosData[3].Add(Lv4Shooter1LocalPos);
        }

        TowerSprite = RangedTowerSpriteCache;
        TowerRenderer.sprite = TowerSprite[Data.Level - 1];

        Shooter0Inst = Instantiate(RangedShooterPrefab, gameObject.transform);
        Shooter0Inst.transform.localPosition = ShooterPosData[Data.Level - 1][0];
        Shooter0Setting = Shooter0Inst.GetComponent<SC_RangedShooter>();
        Shooter0Setting.Data = Data;

        Shooter1Inst = Instantiate(RangedShooterPrefab, gameObject.transform);
        Shooter1Inst.transform.localPosition = ShooterPosData[Data.Level - 1][1];
        Shooter1Setting = Shooter1Inst.GetComponent<SC_RangedShooter>();
        Shooter1Setting.Data = Data;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    protected override void SpriteCaching()
    {
        if (RangedTowerSpriteCache.Count == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                RangedTowerSpriteCache.Add(Resources.Load<Sprite>("StageScene/Tower/Ranged/RangedTower/archer_tower_000" + (i + 1).ToString()));
            }
        }
    }
    protected override void InitData()
    {
        Data.SetData(TowerEnum.RangedTower_Level1);
    }
    protected override void ChangeTower(TowerEnum TowerValue)
    {
        if (!TowerData.IsRangedTower(TowerValue))
        {
            Debug.LogAssertion("Set Ranged Tower : Input Wrong EnumValue");
            return;
        }

        Data.SetData(TowerValue);
        TowerRenderer.sprite = TowerSprite[Data.Level - 1];

        Shooter0Setting.Data = Data;
        Shooter0Setting.ChangeShooter();
        Shooter0Inst.transform.localPosition = ShooterPosData[Data.Level - 1][0];

        Shooter1Setting.Data = Data;
        Shooter1Setting.ChangeShooter();
        Shooter1Inst.transform.localPosition = ShooterPosData[Data.Level - 1][1];
    }
    protected override void AttackAction()
    {
        if (AttackOrder)
        {
            Shooter0Setting.State = ShooterState.Attack;
        }
        else
        {
            Shooter1Setting.State = ShooterState.Attack;
        }
        AttackOrder = !AttackOrder;
    }
    protected override void TransmissionTargetInfoToShooter()
    {
        Shooter0Setting.TargetPos = TargetPos;
        Shooter1Setting.TargetPos = TargetPos;
    }

    [SerializeField]
    private GameObject RangedShooterPrefab;

    private GameObject Shooter0Inst;
    private SC_RangedShooter Shooter0Setting;
    private GameObject Shooter1Inst;
    private SC_RangedShooter Shooter1Setting;

    private bool AttackOrder = false;

    static readonly List<List<Vector3>> ShooterPosData = new List<List<Vector3>>();

    static readonly Vector3 Lv1Shooter0LocalPos = MyMath.CentimeterToMeter(new Vector3(14, 47, -47));
    static readonly Vector3 Lv1Shooter1LocalPos = MyMath.CentimeterToMeter(new Vector3(-10, 47, -47));
    static readonly Vector3 Lv2Shooter0LocalPos = MyMath.CentimeterToMeter(new Vector3(14, 49, -49));
    static readonly Vector3 Lv2Shooter1LocalPos = MyMath.CentimeterToMeter(new Vector3(-10, 49, -49));
    static readonly Vector3 Lv3Shooter0LocalPos = MyMath.CentimeterToMeter(new Vector3(14, 54, -54));
    static readonly Vector3 Lv3Shooter1LocalPos = MyMath.CentimeterToMeter(new Vector3(-10, 54, -54));
    static readonly Vector3 Lv4Shooter0LocalPos = MyMath.CentimeterToMeter(new Vector3(14, 56, -56));
    static readonly Vector3 Lv4Shooter1LocalPos = MyMath.CentimeterToMeter(new Vector3(-10, 56, -56));
}
