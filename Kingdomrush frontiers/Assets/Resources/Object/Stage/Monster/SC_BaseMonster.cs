using Assets.Scenes.Object.Stage.StageData;
using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scenes.Object.Base;

public enum MonsterState
{
    Idle,
    Move,
    Attack,
    Death,
    Born,
    ComeUp,
    GoDown,
    RangeAttack,
    Summon,
}

public enum MoveDir
{
    Null = -1,
    Profile,
    Forward,
    Backward
}

abstract public class SC_BaseMonster : MonoBehaviour
{
    protected virtual void Awake()
    {
        MonsterInit();
        SetData();
        CurHp = Data.Hp;
        StateInit();
        MonsterFSM.ChangeState(MonsterState.Move);
        gameObject.SetActive(false);
        SC_StageManager.PushLiveMonster(gameObject);
    }
    
    // Use Update in FSM
    private void Update()
    {
        HpBarSetting.SetCurHp(CurHp / Data.Hp);
    }

    public void TakeDamage(float Damage)
    {
        CurHp -= Damage;
        if(CurHp < 0)
        {
            CurHp = 0;
        }
    }
    public void StartInteractionWithFighter(SC_BaseFighter Fighter)
    {
        MonsterFSM.ChangeState(MonsterState.Idle);
        RegistFighter(Fighter);
    }
    
    public void EndInteractionWithFighter(SC_BaseFighter Fighter)
    {
        UnregisterFighter(Fighter);
    }

    private void BrodcastDeath()
    {
        foreach (var EachFighter in AttackFighters)
        {
            EachFighter.Value.GetComponent<SC_BaseFighter>().NotifyMonsterDeath();
        }
    }

    private void OnDestroy()
    {
        SC_StageManager.MonsterDeathNotify(gameObject.GetInstanceID());
    }

    private void RegistFighter(SC_BaseFighter Fighter)
    {
        AttackFighters.Add(Fighter.gameObject.GetInstanceID(), Fighter.gameObject);
    }
    
    private void UnregisterFighter(SC_BaseFighter Fighter)
    {
        AttackFighters.Remove(Fighter.gameObject.GetInstanceID());
    }

    private SortedDictionary<int, GameObject> AttackFighters = new SortedDictionary<int, GameObject>();

    public Vector4 CurMonsterPos
    {
        get
        {
            return Walk.MonsterPos;
        }
    }
    public Vector4 DestPoint
    {
        get
        {
            return Walk.DestPoint;
        }
    }
    public Vector4 CurMonsterDir
    {
        get
        {
            return Walk.MonsterDir;
        }
    }
    public float MonsterSpeed
    {
        get
        {
            return Data.Speed;
        }
    }

    // MonsterBase /////////////////////////////////////
    protected MonsterData Data = new MonsterData();

    private GameObject Monster3DColPrefab;
    private GameObject Monster3DColInst;
    protected SphereCollider Monster3DCol
    {
        get
        {
            return Monster3DColInst.GetComponent<SphereCollider>();
        }
    }

    private GameObject Monster2DColPrefab;
    private GameObject Monster2DColInst;
    protected CircleCollider2D Monster2DCol
    {
        get
        {
            return Monster2DColInst.GetComponent<CircleCollider2D>();
        }
    }

    private GameObject HpBarPrefab;
    private GameObject HpBarInst;
    private SC_HpBar HpBarSetting;

    protected Vector3 HpBarLocalPos = new Vector3(0f,0.35f,0f);

    public float CurHp
    {
        get
        {
            return curHp;
        }

        set
        {
            if (curHp <= -1000)
            {
                Debug.LogWarning("Hp Less then -1000");
            }

            curHp = value;
        }
    }
    private float curHp = 0.0f;
    protected abstract void SetColRadius();
    protected abstract void SetData();
    private void MonsterInit()
    {
        //Initialize FSM
        MonsterFSM = gameObject.AddComponent<SC_FSM>();
        if (MonsterFSM == null)
        {
            Debug.LogAssertion("MonsterFSM is null");
        }

        MonsterAnimator = GetComponent<Animator>();
        if (MonsterAnimator == null)
        {
            Debug.LogAssertion("MonsterAnimator is null");
        }

        MonsterRenderer = GetComponent<SpriteRenderer>();
        if (MonsterAnimator == null)
        {
            Debug.LogAssertion("MonsterRenderer is null");
        }
        MonsterRenderer.sortingOrder = (int)RenderOrder.InGameObject;

        Monster3DColPrefab = Resources.Load<GameObject>("Object/Stage/Monster/PF_Monster3DCol");
        Monster3DColInst = Instantiate(Monster3DColPrefab, transform);

        Monster2DColPrefab = Resources.Load<GameObject>("Object/Stage/Monster/PF_Monster2DCol");
        Monster2DColInst = Instantiate(Monster2DColPrefab, transform);

        SetColRadius();
        if (Monster2DCol.radius == 0.0f || Monster3DCol.radius == 0.0f)
        {
            Debug.LogAssertion("Pleas Set Monster Collider Scale");
        }

        gameObject.tag = "Monster";
        gameObject.layer = LayerMask.NameToLayer("Monster");

        HpBarPrefab = Resources.Load<GameObject>("Object/Stage/Etc/PF_HpBar");
        HpBarInst = Instantiate(HpBarPrefab, transform);
        HpBarSetting = HpBarInst.GetComponent<SC_HpBar>();
        HpBarInst.transform.localPosition = HpBarLocalPos;
    }

