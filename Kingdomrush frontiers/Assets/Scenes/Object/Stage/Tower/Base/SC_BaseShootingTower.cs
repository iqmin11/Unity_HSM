using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

abstract public class SC_BaseShootingTower : SC_BaseTower
{
    // Debug
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, Data.Range);
    }

    override protected void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        if (IsFindTargetMonster())
        {
            CalTargetPos();
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
    List<Collider2D> Filter = new List<Collider2D>();
    private bool IsTarget;


    private bool IsFindTargetMonster()
    {
        Collider2D[] Hits = Physics2D.OverlapCircleAll(gameObject.transform.position, Data.Range);

        if (Hits.Length == 0)
        {
            TargetMonster = null;
            return false;
        }
        
        Filter.Clear();
        for(int i = 0; i < Hits.Length; i++)
        {
            if (Hits[i].CompareTag("Monster"))
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
            SC_BaseMonster LeftMonster = Left.GetComponent<Collider2D>().gameObject.GetComponent<SC_BaseMonster>();
            SC_BaseMonster RightMonster = Right.GetComponent<Collider2D>().gameObject.GetComponent<SC_BaseMonster>();
            float LeftRemainDist = Vector4.Distance(LeftMonster.DestPoint, LeftMonster.CurMonsterPos);
            float RightRemainDist = Vector4.Distance(RightMonster.DestPoint, RightMonster.CurMonsterPos);
            return LeftRemainDist.CompareTo(RightRemainDist); 
        });

        TargetMonster = Filter[0].GetComponent<SC_BaseMonster>();
        return true;
    }
    virtual protected void CalTargetPos()
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
