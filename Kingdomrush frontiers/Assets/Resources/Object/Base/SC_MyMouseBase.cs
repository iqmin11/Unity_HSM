using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SC_MyMouseBase : MonoBehaviour
{
    static public SC_MyMouseBase MouseSetting = null;

    private void Awake()
    {
        MouseSetting = this;
        ButtonLayer |= (1 << LayerMask.NameToLayer("MyButton"));
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
            for(int i = 0; i < ReleaseClickEvents.Count; i++)
            {
                ReleaseClickEvents[i]();
            }
            return;
        }

        HitInfos[0].collider.gameObject.GetComponent<SC_MyButton>().Click();
    }

    public void RegistReleaseClickEvent(System.Action Event)
    {
        ReleaseClickEvents.Add(Event);
    }

    //관찰자 패턴 활용?
    private List<System.Action> ReleaseClickEvents = new List<System.Action>();

    private LayerMask ButtonLayer;
    private Vector2 WorldPos = Vector2.zero;
    private RaycastHit2D HitInfo;
    private Physics2D MousePhysics;
    
}
