using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scenes.Object.Stage.StageData;
using UnityEditor.Build.Content;
using Unity.Mathematics;
using Assets.Scenes.Object.Stage.ContentsEnum;

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
    }

    protected virtual void Start()
    {
        CurHp = Data.Hp;
    }

    private FighterData data;
    public FighterData Data
    {
        get
        {
            return data;
        }
    }

    private float curHp = 0f;
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

    private SpriteRenderer FighterRenderer = null;
    protected Animator FighterAnimator = null;
    //private SC_BaseMonster TargetMonster = null;

    private Vector4 prevPos = Vector4.zero;
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
    private Vector4 rallyPos = Vector4.zero;
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
    Vector4 ActorPos = new Vector4(0f, 0f, 0f, 1f);

    float Speed = 1f;
    float MoveTime = 0.0f;
    float MoveRatio = 0.0f;

    void MoveToRally()
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
                MoveRatio = 0.0f;
                MoveTime = 0.0f;
                FighterAnimator.Play("Move");
            },

            () =>
            {
                MoveToRally();
            },

            () =>
            {
                PrevPos = RallyPos;
            }
        );
    }

    void TraceMonsterStateInit()
    {

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
