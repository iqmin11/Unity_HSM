using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Assets.Scenes.Object.Base;
using Unity.VisualScripting;
using UnityEditor;

public class SC_RangedBullet : SC_HowitzerBullet
{
    static private Sprite ArrowSprite = null;
    static private Sprite ArrowMissSprite = null;

    protected override void Update()
    {
        base.Update();
        if (Ratio > 1.0f)
        {
            BulletRenderer.sprite = ArrowMissSprite;
            StartColor = BulletRenderer.color;
            StartCoroutine(MissArrow());
            enabled = false;
        }
    }
    override protected void Awake()
    {
        base.Awake();
        
        if (ArrowSprite == null)
        {
            ArrowSprite = Resources.Load<Sprite>("StageScene/Tower/Ranged/RangedTower/arrow");
        }

        if (ArrowMissSprite == null)
        {
            ArrowMissSprite = Resources.Load<Sprite>("StageScene/Tower/Ranged/RangedTower/decal_arrow");
        }

        BulletRenderer.sprite = ArrowSprite;
    }

    override public void BulletSetting(Vector4 StartPos, Vector4 DestPos)
    {
        base.BulletSetting(StartPos, DestPos);
    }

    protected override void CalRotBulletRot()
    {
        float ZDeg = Mathf.Atan2(B1.y - B0.y, B1.x - B0.x) * Mathf.Rad2Deg;
        Vector4 V4Deg = new Vector4(0, 0, ZDeg, 1);
        gameObject.transform.eulerAngles = V4Deg;
    }

    private IEnumerator MissArrow()
    {
        float FadeTime = 1.0f;
        while (FadeTime > 0)
        {
            FadeTime -= Time.deltaTime;
            StartColor.a = FadeTime;
            BulletRenderer.color = StartColor; 

            yield return null;
        }

        StartColor.a = 0f;
        BulletRenderer.color = StartColor;
        
        yield return new WaitForSeconds(1.0f);

        Destroy(gameObject);
    }

    static private readonly Vector4 ArrowColScale = MyMath.CentimeterToMeter(new Vector4(20.0f, 6.0f, 1.0f, 1.0f));
    static private Color StartColor;
}
