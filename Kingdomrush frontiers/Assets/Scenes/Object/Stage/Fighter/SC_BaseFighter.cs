using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scenes.Object.Stage.StageData;
using UnityEditor.Build.Content;
using Unity.Mathematics;
using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Runtime.InteropServices.WindowsRuntime;

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

        IdleStateInit();
        MoveStateInit();
        TraceMonsterStateInit();
        AttackStateInit();
        ReturnStateInit();
        DeathStateInit();

        FighterFSM.ChangeState(FighterState.Idle);

        Layer |= (1 << LayerMask.NameToLayer("Monster"));
    }

    protected virtual void Start()
    {
        CurHp = Data.Hp;
    }


    public FighterData Data
    {
        get
        {
            return data;
        }
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
        Debug.Log("Fighter Call SetTarget");
        isWork = true;
        TargetMonster = Monster;
        Monster.StartInteractionWithFighter(this);

        FighterFSM.ChangeState(FighterState.TraceMonster);
    }

    public void ClearTarget()
    {
        if(TargetMonster == null)
        {
            return;
        }

        Debug.Log("Fighter Call ClearTarget");

        isWork = false;
        TargetMonster.EndInteractionWithFighter(this);
        TargetMonster = null;
    }

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


    private FighterData data = new FighterData();
    private float curHp = 0f;

    private SpriteRenderer FighterRenderer = null;
    protected Animator FighterAnimator = null;
    private SC_BaseMonster TargetMonster = null;

    private LayerMask Layer = 0;

    private Vector4 prevPos = Vector4.zero;
    private Vector4 rallyPos = Vector4.zero;
    private Vector4 ActorPos = new Vector4(0f, 0f, 0f, 1f);
    private Vector4 SavePos = new Vector4(0f, 0f, 0f, 1f);


    private float Speed = 1f;
    private float MoveTime = 0.0f;
    private float MoveRatio = 0.0f;

    private bool isWork = false;

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
                PrevPos = RallyPos;
                MoveRatio = 0.0f;
                MoveTime = 0.0f;
            }
        );
    }

    void AttackStateInit()
    {
        
    }

    void ReturnStateInit()
    {

    }

    void DeathStateInit()
    {

    }

}
