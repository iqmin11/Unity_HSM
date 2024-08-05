using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scenes.Object.Stage.StageData;
using UnityEditor.Build.Content;
using Unity.Mathematics;

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
		if(FighterRenderer == null)
		{
			Debug.LogAssertion("SC_BaseFighter : FighterRenderer is Null");
		}

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
    }

    private FighterData Data;
    private SpriteRenderer FighterRenderer = null;
    private Animator FighterAnimator = null;
    private SC_BaseMonster TargetMonster = null;
    
    Vector3 PrevPos = Vector3.zero;
    Vector3 ActorPos = Vector3.zero;


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
