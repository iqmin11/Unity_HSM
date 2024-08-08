using System.Collections.Generic;

using UnityEngine;

using Assets.Scenes.Object.Base.MyInterface;

public sealed class SC_ArtilleryBullet : SC_HowitzerBullet, IEffectPlayer
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
        BoomSound = Resources.Load<AudioClip>("Sounds/PlayStage/Tower/Artillery/Sound_Bomb1");
    }

    protected override void Start()
    {
        base.Start();
        if (Data.Level >= 1 && Data.Level <= 3)
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

    protected override float CalDamage()
    {
        return base.CalDamage() / 3;
    }

    private void Explosion()
    {
        PlayEffect();

        Collider2D[] Hits0 = Physics2D.OverlapCircleAll(transform.position, Phy0, BulletLayerMask);
        Collider2D[] Hits1 = Physics2D.OverlapCircleAll(transform.position, Phy1, BulletLayerMask);
        Collider2D[] Hits2 = Physics2D.OverlapCircleAll(transform.position, Phy2, BulletLayerMask);

        for (int i = 0; i < Hits0.Length; i++)
        {
            Hits0[i].gameObject.GetComponent<SC_Monster2DCol>().ParentMonster.TakeDamage(CalDamage());
        }

        for (int i = 0; i < Hits1.Length; i++)
        {
            Hits1[i].gameObject.GetComponent<SC_Monster2DCol>().ParentMonster.TakeDamage(CalDamage());
        }

        for (int i = 0; i < Hits2.Length; i++)
        {
            Hits2[i].gameObject.GetComponent<SC_Monster2DCol>().ParentMonster.TakeDamage(CalDamage());
        }
    }

    public void PlayEffect()
    {
        //생애 주기를 같이하면 안되기 때문에 부모로 설정하면 안됩니다.
        GameObject BombExplosionEffectInst = Instantiate(BombExplosionEffectPrefab);
        BombExplosionEffectInst.transform.position = transform.position;
        AudioSource PlaySound = BombExplosionEffectInst.AddComponent<AudioSource>();
        PlaySound.clip = BoomSound;
        PlaySound.volume = 0.5f;
        PlaySound.Play();
    }

    private static List<Sprite> BombSprits = new List<Sprite>();

    [SerializeField]
    GameObject BombExplosionEffectPrefab;
    AudioClip BoomSound; 

    private static float Phy0 = 0.3f;
    private static float Phy1 = 0.6f;
    private static float Phy2 = 0.9f;
}
