using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scenes.Object.Stage.StageData;
using UnityEditor.Build.Content;
using Unity.Mathematics;
using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.XR;

public enum FighterState
{
    Born,
    Idle,
    Move,
    TraceMonster,
    Attack,
    Return,
    Death,
    Revive,
    Skill0,
    Skill1
};

public class SC_BaseFighter : MonoBehaviour
{
    protected virtual void Awake()
    {
        //ComponentSetting
        FighterRenderer = GetComponent<SpriteRenderer>();
        if (FighterRenderer == null)
        {
            Debug.LogAssertion("SC_BaseFighter : FighterRenderer is Null");
        }

        FighterRenderer.sortingOrder = (int)RenderOrder.InGameObject;

        FighterAnimator = GetComponent<Animator>();
        if (FighterAnimator == null)
        {
            Debug.LogAssertion("SC_BaseFighter : FighterRenderer is Null");
        }

        FighterFSM = gameObject.AddComponent<SC_FSM>();
        if (FighterFSM == null)
        {
            Debug.LogAssertion("SC_BaseFighter : FighterFSM is Null");
        }

        //FsmSetting
        IdleStateInit();
        MoveStateInit();
        TraceMonsterStateInit();
        AttackStateInit();
        ReturnStateInit();
        DeathStateInit();

        FighterFSM.ChangeState(FighterState.Idle);

        //LayerMaskSetting
        Layer |= (1 << LayerMask.NameToLayer("Monster"));

        //HpBarSetting
        HpBarInst = Instantiate(HpBarPrefab, transform);
        HpBarSetting = HpBarInst.GetComponent<SC_HpBar>();
        HpBarInst.transform.localPosition = HpBarLocalPos;
    }

    protected virtual void Start()
    {
        CurHp = Data.Hp;
    }

    private void Update()
    {
        HpBarSetting.SetCurHp(CurHp / Data.Hp);
    }


    public Vector4 PrevPos
    {
        get
        {
            return prevPos;
        }

        set
        {
            prevPos.x = value.x;
            prevPos.y = value.y;
            prevPos.z = value.z;
            prevPos.w = 1f;
        }
    }
    public Vector4 RallyPos
    {
        get
        {
            return rallyPos;
        }

        set
        {
            rallyPos.x = value.x;
            rallyPos.y = value.y;
            rallyPos.z = value.z;
            rallyPos.w = 1f;
        }
    }
    public bool IsWork
    {
        get
        {
            return isWork;
        }

        set
        {
            isWork = value;
        }
    }
    public void SetTarget(SC_BaseMonster Monster)
    {
        isWork = true;
        TargetMonster = Monster;
        Monster.StartInteractionWithFighter(this);

        FighterFSM.ChangeState(FighterState.TraceMonster);
    }

    public void ClearTarget()
    {
        if(TargetMonster == null || TargetMonster.GetCurState() == (int)MonsterState.Death)
        {
            return;
        }

        TargetMonster.EndInteractionWithFighter(this);
        TargetMonster = null;
        isWork = false;
    }

    public void NotifyMonsterDeath()
    {
        FighterFSM.ChangeState(FighterState.Return);
        isWork = false;
        TargetMonster = null;
    }
    public void TakeDamage(float Damage)
    {
        CurHp -= Damage;
        if (CurHp < 0)
        {
            CurHp = 0;
        }
    }

    [SerializeField]
    private GameObject HpBarPrefab;
    private GameObject HpBarInst;
    private SC_HpBar HpBarSetting;

    private Vector3 HpBarLocalPos = new Vector3(0f, 0.35f, 0f);
    protected float CurHp
    {
        get
        {
            return curHp;
        }

        set
        {
            curHp = value;
        }
    }

    public FighterData Data = new FighterData();
    private float curHp = 0f;

    private SpriteRenderer FighterRenderer = null;
    protected Animator FighterAnimator = null;
    private SC_BaseMonster TargetMonster = null;

    private LayerMask Layer = 0;

    private Vector4 prevPos = Vector4.zero;
    private Vector4 rallyPos = Vector4.zero;
    private Vector4 ActorPos = new Vector4(0f, 0f, 0f, 1f);
    private Vector4 SavePos = new Vector4(0f, 0f, 0f, 1f);

    private float Radius = 0.36f;
    private float Speed = 1f;
    private float MoveTime = 0.0f;
    private float MoveRatio = 0.0f;

    private bool isWork = false;

    protected virtual float CalDamage()
    {
        return UnityEngine.Random.Range(Data.Damage_min, Data.Damage_MAX);
    }

    private bool IsAttackCoolTimeEnd = true;
    public void AttackEvent()
    {
        if(TargetMonster == null)
        {
            return;
        }
        
        FighterAnimator.Play("Idle");
        TargetMonster.TakeDamage(CalDamage());
    }

    protected void AttackAction()
    {
        FighterAnimator.Play("Attack");
    }

