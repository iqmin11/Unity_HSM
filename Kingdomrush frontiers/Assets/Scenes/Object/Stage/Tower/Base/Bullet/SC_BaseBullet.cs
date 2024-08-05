using System.Collections;
using System.Collections.Generic;
using Assets.Scenes.Object.Stage.StageData;
using Assets.Scenes.Object.Stage.ContentsEnum;
using UnityEngine;
using Unity.VisualScripting;

abstract public class SC_BaseBullet : MonoBehaviour
{
    protected virtual void Awake()
    {
        BulletRenderer = gameObject.AddComponent<SpriteRenderer>();
        BulletRenderer.sortingOrder = (int)RenderOrder.InGameObject;
        gameObject.SetActive(false);
        BulletLayerMask = 1 << LayerMask.NameToLayer("Monster");
    }

    public virtual void BulletSetting(Vector4 StartPos, Vector4 DestPos)
    {
        ShooterPos = StartPos;
        TargetPos = DestPos;
    }

    protected virtual void Start()
    {
        gameObject.transform.position = ShooterPos;
    }

    // Update is called once per frame
    protected virtual void Update()
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
    protected LayerMask BulletLayerMask;
    protected abstract void CalBulletTransform();
    protected abstract void CalBulletRot();
    protected virtual float CalDamage()
    {
        return UnityEngine.Random.Range(Data.Damage_min, Data.Damage_MAX);
    }

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
