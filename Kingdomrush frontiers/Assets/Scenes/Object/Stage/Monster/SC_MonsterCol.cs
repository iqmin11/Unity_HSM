using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SC_MonsterCol : MonoBehaviour
{
    protected virtual void Awake()
    {
        gameObject.tag = "Monster";
        gameObject.layer = LayerMask.NameToLayer("Monster");
    }
    
    public GameObject ParentObject
    {
        get
        {
            return transform.parent.gameObject;
        }
    }

    public SC_BaseMonster ParentMonster
    {
        get
        {
            return transform.parent.gameObject.GetComponent<SC_BaseMonster>();
        }
    }

    public Vector4 DestPoint
    {
        get
        {
            return transform.parent.GetComponent<SC_BaseMonster>().DestPoint;
        }
    }

    public Vector4 CurMonsterPos
    {
        get
        {
            return transform.parent.GetComponent<SC_BaseMonster>().CurMonsterPos;
        }
    }
}
