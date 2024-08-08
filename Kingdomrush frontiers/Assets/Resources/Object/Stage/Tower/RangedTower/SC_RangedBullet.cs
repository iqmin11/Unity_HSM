using System.Collections;
using UnityEngine;

sealed public class SC_RangedBullet : SC_HowitzerBullet
{
    //Debug
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(gameObject.transform.position, ArrowColScale);
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
        InitSoundClips();
    }
    protected override void Update()
    {
        base.Update();
        Collider[] Hits = Physics.OverlapSphere(transform.position, ArrowColScale, BulletLayerMask);

        //Hit
        if (Hits.Length >= 1)
        {
            Hits[0].gameObject.GetComponent<SC_Monster3DCol>().ParentMonster.TakeDamage(CalDamage());
            PlaySound(Random.Range(0,2).ToString());
            Destroy(gameObject);
            return;
        }

        //Miss
        if (Ratio > 1.0f)
        {
            BulletRenderer.sprite = ArrowMissSprite;
            StartColor = BulletRenderer.color;
            StartCoroutine(MissArrow());
            enabled = false;
        }
    }

    protected override void CalBulletRot()
    {
        float ZDeg = Mathf.Atan2(B1.y - B0.y, B1.x - B0.x) * Mathf.Rad2Deg;
        Vector4 V4Deg = new Vector4(0, 0, ZDeg, 1);
        gameObject.transform.eulerAngles = V4Deg;
    }

    private IEnumerator MissArrow()
    {
        float FadeTime = 1.0f;
        while (FadeTime > 0f)
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

    static private readonly float ArrowColScale = 0.03f;
    static private Color StartColor;
    static private Sprite ArrowSprite = null;
    static private Sprite ArrowMissSprite = null;

    // Sound /////////////////////////////////////////////

    private void InitSoundClips()
    {
        AddAudioClip("0", "Sounds/PlayStage/Tower/Ranged/Sound_ArrowRelease2");
        AddAudioClip("1", "Sounds/PlayStage/Tower/Ranged/Sound_ArrowRelease3");
    }
}
