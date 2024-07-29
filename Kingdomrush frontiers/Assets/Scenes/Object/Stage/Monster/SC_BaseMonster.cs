using Assets.Scenes.Object.Stage.StageData;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

enum MonsterState
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

public class SC_BaseMonster : MonoBehaviour
{
    // Start is called before the first frame update
    protected virtual void Start() 
    {
        MonsterInit();
    }

    // Use Update in FSM
    //private void Update()

    // private member /////////////////////////////////////
    private MonsterData data = new MonsterData();
    public MonsterData Data
    {
        get
        {
            return data;
        }
    }

    private void MonsterInit()
    {
        //Initialize FSM
        MonsterFSM = GetComponent<SC_FSM>();
        if (MonsterFSM == null)
        {
            Debug.LogAssertion("MonsterFSM is null");
        }
    }


    // Walk ///////////////////////////////////// 
    private class WalkData
    {
        public Vector4 MonsterPos = Vector4.zero;
        public Vector4 PrevMonsterPos = Vector4.zero;
        public Vector4 ActorDir = Vector4.zero;

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
    private void WalkToNextPoint()
    {
        Walk.Time += Time.deltaTime;
        Walk.Ratio = Walk.Time * (Data.Speed / (Walk.NextPoint.Current - Walk.CurPoint.Current).magnitude);
        Walk.MonsterPos = Vector4.Lerp(Walk.CurPoint.Current, Walk.NextPoint.Current, Walk.Ratio);
        gameObject.transform.position = Walk.MonsterPos;
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
    }

    // Animation ///////////////////////////////////// 
    string GetCurState()
    {
        return MonsterFSM.GetCurState();
    } // 이걸 Notify하여 애니메이션 변환;(나중에), 아니면 State변환안에 Notify? 나중에 생각합시다.

    //FSM /////////////////////////////////////
    protected SC_FSM MonsterFSM;
    virtual protected void MoveStateInit()
    {
        if (MonsterFSM == null)
        {
            Debug.LogAssertion("MonsterFSM Is null");
            return;
        }

        MonsterFSM.CreateState("Move",
            () =>
            {
                Debug.Log("MoveState Start");
            },

            () =>
            {
                WalkPath();
            },

            () =>
            {
                Debug.Log("MoveState End");
            }
        );
    }
}
