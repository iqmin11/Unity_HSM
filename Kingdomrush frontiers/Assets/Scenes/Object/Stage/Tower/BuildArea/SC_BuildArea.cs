using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SC_BuildArea : MonoBehaviour
{
    private void Awake()
    {
        ButtonInst = Instantiate(ButtonPrefab, gameObject.transform);
        ButtonSetting = ButtonInst.GetComponent<SC_BuildAreaButton>();

        TowerUiInst = Instantiate(TowerUiPrefab, transform);
        TowerUiInst.SetActive(false);
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
    private GameObject TowerUiPrefab;
    private GameObject TowerUiInst;
    private SC_ConstructUI TowerUiSetting;

    private GameObject ChildTowerInst;
}
