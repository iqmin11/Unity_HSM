using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SC_MyMouseBase : MonoBehaviour
{
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
        RaycastHit2D[] HitInfos = Physics2D.RaycastAll(WorldPos, Vector2.zero);
        Filter.Clear();
        Filter.Capacity = HitInfos.Length;
        HitInfos.OrderBy(HitInfos => 
        HitInfo.collider.GetComponent<SpriteRenderer>().sortingOrder
        );
        for (int i = 0; i < HitInfos.Length; i++)
        {
            if (!(HitInfos[i].collider.gameObject.CompareTag("MyButton")))
            {
                continue;
            }

            Filter.Add(HitInfos[i].collider.gameObject);
        }

        Filter.Sort(
            (Left, Right) =>
            Left.GetComponent<SpriteRenderer>().sortingOrder.
            CompareTo(Right.GetComponent<SpriteRenderer>().sortingOrder)
            );

        if (Filter.Count == 0)
        {
            return;
        }

        Filter[0].GetComponent<SC_MyButton>().Click();
    }

    private Vector2 WorldPos = Vector2.zero;
    private RaycastHit2D HitInfo;
    private Physics2D MousePhysics;
    private List<GameObject> Filter = new List<GameObject>();
}
