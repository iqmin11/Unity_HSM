using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections;
using UnityEngine;

public class SC_Lv4ArtilleryAttackEffect : SC_BaseEffect
{
    static private Sprite CacheSprite = null;
    protected virtual void Awake()
    {
        if (CacheSprite == null)
        {
            CacheSprite = Resources.Load<Sprite>("StageScene/Tower/Artillery/ArtilleryBomb/EarthquakeTower_HitDecal3");
        }

        EffectSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        EffectSpriteRenderer.sprite = CacheSprite;
        EffectSpriteRenderer.sortingOrder = (int)RenderOrder.InGameObject0;
        StartCoroutine(StartEffect());
    }

    IEnumerator StartEffect()
    {
        Color c = EffectSpriteRenderer.color;
        Vector3 SetScale = Vector3.zero;
        float Ratio = 1f;
        float EffectTime = 0.2f;
        for(float CurTime = 0; CurTime <= EffectTime; CurTime += Time.deltaTime)
        {
            Ratio = CurTime / EffectTime;
            c.a = 1f - Ratio;
            EffectSpriteRenderer.material.color = c;

            float Scalef = Mathf.Lerp(1f, 3f, Ratio);

            SetScale.x = Scalef;
            SetScale.y = Scalef;
            SetScale.z = 1;
            transform.localScale = SetScale;
            yield return null;
        }

        EffectEndEvent();
    }

    private SpriteRenderer EffectSpriteRenderer;
}
