using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scenes.Object.Stage.ContentsEnum;
using System;


public enum ConstructButtonEnum
{
    RangedTower,
    MagicTower,
    ArtilleryTower,
}

public class SC_BaseTowerUI : MonoBehaviour
{
    protected virtual void Awake()
    {
        if (RingSprite == null)
        {
            RingSprite = Resources.Load<Sprite>("StageScene/GUI/gui_ring");
        }

        TowerUIring = gameObject.AddComponent<SpriteRenderer>();
        TowerUIring.sprite = RingSprite;
        TowerUIring.sortingOrder = (int)RenderOrder.InGameUI;

        ButtonInsts.Add(Instantiate(ButtonPrefabs[(int)ConstructButtonEnum.RangedTower], transform).GetComponent<SC_MyButton>());
        //Buttons.Add("MagicTower", Instantiate(ButtonPrefabs["MagicTower"], transform).GetComponent<SC_MyButton>());
        //Buttons.Add("ArtilleryTower", Instantiate(ButtonPrefabs["ArtilleryTower"], transform).GetComponent<SC_MyButton>());
    }

    public void SettingButtonCallback(int Index, System.Action Callback)
    {
        ButtonInsts[Index].Click = Callback;
    }

    public void SettingButtonCallback<EnumT>(EnumT Key, System.Action Callback)
    {
        SettingButtonCallback(Convert.ToInt32(Key), Callback);
    }
    
    
    [SerializeField]
    private List<GameObject> ButtonPrefabs = new List<GameObject>();
    protected List<SC_MyButton> ButtonInsts = new List<SC_MyButton>();
    
    private static Sprite RingSprite = null;
    private SpriteRenderer TowerUIring = null;
}
