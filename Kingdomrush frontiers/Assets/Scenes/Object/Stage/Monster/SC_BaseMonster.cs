using Assets.Scenes.Object.Stage.StageData;
using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Mathematics;
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
    // Start is called before the first frame update
    protected virtual void Start() 
    {
        MonsterInit();
        SetData();
        //CurHp = Data.Hp;
        StateInit();
        MonsterFSM.ChangeState(MonsterState.Move);
    }

    // Use Update in FSM
    //private void Update()

    // MonsterBase /////////////////////////////////////
    protected MonsterData Data = new MonsterData();
    abstract protected void SetData();
    private void MonsterInit()
    {
        //Initialize FSM
        MonsterFSM = GetComponent<SC_FSM>();
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

        transform.position = Walk.CurPoint.Current;
    }
    protected void WalkPath()
    {
        if(Walk.PathInfo == null)
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
    private Animator MonsterAnimator;
    private SpriteRenderer MonsterRenderer;
    protected int GetCurState()
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

    //FSM /////////////////////////////////////
    protected SC_FSM MonsterFSM;
    abstract protected void StateInit();
    virtual protected void MoveStateInit()
    {
        if (MonsterFSM == null)
        {
            Debug.LogAssertion("MonsterFSM Is null");
            return;
        }

        MonsterFSM.CreateState<MonsterState>(MonsterState.Move,
            () =>
            {
                MonsterAnimator.SetInteger("MonsterState", GetCurState());
            },

            () =>
            {
                WalkPath();
                MonsterAnimator.SetInteger("MoveDir", (int)Walk.DirState);
            },

            () =>
            {

            }
        );
    }
}
