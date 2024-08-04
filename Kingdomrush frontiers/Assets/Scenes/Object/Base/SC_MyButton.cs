using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public enum ButtonState
{
    Null = -1,
    Release,
    Hover,
    Press
}

abstract public class SC_MyButton : MonoBehaviour
{
    //리소스 캐싱을 위해 만들면 좋은 인터페이스(권장합니다)
    //static Sprite CacheReleaseSprite = null;
    //static Sprite CacheHoverSprite = null;
    //static Sprite CachePressSprite = null;

    virtual protected void Awake()
    {
        ButtonRenderer = gameObject.AddComponent<SpriteRenderer>();
        ButtonRenderer.sprite = ReleaseSprite;
        SettingButtonRenderOrder();

        ButtonCol = gameObject.AddComponent<BoxCollider2D>();
        ButtonCol.size = new Vector2(ColScale.x, ColScale.y);
        gameObject.tag = "MyButton";
        gameObject.layer = LayerMask.NameToLayer("MyButton");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D HitInfo = Physics2D.Raycast(MousePos, Vector2.zero);

        if (HitInfo.collider == ButtonCol)
        {
            if (Input.GetMouseButtonDown((int)MouseButton.Left))
            {
                State = ButtonState.Press;
                ButtonRenderer.sprite = PressSprite;
            }
            else
            {
                if (State != ButtonState.Hover)
                {
                    State = ButtonState.Hover;
                    ButtonRenderer.sprite = HoverSprite;
                }
            }
        }
        else
        {
            if (State != ButtonState.Release)
            {
                State = ButtonState.Release;
                ButtonRenderer.sprite = ReleaseSprite;
            }
        }
    }
    
    protected abstract void SettingButtonRenderOrder();
    
    protected Sprite ReleaseSprite;
    protected Sprite HoverSprite;
    protected Sprite PressSprite;
    protected Vector4 ColScale = Vector4.one;
    protected System.Action ClickCallBack;
    public System.Action Click
    {
        get
        {
            return ClickCallBack;
        }
        set
        {
            ClickCallBack = value;
        }
    }

    protected SpriteRenderer ButtonRenderer;
    private ButtonState state = ButtonState.Null;
    protected ButtonState State
    {
        get
        {
            return state;
        }

        private set
        {
            state = value;
        }
    }
    private BoxCollider2D ButtonCol;
}
