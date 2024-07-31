using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
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
    // Start is called before the first frame update
    virtual protected void Start()
    {
        //FSM Init
        ShooterFSM = gameObject.AddComponent<SC_FSM>();
        IdleStateInit();
        AttackStateInit();
        ShooterFSM.ChangeState<ShooterState>(ShooterState.Idle);

        ShooterAnimator = gameObject.GetComponent<Animator>();
        ShooterRenderer = gameObject.GetComponent<SpriteRenderer>();
        ShooterRenderer.sortingOrder = (int)RenderOrder.InGameObject;
    }

    protected GameObject parenttower = null;
    public GameObject ParentTower
    {
        get
        {
            return parenttower;
        }

        set
        {
            parenttower = value;
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
    public ShooterDir Dir = ShooterDir.Forward;

    abstract protected void Attack();
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
                ShooterAnimator.SetInteger("State", (int)State);
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
                ShooterAnimator.SetInteger("State", (int)State);
                ShooterAnimator.SetInteger("ShooterDir", (int)Dir);
                //��� �ִϸ��̼� ����
                //��Ƽ���� 2��.
                //1. ������ ȭ���� ������ �ϴ� ��Ƽ����.
                //2. ������ Idle�� ������Ʈ�� �ٲ��ִ� ��Ƽ����
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
    private Vector4 TargetPos;
    private void CheckDir()
    {
        //Ÿ�ٰ� ���� ��ġ�� ���� ���Ͱ� �ٶ� ���� ���ϱ�
    }

}
