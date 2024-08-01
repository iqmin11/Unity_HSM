using Assets.Scenes.Object.Stage.ContentsEnum;
using Assets.Scenes.Object.Stage.StageData;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

abstract public class SC_BaseTower : MonoBehaviour
{
    virtual protected void Awake()
    {
        SpriteCaching();

        TowerRenderer = gameObject.AddComponent<SpriteRenderer>();
        TowerRenderer.sortingOrder = (int)RenderOrder.InGameObject;

        InitData();
    }

    // Update is called once per frame
    virtual protected void Update()
    {

    }

    abstract protected void SpriteCaching();
    abstract protected void InitData();

    protected TowerData Data = new TowerData();
    protected List<Sprite> TowerSprite = null;
    protected SpriteRenderer TowerRenderer;
}
