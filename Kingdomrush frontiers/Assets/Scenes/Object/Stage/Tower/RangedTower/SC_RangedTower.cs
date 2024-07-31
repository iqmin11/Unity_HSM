using Assets.Scenes.Object.Stage.StageData;
using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed public class SC_RangedTower : SC_BaseShootingTower
{
    static private List<Sprite> RangedTowerSpriteCache = new List<Sprite>();

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        TowerSprite = RangedTowerSpriteCache;
        TowerRenderer.sprite = TowerSprite[Data.Level - 1];

        Shooter0Inst = Instantiate(RangedShooterPrefab, gameObject.transform);
        Shooter0Setting = Shooter0Inst.GetComponent<SC_RangedShooter>();

        Shooter1Inst = Instantiate(RangedShooterPrefab, gameObject.transform);
        Shooter1Setting = Shooter1Inst.GetComponent<SC_RangedShooter>();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    override protected void SpriteCaching()
    {
        if (RangedTowerSpriteCache.Count == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                RangedTowerSpriteCache.Add(Resources.Load<Sprite>("StageScene/Tower/Ranged/RangedTower/archer_tower_000" + (i + 1).ToString()));
            }
        }
    }

    override protected void InitData()
    {
        Data.SetData(TowerEnum.RangedTower_Level1);
    }

    public void SetRangedTower(TowerEnum TowerValue)
    {
        if (TowerValue < TowerEnum.RangedTower_Level1 || TowerValue > TowerEnum.RangedTower_Level4)
        {
            Debug.LogAssertion("Set Ranged Tower : Input Wrong EnumValue");
            return;
        }

        Data.SetData(TowerValue);
        TowerRenderer.sprite = TowerSprite[Data.Level - 1];
        //ChangeShooter(Data.Level);
    }

    override protected void AttackAction()
    {
        if (AttackOrder)
        {
            Shooter0Setting.State = ShooterState.Attack;
            Shooter1Setting.State = ShooterState.Idle;
        }
        else
        {
            Shooter0Setting.State = ShooterState.Idle;
            Shooter1Setting.State = ShooterState.Attack;
        }
        AttackOrder = !AttackOrder;
    }

    [SerializeField]
    private GameObject RangedShooterPrefab;

    private GameObject Shooter0Inst;
    private SC_RangedShooter Shooter0Setting;
    private GameObject Shooter1Inst;
    private SC_RangedShooter Shooter1Setting;

    private bool AttackOrder = false;

}
