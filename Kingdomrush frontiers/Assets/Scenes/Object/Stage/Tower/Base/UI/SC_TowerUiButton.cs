using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_TowerUiButton : SC_MyButton
{
    [SerializeField]
    private GameObject GlowEffectPrefab;
    protected GameObject GlowEffectInst;

    protected override void Awake()
    {
        base.Awake();
        GlowEffectInst = Instantiate(GlowEffectPrefab, transform);
        
        HoverEvent = () =>
        {
            GlowEffectInst.SetActive(true);
        };

        ReleaseEvent = () =>
        {
            GlowEffectInst.SetActive(false);
        };
    }

    protected override void SettingButtonRenderOrder()
    {
        ButtonRenderer.sortingOrder = (int)RenderOrder.InGameUI1;
    }
}
