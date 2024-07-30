using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scenes.Object.Base;
using UnityEngine;

public class SC_BuildAreaButton : SC_MyButton
{
    protected override void Start()
    {
        ReleaseSprite = Resources.Load<Sprite>("StageScene/Tower/TowerBase/build_terrain_0004");
        HoverSprite = Resources.Load<Sprite>("StageScene/Tower/TowerBase/build_terrain_0004_Hover");
        PressSprite = Resources.Load<Sprite>("StageScene/Tower/TowerBase/build_terrain_0004_Hover");
        ColScale = MyMath.CentimeterToMeter(new Vector4(105, 60, 1));
        Click = () =>
        {
            Debug.Log("BuildAreaButton Click");
        };

        base.Start();
    }
}
