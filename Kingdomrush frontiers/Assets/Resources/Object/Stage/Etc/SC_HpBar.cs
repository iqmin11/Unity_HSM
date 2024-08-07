using Assets.Scenes.Object.Stage.ContentsEnum;
using UnityEngine;

public class SC_HpBar : MonoBehaviour
{
    private void Awake()
    {
        if(HpBarBg == null)
        {
            HpBarBgSprite = Resources.Load<Sprite>("StageScene/Enemies/lifebar_bg_small");
        }

        transform.localPosition = new Vector3(0, 0, -1);

        HpBarBg = gameObject.AddComponent<SpriteRenderer>();
        HpBarBg.sprite = HpBarBgSprite;
        HpBarBg.sortingOrder = (int)RenderOrder.InGameObject;

        CurHpBarInst = Instantiate(CurHpBarPrefab, transform);
        
        if (BoundSize == Vector4.zero)
        {
            BoundSize = HpBarBg.bounds.size;
            StartPos = new Vector4(0, 0, -1, 0);
            EndPos = new Vector4(-BoundSize.x / 2, 0, -1, 0);
        }
    }

    public void SetCurHp(float Percentage) // Input 0 ~ 1;
    {
        Vector4 tempScale = CurHpBarInst.transform.localScale;
        tempScale.x = Percentage;
        CurHpBarInst.transform.localScale = tempScale;

        Vector4 tempPosition = Vector4.Lerp(StartPos, EndPos, 1 - Percentage);
        CurHpBarInst.transform.localPosition = tempPosition;
    }

    private SpriteRenderer HpBarBg;
    private static Sprite HpBarBgSprite = null;
    private static Vector4 BoundSize = Vector4.zero;
    private static Vector4 StartPos = Vector4.zero;
    private static Vector4 EndPos = Vector4.zero;

    [SerializeField]
    private GameObject CurHpBarPrefab;
    private GameObject CurHpBarInst;
}
