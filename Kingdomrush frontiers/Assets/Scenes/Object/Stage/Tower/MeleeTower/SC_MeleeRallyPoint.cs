using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MeleeRallyPoint : SC_BaseRallyPoint
{
    protected void Start()
    {
        SetFighter(3, FighterEnum.MeleeLv1);
    }

    protected override void SetFighter(int Count, FighterEnum Value)
    {
        if (Value < FighterEnum.MeleeLv1 || Value > FighterEnum.MeleeLv4)
        {
            Debug.LogAssertion("MeleeFigherRally : Wrong Fighter Enum Value");
            return;
        }

        base.SetFighter(Count, Value);
    }

    public void ChangeTower(int TowerLevel)
    {
        for (int i = 0; i < FighterInsts.Count; i++)
        {
            if (FighterInsts[i] == null)
            {
                continue;
            }

            FighterInsts[i].GetComponent<SC_MeleeFighter>().ChangeTower(TowerLevel);
        }
    }
}
