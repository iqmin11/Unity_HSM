using UnityEngine;

public class SC_Monster2DCol : SC_MonsterCol
{
    protected override void Awake()
    {
        base.Awake();
        Monster2DCol = gameObject.AddComponent<CircleCollider2D>();    
    }

    private CircleCollider2D Monster2DCol;
}