    // Walk /////////////////////////////////////
    private class WalkData
    {
        public Vector4 MonsterPos = Vector4.zero;
        public Vector4 PrevMonsterPos = Vector4.zero;
        public Vector4 MonsterDir = Vector4.zero;

        public MoveDir DirState = MoveDir.Null;

        public List<Vector4> PathInfo = null;
        public List<Vector4>.Enumerator CurPoint;
        public List<Vector4>.Enumerator NextPoint;
        public Vector4 DestPoint = Vector4.zero;

        public float Ratio = 0.0f;
        public float Time = 0.0f;
    }

    private WalkData Walk = new WalkData();
    public void SetPathInfo(List<Vector4> PathValue)
    {
        Walk.PathInfo = PathValue;
        Walk.CurPoint = Walk.PathInfo.GetEnumerator();
        Walk.CurPoint.MoveNext();

        Walk.NextPoint = Walk.CurPoint;
        Walk.NextPoint.MoveNext();
        Walk.DestPoint = PathValue[PathValue.Count - 1];

        transform.position = Walk.CurPoint.Current;
    }
    protected void WalkPath()
    {
        if (Walk.PathInfo == null)
        {
            Debug.LogAssertion("Monster PathInfo Is null");
            return;
        }

        if (Walk.Ratio >= 1.0f)
        {
            Walk.Time = 0;
            Walk.Ratio = 0;
            Walk.CurPoint.MoveNext();
            if (Walk.NextPoint.MoveNext() == false)
            {
                Destroy(gameObject);
                return;
            }
        }

        WalkToNextPoint();
        CalMonsterDir();
    }
    private void WalkToNextPoint()
    {
        Walk.Time += Time.deltaTime;
        Walk.Ratio = Walk.Time * (Data.Speed / (Walk.NextPoint.Current - Walk.CurPoint.Current).magnitude);
        Walk.MonsterPos = Vector4.Lerp(Walk.CurPoint.Current, Walk.NextPoint.Current, Walk.Ratio);
        gameObject.transform.position = Walk.MonsterPos;
    }
    private void CalMonsterDir()
    {
        Walk.MonsterDir.w = 0.0f;
        Walk.MonsterDir = Walk.MonsterPos - Walk.PrevMonsterPos;
        Walk.MonsterDir.Normalize(); // 지름이 1인 원
        Walk.PrevMonsterPos = Walk.MonsterPos;

        float DegZ = MyMath.GetAngleDegZ(Walk.MonsterDir);

        if (DegZ > 45.0f && DegZ <= 135.0f)
        {
            Walk.DirState = MoveDir.Backward;
        }
        else if (DegZ > 135.0f && DegZ <= 225.0f)
        {
            Walk.DirState = MoveDir.Profile;
            FlipXNegative();
        }
        else if (DegZ > 225.0f && DegZ <= 315.0f)
        {
            Walk.DirState = MoveDir.Forward;
        }
        else if ((DegZ > 315.0f && DegZ <= 360.0f) || (DegZ >= 0.0f && DegZ <= 45.0f))
        {
            Walk.DirState = MoveDir.Profile;
            FlipXPositive();
        }
    }

    // Animation ///////////////////////////////////// 
    private SpriteRenderer MonsterRenderer;
    private Animator MonsterAnimator;
    public int GetCurState()
    {
        return MonsterFSM.GetCurState();
    }
    private void FlipXNegative()
    {
        MonsterRenderer.flipX = true;
    }
    private void FlipXPositive()
    {
        MonsterRenderer.flipX = false;
    }
    IEnumerator FadeAndDestroy()
    {
        Color c = MonsterRenderer.material.color;
        for (float DeathTime = 2f; DeathTime >= 0; DeathTime -= Time.deltaTime)
        {
            c.a = DeathTime / 2;
            MonsterRenderer.material.color = c;
            yield return null;
        }

        Destroy(gameObject);
    }

    // Attack /////////////////////////////////////
    private bool IsAttackCoolTimeEnd = true;
    protected virtual float CalDamage()
    {
        return UnityEngine.Random.Range(Data.Damage_min, Data.Damage_MAX);
    }

    private SC_BaseFighter FindTargetFighter()
    {
        if(AttackFighters.Count == 0)
        {
            return null;
        }

        var enumerator = AttackFighters.GetEnumerator();
        enumerator.MoveNext();
        return enumerator.Current.Value.GetComponent<SC_BaseFighter>();
    }

