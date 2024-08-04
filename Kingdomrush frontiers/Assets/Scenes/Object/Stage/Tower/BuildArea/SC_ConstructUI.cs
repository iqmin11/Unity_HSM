using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConstructButtonEnum
{
    RangedTower,
    MagicTower,
    ArtilleryTower,
}

public class SC_ConstructUI : SC_BaseTowerUI
{
    protected override void InitButtons()
    {
        AddButton(ConstructButtonEnum.RangedTower);
        AddButton(ConstructButtonEnum.MagicTower);
        AddButton(ConstructButtonEnum.ArtilleryTower);
    }
}
