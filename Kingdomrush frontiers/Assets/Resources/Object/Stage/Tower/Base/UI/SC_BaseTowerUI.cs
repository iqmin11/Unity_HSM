using System;
using System.Collections.Generic;

using UnityEngine;

using Assets.Scenes.Object.Stage.ContentsEnum;
using Assets.Scenes.Object.Base;

public abstract class SC_BaseTowerUI : MonoBehaviour
{
    public static GameObject UpdatingTowerUI;

    protected virtual void Awake()
    {
        if (RingSprite == null)
        {
            RingSprite = Resources.Load<Sprite>("StageScene/GUI/gui_ring");
            SC_MyMouseBase.MouseInfo.RegistReleaseClickEvent(IsReleaseClickEvent);
        }

        TowerUIring = gameObject.AddComponent<SpriteRenderer>();
        TowerUIring.sprite = RingSprite;
        TowerUIring.sortingOrder = (int)RenderOrder.InGameUI0;

        Vector2 spriteSize = TowerUIring.sprite.bounds.size;
        transform.localScale = new Vector2(RingRenderScale.x / spriteSize.x, RingRenderScale.y / spriteSize.y);

        InitButtons();

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if(UpdatingTowerUI != null)
        {
            UpdatingTowerUI.SetActive(false);
        }

        UpdatingTowerUI = this.gameObject;
    }

    private void OnDisable()
    {
        UpdatingTowerUI = null;
    }

    protected abstract void InitButtons();
    protected virtual void AddButton<EnumT>(EnumT Button)
    {
        ButtonSettings.Add(Instantiate(ButtonPrefabs[Convert.ToInt32(Button)], transform).GetComponent<SC_MyButton>());
    }

    public void SettingButtonCallback(int Index, System.Action Callback)
    {
        ButtonSettings[Index].Click = Callback;
    }

    public void SettingButtonCallback<EnumT>(EnumT Key, System.Action Callback)
    {
        SettingButtonCallback(Convert.ToInt32(Key), Callback);
    }

    private static void IsReleaseClickEvent()
    {
        if (UpdatingTowerUI != null)
        {
            UpdatingTowerUI.SetActive(false);
        }
    }
    
    [SerializeField]
    private List<GameObject> ButtonPrefabs = new List<GameObject>();
    protected List<SC_MyButton> ButtonSettings = new List<SC_MyButton>();
    
    private static Sprite RingSprite = null;
    private SpriteRenderer TowerUIring = null;
    private static readonly Vector4 RingRenderScale = MyMath.CentimeterToMeter(new Vector2(171, 171));

}