    public void AttackEvent()
    {
        if (AttackFighters == null)
        {
            return;
        }

        if (AttackFighters.Count == 0)
        {
            return;
        }

        SC_BaseFighter CurTarget = FindTargetFighter();

        if(CurTarget == null)
        {
            Debug.LogAssertion("BaseMonster : CurTarget Is Null");
            return;
        }

        CurTarget.TakeDamage(CalDamage());
        MonsterAnimator.Play("Idle");
    }

    protected void AttackAction()
    {
        SC_BaseFighter CurTarget = FindTargetFighter();

        if (CurTarget == null)
        {
            Debug.LogAssertion("BaseMonster : CurTarget Is Null");
            return;
        }

        if (transform.position.x - CurTarget.transform.position.x > 0)
        {
            MonsterRenderer.flipX = true;
        }
        else if (transform.position.x - CurTarget.transform.position.x < 0)
        {
            MonsterRenderer.flipX = false;
        }

        MonsterAnimator.Play("Attack");
    }

    private IEnumerator Attack(float AttackRate)
    {
        IsAttackCoolTimeEnd = false;
        AttackAction();
        yield return new WaitForSeconds(AttackRate);
        IsAttackCoolTimeEnd = true;
    }

    //FSM /////////////////////////////////////
    protected SC_FSM MonsterFSM;
    protected abstract void StateInit();
    protected virtual void IdleStateInit()
    {
        if (MonsterFSM == null)
        {
            Debug.LogAssertion("MonsterFSM Is null");
            return;
        }

        MonsterFSM.CreateState<MonsterState>(MonsterState.Idle,
            () =>
            {
                MonsterAnimator.Play("Idle");
            },

            () =>
            {
                if (CurHp <= 0f)
                {
                    MonsterFSM.ChangeState(MonsterState.Death);
                }
                else if(AttackFighters.Count == 0)
                {
                    MonsterFSM.ChangeState(MonsterState.Move);
                }
                else
                {
                    var enumerator = AttackFighters.GetEnumerator();
                    enumerator.MoveNext();
                    Vector3 TargetPos = enumerator.Current.Value.transform.position;
                    
                    if ((TargetPos - transform.position).magnitude < 0.36f)
                    {
                        MonsterFSM.ChangeState(MonsterState.Attack);
                    }
                }
            },

            () =>
            {

            }
        );
    }
    protected virtual void MoveStateInit()
    {
        if (MonsterFSM == null)
        {
            Debug.LogAssertion("MonsterFSM Is null");
            return;
        }

        MonsterFSM.CreateState<MonsterState>(MonsterState.Move,
            () =>
            {
                if (Walk.DirState == MoveDir.Profile)
                {
                    MonsterAnimator.Play("Move_Profile");
                }
                else if (Walk.DirState == MoveDir.Forward)
                {
                    MonsterAnimator.Play("Move_Forward");
                }
                else if (Walk.DirState == MoveDir.Backward)
                {
                    MonsterAnimator.Play("Move_Backward");
                }
            },

            () =>
            {
                if (CurHp <= 0f)
                {
                    MonsterFSM.ChangeState(MonsterState.Death);
                    return;
                }

                int PrevDir = (int)Walk.DirState;
                WalkPath();
                if (PrevDir != (int)Walk.DirState)
                {
                    if (Walk.DirState == MoveDir.Profile)
                    {
                        MonsterAnimator.Play("Move_Profile");
                    }
                    else if (Walk.DirState == MoveDir.Forward)
                    {
                        MonsterAnimator.Play("Move_Forward");
                    }
                    else if (Walk.DirState == MoveDir.Backward)
                    {
                        MonsterAnimator.Play("Move_Backward");
                    }
                }
            },

            () =>
            {

            }
        );
    }
    protected virtual void DeathStateInit()
    {
        if (MonsterFSM == null)
        {
            Debug.LogAssertion("MonsterFSM Is null");
            return;
        }

        MonsterFSM.CreateState<MonsterState>(MonsterState.Death,
            () =>
            {
                MonsterAnimator.Play("Death");
                StartCoroutine(FadeAndDestroy());
                Monster2DCol.enabled = false;
                Monster3DCol.enabled = false;
                HpBarInst.SetActive(false);
                BrodcastDeath();
            },

            () =>
            {

            },

            () =>
            {

            }
        );
    }
    protected virtual void AttackStateInit()
    {
        if (MonsterFSM == null)
        {
            Debug.LogAssertion("MonsterFSM Is null");
            return;
        }

        MonsterFSM.CreateState<MonsterState>(MonsterState.Attack,
            () =>
            {
                MonsterAnimator.Play("Idle");
            },

            () =>
            {
                if (CurHp <= 0f)
                {
                    MonsterFSM.ChangeState(MonsterState.Death);
                }
                else if (AttackFighters.Count == 0)
                {
                    MonsterFSM.ChangeState(MonsterState.Move);
                }
                else if (IsAttackCoolTimeEnd)
                {
                    StartCoroutine(Attack(Data.AttackRate));
                }
            },

            () =>
            {

            }
        );
    }
}
