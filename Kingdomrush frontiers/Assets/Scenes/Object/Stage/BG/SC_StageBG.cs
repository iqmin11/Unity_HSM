using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SC_StageBG : MonoBehaviour
{
    private void Awake()
    {
        BGRenderer = gameObject.AddComponent<SpriteRenderer>();
        BGRenderer.sortingOrder = 0;

        BGSprite.Add(Resources.Load<Sprite>("StageScene/StageBg/Stage_1"));
        BGSprite.Add(Resources.Load<Sprite>("StageScene/StageBg/Stage_2"));
        BGSprite.Add(Resources.Load<Sprite>("StageScene/StageBg/Stage_3"));
        BGSprite.Add(Resources.Load<Sprite>("StageScene/StageBg/Stage_4"));
        BGSprite.Add(Resources.Load<Sprite>("StageScene/StageBg/Stage_5"));
        BGSprite.Add(Resources.Load<Sprite>("StageScene/StageBg/Stage_6"));
    }

    public void SetStageBG(int CurStage)
    {
        BGRenderer.sprite = BGSprite[CurStage];
    }

    private List<Sprite> BGSprite = new List<Sprite>();
    private SpriteRenderer BGRenderer;
}
