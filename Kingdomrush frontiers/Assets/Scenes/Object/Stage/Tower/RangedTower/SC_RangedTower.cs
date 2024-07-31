using Assets.Scenes.Object.Stage.StageData;
using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_RangedTower : SC_BaseShootingTower
{
    static private List<Sprite> RangedTowerSpriteCache = new List<Sprite>();

    // Start is called before the first frame update
    override protected void Start()
    {
        if(RangedTowerSpriteCache.Count == 0)
        {
            for(int i = 0; i < 4; i++)
            {
                RangedTowerSpriteCache.Add(Resources.Load<Sprite>("StageScene/Tower/Ranged/RangedTower/archer_tower_000" + (i + 1).ToString()));
            }
        }
        
        TowerSprite = RangedTowerSpriteCache;
        Data.SetData(TowerEnum.RangedTower_Level1);
        
        base.Start();
        TowerRenderer = gameObject.AddComponent<SpriteRenderer>();
        TowerRenderer.sortingOrder = (int)RenderOrder.InGameObject;

        TowerRenderer.sprite = TowerSprite[Data.Level - 1];
        //Shooter0 = Instantiate(RangedShooterPrefab, gameObject.transform)
        //Shooter1 = Instantiate(RangedShooterPrefab, gameObject.transform)
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    private void InitTower()
    {
        
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

    TowerData Data = new TowerData();
    List<Sprite> TowerSprite = null;
    SpriteRenderer TowerRenderer;
}
