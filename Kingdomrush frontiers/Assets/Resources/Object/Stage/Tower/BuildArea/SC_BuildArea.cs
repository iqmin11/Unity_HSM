using UnityEngine;

public class SC_BuildArea : MonoBehaviour
{
    private void Awake()
    {
        ButtonInst = Instantiate(ButtonPrefab, gameObject.transform);
        ButtonSetting = ButtonInst.GetComponent<SC_BuildAreaButton>();

        TowerUiInst = Instantiate(TowerUiPrefab, transform);
        ButtonSetting.Click = () =>
        {
            TowerUiInst.SetActive(true);
        };

        TowerUiSetting = TowerUiInst.GetComponent<SC_ConstructUI>();
        TowerUiSetting.SettingButtonCallback(ConstructButtonEnum.RangedTower, () =>
        {
            ChildTowerInst = Instantiate(RangedTowerPrefab, transform);
            ButtonInst.SetActive(false);
            TowerUiInst.SetActive(false);
        });

        TowerUiSetting.SettingButtonCallback(ConstructButtonEnum.MagicTower, () =>
        {
            ChildTowerInst = Instantiate(MagicTowerPrefab, transform);
            ButtonInst.SetActive(false);
            TowerUiInst.SetActive(false);
        });

        TowerUiSetting.SettingButtonCallback(ConstructButtonEnum.ArtilleryTower, () =>
        {
            ChildTowerInst = Instantiate(ArtilleryTowerPrefab, transform);
            ButtonInst.SetActive(false);
            TowerUiInst.SetActive(false);
        });

        TowerUiSetting.SettingButtonCallback(ConstructButtonEnum.MeleeTower, () =>
        {
            ChildTowerInst = Instantiate(MeleeTowerPrefab, transform);
            ChildTowerInst.GetComponent<SC_MeleeTower>().DefaultRallyPos = DefaultRallyPos;
            ButtonInst.SetActive(false);
            TowerUiInst.SetActive(false);
        });
    }

    public void OnBuildAreaButton()
    {
        ButtonInst.SetActive(true); 
    }

    [SerializeField]
    private GameObject ButtonPrefab;
    private GameObject ButtonInst;
    private SC_BuildAreaButton ButtonSetting;

    [SerializeField]
    private GameObject RangedTowerPrefab;
    [SerializeField]
    private GameObject MagicTowerPrefab;
    [SerializeField]
    private GameObject ArtilleryTowerPrefab;
    [SerializeField]
    private GameObject MeleeTowerPrefab;

    [SerializeField]
    private GameObject TowerUiPrefab;
    private GameObject TowerUiInst;
    private SC_BaseTowerUI TowerUiSetting;

    private GameObject ChildTowerInst;

    private Vector4 defaultRallyPos;
    public Vector4 DefaultRallyPos
    {
        get
        {
            return defaultRallyPos;
        }

        set
        {
            defaultRallyPos = value;
        }
    }
}
