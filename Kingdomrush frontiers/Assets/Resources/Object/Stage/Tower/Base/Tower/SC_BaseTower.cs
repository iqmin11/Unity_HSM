using Assets.Scenes.Object.Stage.ContentsEnum;
using Assets.Scenes.Object.Stage.StageData;
using System.Collections.Generic;
using UnityEngine;

public abstract class SC_BaseTower : MonoBehaviour
{
    protected virtual void Awake()
    {
        SpriteCaching();

        TowerRenderer = gameObject.AddComponent<SpriteRenderer>();
        TowerRenderer.sortingOrder = (int)RenderOrder.InGameObject;

        InitData();
        
        if (ButtonPrefab == null)
        {
            ButtonPrefab = Resources.Load<GameObject>("Object/Stage/Tower/Base/UI/PF_TowerButton");
        }
        ButtonInst = Instantiate(ButtonPrefab, transform);
        ButtonInst.transform.localPosition = new Vector3(0, 0, 1);
        ButtonSetting = ButtonInst.GetComponent<SC_TowerButton>();

        TowerUiInst = Instantiate(TowerUiPrefab, transform);

        ButtonSetting.Click = () =>
        {
            TowerUiInst.SetActive(true);
        };

        TowerUiSetting = TowerUiInst.GetComponent<SC_UpgradeUI>();
        TowerUiSetting.SettingButtonCallback(UpgradeUiEnum.Upgrade, () =>
        {
            ChangeTower(Data.TowerType + Data.Level + 1);
            TowerUiInst.SetActive(false);
        });

        TowerUiSetting.SettingButtonCallback(UpgradeUiEnum.Sell, () =>
        {
            SellTower();
        });
    }

    protected abstract void SpriteCaching();
    protected abstract void InitData();
    protected abstract void ChangeTower(TowerEnum TowerValue);

    private void SellTower()
    {
        transform.parent.GetComponent<SC_BuildArea>().OnBuildAreaButton();
        Destroy(gameObject);
    }

    protected TowerData Data = new TowerData();
    protected List<Sprite> TowerSprite = null;
    protected SpriteRenderer TowerRenderer;

    private static GameObject ButtonPrefab = null;
    private GameObject ButtonInst;
    private SC_TowerButton ButtonSetting;

    [SerializeField]
    private GameObject TowerUiPrefab;
    private GameObject TowerUiInst;
    private SC_BaseTowerUI TowerUiSetting;
}
