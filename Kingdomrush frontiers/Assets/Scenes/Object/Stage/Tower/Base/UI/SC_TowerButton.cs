using Assets.Scenes.Object.Base;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SC_TowerButton : SC_MyButton
{
    static Sprite CacheReleaseSprite = null;
    static Sprite CacheHoverSprite = null;
    static Sprite CachePressSprite = null;

    protected override void Awake()
    {
        if (CacheReleaseSprite == null)
        {
            CacheReleaseSprite = Resources.Load<Sprite>("StageScene/Tower/TowerBase/terrain_0004");
        }

        ReleaseSprite = CacheReleaseSprite;

        if (CacheHoverSprite == null)
        {
            CacheHoverSprite = Resources.Load<Sprite>("StageScene/Tower/TowerBase/terrain_0004_Hover");
        }

        HoverSprite = CacheHoverSprite;

        if (CachePressSprite == null)
        {
            CachePressSprite = Resources.Load<Sprite>("StageScene/Tower/TowerBase/terrain_0004_Hover");
        }

        PressSprite = CachePressSprite;

        ColScale = MyMath.CentimeterToMeter(new Vector4(110, 95, 1));

        base.Awake();
    }
}
