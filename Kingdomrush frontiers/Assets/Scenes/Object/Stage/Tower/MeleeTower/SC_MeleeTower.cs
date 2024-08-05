using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Assets.Scenes.Object.Stage.ContentsEnum;
using TMPro.EditorUtilities;
using Assets.Scenes.Object.Stage.StageData;

public class SC_MeleeTower : SC_BaseTower
{
    protected override void Awake()
    {
        base.Awake();
        TowerSprite = MeleeTowerSpriteCache;
        TowerRenderer.sprite = TowerSprite[Data.Level - 1];

        MeleeRallyPointInst = Instantiate(MeleeRallyPointPrefab, transform);
        MeleeRallyPointSetting = MeleeRallyPointInst.GetComponent<SC_MeleeRallyPoint>();
    }

    protected override void SpriteCaching()
    {
        if (MeleeTowerSpriteCache.Count == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                MeleeTowerSpriteCache.Add(Resources.Load<Sprite>("StageScene/Tower/Melee/MeleeTower/tower_barracks_lvl" + (i + 1).ToString()));
            }
        }
    }
    protected override void InitData()
    {
        Data.SetData(TowerEnum.MeleeTower_Level1);
    }
    protected override void ChangeTower(TowerEnum TowerValue)
    {
        if (!TowerData.IsMeleeTower(TowerValue))
        {
            Debug.LogAssertion("Set Melee Tower : Input Wrong EnumValue");
            return;
        }

        Data.SetData(TowerValue);
        TowerRenderer.sprite = TowerSprite[Data.Level - 1];
        MeleeRallyPointSetting.ChangeTower(Data.Level);
    }

    [SerializeField]
    private GameObject MeleeRallyPointPrefab;
    private GameObject MeleeRallyPointInst = null;
    private SC_MeleeRallyPoint MeleeRallyPointSetting = null;

    private static List<Sprite> MeleeTowerSpriteCache = new List<Sprite>();
}
