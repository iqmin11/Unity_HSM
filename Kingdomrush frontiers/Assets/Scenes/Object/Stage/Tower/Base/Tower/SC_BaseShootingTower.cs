using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    // Contents
    override protected void Awake()
    {
        base.Awake();
        TargetLayer = 1 << LayerMask.NameToLayer("Monster");
    }

    // Update is called once per frame
    virtual protected void Update()
    {
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
    Collider Hit = new Collider();
    protected Vector4 TargetPos = Vector4.zero;
    private bool IsTarget;
    private LayerMask TargetLayer;

    private bool IsFindTargetMonster()
    {
        Collider[] Hits = Physics.OverlapSphere(gameObject.transform.position, Data.Range, TargetLayer.value);
        Hits.OrderBy(Hits =>
            Vector4.Distance(
            Hit.gameObject.GetComponent<SC_BaseMonster>().DestPoint,
            Hit.gameObject.GetComponent<SC_BaseMonster>().CurMonsterPos));

        if (Hits.Length == 0)
        {
            TargetMonster = null;
            return false;
        }
        
        TargetMonster = Hits[0].GetComponent<SC_BaseMonster>();
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
