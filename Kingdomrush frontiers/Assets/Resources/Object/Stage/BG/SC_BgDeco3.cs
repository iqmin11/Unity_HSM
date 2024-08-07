using Assets.Scenes.Object.Base;
using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BgDeco3 : MonoBehaviour
{
    private void Awake()
    {
        Obj0 = new GameObject("Obj0");
        Obj0Renderer = Obj0.AddComponent<SpriteRenderer>();
        Obj0Renderer.sortingOrder = (int)RenderOrder.InGameObject1;
        Obj0Renderer.sprite = Resources.Load<Sprite>("StageScene/StageBg/StageOBJ/Stage_3_Obj0");
        Obj0.transform.position = MyMath.CentimeterToMeter(new Vector3(750f, -350f, -350f));
        Obj0.transform.SetParent(transform);
    }

    GameObject Obj0;
    SpriteRenderer Obj0Renderer;
}