    private IEnumerator Attack(float AttackRate)
    {
        IsAttackCoolTimeEnd = false;
        AttackAction();
        yield return new WaitForSeconds(AttackRate);
        IsAttackCoolTimeEnd = true;
    }


    private void MoveToRally()
    {
        MoveTime += Time.deltaTime;
        MoveRatio = MoveTime * (Speed / (RallyPos - PrevPos).magnitude);
        ActorPos = Vector4.Lerp(PrevPos, RallyPos, MoveRatio);
        transform.position = ActorPos;

        if (ActorPos.x - RallyPos.x > 0)
        {
            FighterRenderer.flipX = true;
        }
        else if (ActorPos.x - RallyPos.x < 0)
        {
            FighterRenderer.flipX = false;
        }

        if (MoveRatio >= 1)
        {
            FighterFSM.ChangeState(FighterState.Idle);
        }
    }
    private void MoveToTarget()
    {
        MoveTime += Time.deltaTime;
        Vector4 TargetPos = TargetMonster.transform.position;
        MoveRatio = MoveTime * (Speed / (TargetPos - SavePos).magnitude);
        ActorPos = Vector4.Lerp(SavePos, TargetPos, MoveRatio);

        if (ActorPos.x - TargetPos.x > 0)
        {
            FighterRenderer.flipX = true;
        }
        else if (ActorPos.x - TargetPos.x < 0)
        {
            FighterRenderer.flipX = false;
        }

        transform.position = ActorPos;

        if((TargetPos - ActorPos).magnitude < Radius)
        {
            FighterFSM.ChangeState(FighterState.Attack);
        }
    }
    private void ReturnToRally()
    {
        MoveTime += Time.deltaTime;
        MoveRatio = MoveTime * (Speed / (RallyPos - SavePos).magnitude);
        ActorPos = Vector4.Lerp(SavePos, RallyPos, MoveRatio);

        if (ActorPos.x - RallyPos.x > 0)
        {
            FighterRenderer.flipX = true;
        }
        else if (ActorPos.x - RallyPos.x < 0)
        {
            FighterRenderer.flipX = false;
        }

        transform.position = ActorPos;
    }

    //FSM ////////////////////////////
    private SC_FSM FighterFSM = null;
    void IdleStateInit()
    {
        if (FighterFSM == null)
        {
            Debug.LogAssertion("FighterFSM Is null");
            return;
        }

        FighterFSM.CreateState<FighterState>(FighterState.Idle,
            () =>
            {
                FighterAnimator.Play("Idle");
            },

            () =>
            {
                if (PrevPos != RallyPos)
                {
                    FighterFSM.ChangeState(FighterState.Move);
                    return;
                }
            },

            () =>
            {

            }
        );
    }

    void MoveStateInit()
    {
        if (FighterFSM == null)
        {
            Debug.LogAssertion("FighterFSM Is null");
            return;
        }

        FighterFSM.CreateState<FighterState>(FighterState.Move,
            () =>
            {
                FighterAnimator.Play("Move");
            },

            () =>
            {
                MoveToRally();
            },

            () =>
            {
                PrevPos = RallyPos;
                MoveRatio = 0.0f;
                MoveTime = 0.0f;
            }
        );
    }

    void TraceMonsterStateInit()
    {
        if (FighterFSM == null)
        {
            Debug.LogAssertion("FighterFSM Is null");
            return;
        }

        FighterFSM.CreateState<FighterState>(FighterState.TraceMonster,
            () =>
            {
                FighterAnimator.Play("Move");
                SavePos = transform.position;
            },

            () =>
            {
                MoveToTarget();
            },

            () =>
            {
                MoveRatio = 0.0f;
                MoveTime = 0.0f;
            }
        );
    }

    void AttackStateInit()
    {
        if (FighterFSM == null)
        {
            Debug.LogAssertion("FighterFSM Is null");
            return;
        }

        FighterFSM.CreateState<FighterState>(FighterState.Attack,
            () =>
            {
                FighterAnimator.Play("Idle");
            },

            () =>
            {
                if(IsAttackCoolTimeEnd)
                {
                    StartCoroutine(Attack(Data.AttackRate));
                }
            },

            () =>
            {

            }
        );
    }

    void ReturnStateInit()
    {
        if (FighterFSM == null)
        {
            Debug.LogAssertion("FighterFSM Is null");
            return;
        }

        FighterFSM.CreateState<FighterState>(FighterState.Return,
            () =>
            {
                FighterAnimator.Play("Move");
                SavePos = transform.position;
            },

            () =>
            {
                if(MoveRatio >= 1.0f)
                {
                    FighterFSM.ChangeState(FighterState.Idle);
                    return;
                }

                ReturnToRally();
            },

            () =>
            {
                MoveRatio = 0.0f;
                MoveTime = 0.0f;
            }
        );
    }

    void DeathStateInit()
    {

    }

}
