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

    private Vector3 prevPos = Vector3.zero;
    public Vector3 PrevPos
    {
        get
        {
            return prevPos;
        }

        set
        {
            prevPos = value;
        }
    }
    private Vector3 rallyPos = Vector3.zero;
    public Vector3 RallyPos
    {
        get
        {
            return rallyPos;
        }

        set
        {
            rallyPos = value;
        }
    }
    //Vector3 ActorPos = Vector3.zero;

    //FSM ////////////////////////////
    private SC_FSM FighterFSM = null;
    void IdleStateInit()
    {
        if (FighterFSM == null)
        {
            Debug.LogAssertion("MonsterFSM Is null");
            return;
        }

        FighterFSM.CreateState<FighterState>(FighterState.Idle,
            () =>
            {
                FighterAnimator.Play("Idle");
            },

            () =>
            {

            },

            () =>
            {

            }
        );
    }

    void MoveStateInit()
    {

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
