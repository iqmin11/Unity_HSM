using Assets.Scenes.Object.Stage.ContentsEnum;
using UnityEngine;

public class SC_CurHpBar : MonoBehaviour
{
    private void Awake()
    {
        if(CurHpBarSprite == null)
        {
            CurHpBarSprite = Resources.Load<Sprite>("StageScene/Enemies/lifebar_small");
        }

        transform.localPosition = new Vector3(0, 0, -1);
        
        CurHpBar = gameObject.AddComponent<SpriteRenderer>();
        CurHpBar.sprite = CurHpBarSprite;
        CurHpBar.sortingOrder = (int)RenderOrder.InGameObject0;
    }

    private static Sprite CurHpBarSprite = null;
    private SpriteRenderer CurHpBar;
}
