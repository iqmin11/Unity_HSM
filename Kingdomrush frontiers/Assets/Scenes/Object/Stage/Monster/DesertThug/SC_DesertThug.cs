using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

sealed public class SC_DesertThug : SC_BaseMonster
{
    protected override void Start()
    {
        base.Start(); //부모의 Start 호출
    }

    override protected void SetData()
    {
        Data.SetData(MonsterEnum.DesertThug);
    }

    protected override void StateInit()
    {
        MoveStateInit();
    }
}
