using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

enum ButtonState
{
    Null = -1,
    Release,
    Hover,
    Press
}

public class SC_MyButton : MonoBehaviour
{
    // Start is called before the first frame update
    protected virtual void Start()
    {
        ButtonRenderer = gameObject.AddComponent<SpriteRenderer>();
        ButtonRenderer.sprite = ReleaseSprite;
        ButtonRenderer.sortingOrder = 1;

        ButtonCol = gameObject.AddComponent<BoxCollider2D>();
        ButtonCol.size = new Vector2(ColScale.x, ColScale.y);
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

    protected Sprite ReleaseSprite;
    protected Sprite HoverSprite;
    protected Sprite PressSprite;
    protected Vector4 ColScale = Vector4.one;
    protected System.Action ClickCallBack;

    private ButtonState State = ButtonState.Null;
    private SpriteRenderer ButtonRenderer;
    private BoxCollider2D ButtonCol;
}
