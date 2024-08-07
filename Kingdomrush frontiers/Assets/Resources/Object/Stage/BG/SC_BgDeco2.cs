using Assets.Scenes.Object.Base;
using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BgDeco2 : MonoBehaviour
{
    private void Awake()
    {
        Obj0 = new GameObject("Obj0");
        Obj0Renderer = Obj0.AddComponent<SpriteRenderer>();
        Obj0Renderer.sortingOrder = (int)RenderOrder.InGameObject1;
        Obj0Renderer.sprite = Resources.Load<Sprite>("StageScene/StageBg/StageOBJ/Stage_2_Obj0");
        Obj0.transform.position = MyMath.CentimeterToMeter(new Vector3(-703f, -239f, -239f));
        Obj0.transform.SetParent(transform);

        Obj1 = new GameObject("Obj1");
        Obj1Renderer = Obj1.AddComponent<SpriteRenderer>();
        Obj1Renderer.sortingOrder = (int)RenderOrder.InGameObject1;
        Obj1Renderer.sprite = Resources.Load<Sprite>("StageScene/StageBg/StageOBJ/Stage_2_Obj1");
        Obj1.transform.position = MyMath.CentimeterToMeter(new Vector3(715f, -295f, -295f));
        Obj1.transform.SetParent(transform);
    }

    GameObject Obj0;
    SpriteRenderer Obj0Renderer;

    GameObject Obj1;
    SpriteRenderer Obj1Renderer;
}
