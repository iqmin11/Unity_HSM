using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    // Start is called before the first frame update
    void Start()
    {
        MonsterFSM = GetComponent<SC_FSM>();
        MoveStateInit();
        MonsterFSM.ChangeState("Move");
    }

    // Use Update in FSM

    private WalkData Walk = new WalkData();
    public void SetPathInfo(List<Vector4> PathValue)
    {
        Walk.PathInfo = PathValue;
        Walk.CurPoint = Walk.PathInfo.GetEnumerator();
        Walk.CurPoint.MoveNext();
        Walk.NextPoint = Walk.PathInfo.GetEnumerator();
        Walk.NextPoint.MoveNext();
        Walk.NextPoint.MoveNext();
    }
    private void WalkToNextPoint()
    {
        Walk.Time += Time.deltaTime;
        Walk.Ratio = Walk.Time * (1 / (Walk.NextPoint.Current - Walk.CurPoint.Current).magnitude);
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


    //FSM /////////////////////////////////////
    private SC_FSM MonsterFSM;
    protected void MoveStateInit()
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
