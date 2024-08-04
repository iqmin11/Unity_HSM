using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public enum UpgradeUiEnum
{
    Upgrade,
    Sell,
}

public class SC_UpgradeUI : SC_BaseTowerUI
{
    protected override void InitButtons()
    {
        AddButton(UpgradeUiEnum.Upgrade);
        AddButton(UpgradeUiEnum.Sell);
    }
}
