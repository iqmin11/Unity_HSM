using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

sealed public class SC_DesertThug : SC_BaseMonster
{
    protected override void Start()
    {
        base.Start(); //부모의 Start 호출
        //Data.SetData(MonsterEnum::DesertThug);
        //CurHp = Data.Hp;
        MoveStateInit();
        MonsterFSM.ChangeState("Move");
    }
}
