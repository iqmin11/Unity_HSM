using Assets.Scenes.Object.Stage.ContentsEnum;
using Assets.Scenes.Object.Stage.StageData;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
public enum ShooterState
{
    Null = -1,
    Idle,
	Attack
};

public enum ShooterDir
{
    Null = -1,
    Forward,
    Backward
}

abstract public class SC_BaseShooter : MonoBehaviour
{
    [SerializeField]
    protected GameObject BulletPrefab;

    virtual protected void Awake()
    {
        ShooterAnimator = gameObject.GetComponent<Animator>();
        ShooterRenderer = gameObject.GetComponent<SpriteRenderer>();
        ShooterRenderer.sortingOrder = (int)RenderOrder.InGameObject;

        //FSM Init
        ShooterFSM = gameObject.AddComponent<SC_FSM>();
        IdleStateInit();
        AttackStateInit();
        ShooterFSM.ChangeState<ShooterState>(ShooterState.Idle);
    }

    private TowerData data;
    public TowerData Data 
    {
        get
        {
            return data;
        }

        set
        {
            data = value;
        }
    }

    protected SpriteRenderer ShooterRenderer;
    protected Animator ShooterAnimator;
    protected SC_FSM ShooterFSM;
    protected ShooterState state;
    public ShooterState State
    {
        get
        {
            return state;
        }

        set
        {
            state = value;
        }
    }
    protected ShooterDir Dir = ShooterDir.Forward;

    virtual protected void Attack()
    {
        SC_BaseBullet CurShotBullet = Instantiate(BulletPrefab).GetComponent<SC_BaseBullet>();
        CurShotBullet.BulletSetting(gameObject.transform.position, TargetPos);
        CurShotBullet.Data = Data;
        CurShotBullet.gameObject.SetActive(true);
    }
    virtual protected void IdleStateInit()
    {
        if (ShooterFSM == null)
        {
            Debug.LogAssertion("ShooterFSM Is null");
            return;
        }

        ShooterFSM.CreateState<ShooterState>(ShooterState.Idle,
            () =>
            {
                ShooterAnimator.SetInteger("ShooterState", (int)State);
            },

            () =>
            {
                if(State == ShooterState.Attack)
                {
                    ShooterFSM.ChangeState(State);
                    return;
                }
            },

            () =>
            {

            }
        );
    }
    virtual protected void AttackStateInit()
    {
        if (ShooterFSM == null)
        {
            Debug.LogAssertion("ShooterFSM Is null");
            return;
        }

        ShooterFSM.CreateState<ShooterState>(ShooterState.Attack,
            () =>
            {
                CheckDir();
                ShooterAnimator.SetInteger("ShooterState", (int)State);
                ShooterAnimator.SetInteger("ShooterDir", (int)Dir);
                //쏘는 애니메이션 실행
                //노티파이 2개.
                //1. 실제로 화살이 나가야 하는 노티파이.
                //2. 끝나면 Idle로 스테이트를 바꿔주는 노티파이
            },

            () =>
            {
                if (State == ShooterState.Idle)
                {
                    ShooterFSM.ChangeState(State);
                    return;
                }
            },

            () =>
            {

            }
        );
    }

    private float Time;
    private Vector4 targetPos;
    public Vector4 TargetPos
    {
        get
        {
            return targetPos;
        }

        set
        {
            targetPos = value;
        }
    }
    private void CheckDir()
    {
        //타겟과 나의 위치를 통해 슈터가 바라볼 방향 정하기
    }
    

}
