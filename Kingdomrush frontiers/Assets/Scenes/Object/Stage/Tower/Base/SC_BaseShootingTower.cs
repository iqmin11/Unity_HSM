using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

abstract public class SC_BaseShootingTower : SC_BaseTower
{
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        if(IsFindTargetMonster())
        {
            CalTargetPos();
            TransitionTargetInfoToShooter();
            if (IsCoolTimeEnd)
            {
                IsCoolTimeEnd = false;
                StartCoroutine(Attack(Data.FireRate));
            }
        }
    }

    //Target
    protected SC_BaseMonster TargetMonster = null;
    protected Vector4 TargetPos = Vector4.zero;
    List<RaycastHit2D> Filter = new List<RaycastHit2D>();
    private bool IsTarget;
    private bool IsFindTargetMonster()
    {
        RaycastHit2D[] Hits = Physics2D.CircleCastAll(gameObject.transform.position, Data.Range, Vector2.zero);
        if (Hits.Length == 0)
        {
            TargetMonster = null;
            return false;
        }
        
        Filter.Clear();
        Filter.Capacity = Hits.Length;
        for(int i = 0; i < Filter.Capacity; i++)
        {
            if (Hits[i].collider.CompareTag("Monster"))
            {
                Filter.Add(Hits[i]);
            }
        }

        if (Filter.Count == 0)
        {
            TargetMonster = null;
            return false;
        }

        Filter.Sort((Left, Right) =>
        {
            SC_BaseMonster LeftMonster = Left.collider.gameObject.GetComponent<SC_BaseMonster>();
            SC_BaseMonster RightMonster = Right.collider.gameObject.GetComponent<SC_BaseMonster>();
            float LeftRemainDist = Vector4.Distance(LeftMonster.DestPoint, LeftMonster.CurMonsterPos);
            float RightRemainDist = Vector4.Distance(RightMonster.DestPoint, RightMonster.CurMonsterPos);
            return LeftRemainDist.CompareTo(RightRemainDist); 
        });

        TargetMonster = Filter[0].collider.GetComponent<SC_BaseMonster>();
        return true;
    }
    private void CalTargetPos()
    {
        Vector4 CurPos = TargetMonster.CurMonsterPos;
        Vector4 Dir = TargetMonster.CurMonsterDir;
        float MonsterSpeed = 0;
        if (TargetMonster.GetCurState() == (int)MonsterState.Move)
        {
            MonsterSpeed = TargetMonster.MonsterSpeed;
        }
        float BulletTime = Data.BulletTime;

        TargetPos = CurPos + Dir * MonsterSpeed * BulletTime;
    }
    abstract protected void TransitionTargetInfoToShooter();

    //Attack
    protected bool IsCoolTimeEnd = true;
    protected IEnumerator Attack(float FireRate)
    {
        AttackAction();
        yield return new WaitForSeconds(FireRate);
        IsCoolTimeEnd = true;
    }
    abstract protected void AttackAction();


}
