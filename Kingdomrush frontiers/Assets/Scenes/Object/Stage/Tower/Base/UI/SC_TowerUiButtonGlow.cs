using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_TowerUiButtonGlow : MonoBehaviour
{
    private void Awake()
    {
        if(CacheGlowSprite == null)
        {
            CacheGlowSprite = Resources.Load<Sprite>("StageScene/GUI/Button/ButtonsGlow");
        }

        GlowRenderer = gameObject.AddComponent<SpriteRenderer>();
        GlowRenderer.sprite = CacheGlowSprite;
        GlowRenderer.sortingOrder = (int)RenderOrder.InGameUI2;
    }

    private static Sprite CacheGlowSprite = null;
    private SpriteRenderer GlowRenderer = null;
}
