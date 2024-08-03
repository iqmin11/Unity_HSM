using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Mathematics;
using UnityEngine;

public sealed class SC_ArtilleryBullet : SC_HowitzerBullet
{
    protected override void Awake()
    {
        base.Awake();

        if (BombSprits.Count == 0)
        {
            BombSprits.Add(Resources.Load<Sprite>("StageScene/Tower/Artillery/ArtilleryBomb/bombs_0001"));
            BombSprits.Add(Resources.Load<Sprite>("StageScene/Tower/Artillery/ArtilleryBomb/bombs_0002"));
            BombSprits.Add(Resources.Load<Sprite>("StageScene/Tower/Artillery/ArtilleryBomb/bombs_0003"));
        }

        BulletRenderer.sprite = BombSprits[0];
    }

    protected override void Start()
    {
        base.Start();
        if(Data.Level >= 1 && Data.Level <= 3)
        {
            BulletRenderer.sprite = BombSprits[Data.Level - 1];
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (Ratio > 1.0f)
        {
            Explosion();
            Destroy(gameObject);
        }
    }

    protected override void CalBulletRot()
    {
        float ZDeg;
        Vector4 V4Deg;
        if (Data.Level == 3)
        {
            ZDeg = Mathf.Atan2(B1.y - B0.y, B1.x - B0.x) * Mathf.Rad2Deg;
            V4Deg = new Vector4(0f, 0f, ZDeg, 1f);
            gameObject.transform.eulerAngles = V4Deg;
            return;
        }

        ZDeg = Mathf.Lerp(0f, 640.0f, Ratio);
        V4Deg = new Vector4(0f, 0f, ZDeg, 1f);
        if (TargetPos.x - ShooterPos.x > 0)
        {
            transform.eulerAngles = -V4Deg;
        }
        else
        {
            transform.eulerAngles = V4Deg;
        }
    }

    private void Explosion()
    {
        Debug.Log("Bomb Explosion");
    }

    static private List<Sprite> BombSprits = new List<Sprite>();
}
