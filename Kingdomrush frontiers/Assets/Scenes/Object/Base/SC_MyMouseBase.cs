using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SC_MyMouseBase : MonoBehaviour
{
    private void Awake()
    {
        ButtonLayer = 1 << LayerMask.NameToLayer("MyButton");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonUp((int)MouseButton.Left))
        {
            ButtonClick();
        }
    }

    private void ButtonClick()
    {
        RaycastHit2D[] HitInfos = Physics2D.RaycastAll(WorldPos, Vector2.zero, 10.0f, ButtonLayer);
        HitInfos.OrderBy(HitInfos => 
        HitInfo.collider.GetComponent<SpriteRenderer>().sortingOrder
        );

        if (HitInfos.Length == 0)
        {
            return;
        }

        HitInfos[0].collider.gameObject.GetComponent<SC_MyButton>().Click();
    }

    private LayerMask ButtonLayer;
    private Vector2 WorldPos = Vector2.zero;
    private RaycastHit2D HitInfo;
    private Physics2D MousePhysics;
}
