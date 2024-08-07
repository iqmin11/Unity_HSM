using UnityEngine;

public class SC_Monster3DCol : SC_MonsterCol
{
    protected override void Awake()
    {
        base.Awake();
        Monster3DCol = gameObject.AddComponent<SphereCollider>();
    }

    private SphereCollider Monster3DCol;
}
