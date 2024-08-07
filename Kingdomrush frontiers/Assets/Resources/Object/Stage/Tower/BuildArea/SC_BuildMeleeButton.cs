using Assets.Scenes.Object.Base;
using Assets.Scenes.Object.Stage.ContentsEnum;
using UnityEngine;

public class SC_BuildMeleeButton : SC_TowerUiButton
{
    static Sprite CacheReleaseSprite = null;
    static Sprite CacheHoverSprite = null;
    static Sprite CachePressSprite = null;
    protected override void Awake()
    {
        if (CacheReleaseSprite == null)
        {
            CacheReleaseSprite = Resources.Load<Sprite>("StageScene/GUI/Button/main_icons_0002");
        }
        ReleaseSprite = CacheReleaseSprite;

        if (CacheHoverSprite == null)
        {
            CacheHoverSprite = Resources.Load<Sprite>("StageScene/GUI/Button/main_icons_0002");
        }
        HoverSprite = CacheHoverSprite;

        if (CachePressSprite == null)
        {
            CachePressSprite = Resources.Load<Sprite>("StageScene/GUI/Button/main_icons_0002");
        }
        PressSprite = CachePressSprite;

        transform.localPosition = LocPos;
        base.Awake();

        GlowEffectInst.transform.localScale = new Vector3(1.35f, 1.35f);
    }

    protected override void SettingButtonRenderOrder()
    {
        ButtonRenderer.sortingOrder = (int)RenderOrder.InGameUI1;
    }

    static readonly Vector2 LocPos = MyMath.CentimeterToMeter(new Vector2(80, 80));
}
