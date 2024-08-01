using System.Collections;
using System.Collections.Generic;
using Assets.Scenes.Object.Stage.StageData;
using Assets.Scenes.Object.Stage.ContentsEnum;
using UnityEngine;
using Unity.VisualScripting;

abstract public class SC_BaseBullet : MonoBehaviour
{
    virtual protected void Awake()
    {
        BulletRenderer = gameObject.AddComponent<SpriteRenderer>();
        BulletRenderer.sortingOrder = (int)RenderOrder.InGameObject;
        gameObject.SetActive(false);
    }

    virtual public void BulletSetting(Vector4 StartPos, Vector4 DestPos)
    {
        ShooterPos = StartPos;
        TargetPos = DestPos;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        AccTime += Time.deltaTime;
        Ratio = AccTime / Data.BulletTime;
        CalBulletTransform();
    }

    protected SpriteRenderer BulletRenderer;
    protected TowerData data;
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
    abstract protected void CalBulletTransform();
    abstract protected void CalRotBulletRot();

    private Vector4 shooterPos;
    protected Vector4 ShooterPos
    {
        get
        {
            return shooterPos;
        }

        set
        {
            shooterPos = value;
        }
    }

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
    private float AccTime;
    private float ratio;
    protected float Ratio
    {
        get
        {
            return ratio;
        }

        set
        {
            ratio = value;
        }
    }
}
