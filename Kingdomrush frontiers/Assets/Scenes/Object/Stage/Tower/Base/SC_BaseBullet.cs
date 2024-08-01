using System.Collections;
using System.Collections.Generic;
using Assets.Scenes.Object.Stage.StageData;
using Assets.Scenes.Object.Stage.ContentsEnum;
using UnityEngine;
using Unity.VisualScripting;

abstract public class SC_BaseBullet : MonoBehaviour
{
    private void Awake()
    {
        BulletRenderer = gameObject.AddComponent<SpriteRenderer>();
        BulletRenderer.sortingOrder = (int)RenderOrder.InGameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected SpriteRenderer BulletRenderer;
    protected TowerData data;
    public TowerData Data
    {
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
    private float Time;
    private float ratio;
    protected float Ratio
    {
        get
        {
            return ratio;
        }
    }


    private bool IsBulletDeath = false;

}
