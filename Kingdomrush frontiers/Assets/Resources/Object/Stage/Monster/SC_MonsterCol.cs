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

    public Vector2 DestPoint2D
    {
        get
        {
            return transform.parent.GetComponent<SC_BaseMonster>().DestPoint;
        }
    }

    public Vector2 CurMonsterPos2D
    {
        get
        {
            return transform.parent.GetComponent<SC_BaseMonster>().CurMonsterPos;
        }
    }
}
