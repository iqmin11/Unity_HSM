using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scenes.Object.Base;
using UnityEngine;

public class SC_BuildAreaButton : SC_MyButton
{
    static Sprite CacheReleaseSprite = null;
    static Sprite CacheHoverSprite = null;
    static Sprite CachePressSprite = null;
    protected override void Start()
    {
        if(CacheReleaseSprite == null)
        {
            CacheReleaseSprite = Resources.Load<Sprite>("StageScene/Tower/TowerBase/build_terrain_0004");
        }

        ReleaseSprite = CacheReleaseSprite;

        if (CacheHoverSprite == null)
        {
            CacheHoverSprite = Resources.Load<Sprite>("StageScene/Tower/TowerBase/build_terrain_0004_Hover");
        }

        HoverSprite = CacheHoverSprite;

        if (CachePressSprite == null)
        {
            CachePressSprite = Resources.Load<Sprite>("StageScene/Tower/TowerBase/build_terrain_0004_Hover");
        }

        PressSprite = CachePressSprite;

        ColScale = MyMath.CentimeterToMeter(new Vector4(105, 60, 1));

        base.Start();
    }
}
