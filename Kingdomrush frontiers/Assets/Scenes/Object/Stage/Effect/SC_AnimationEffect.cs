using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_AnimationEffect : SC_BaseEffect
{
    protected virtual void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = (int)RenderOrder.InGameObject;
    }
}
