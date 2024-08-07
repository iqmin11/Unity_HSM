using Assets.Scenes.Object.Base;
using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BgDeco0 : MonoBehaviour
{
    private void Awake()
    {
        HammerObj = new GameObject("HammerObject");
        HammerObjRenderer = HammerObj.AddComponent<SpriteRenderer>();
        HammerObjRenderer.sortingOrder = (int)RenderOrder.InGameObject0;
        HammerObjRenderer.sprite = Resources.Load<Sprite>("StageScene/StageBg/StageOBJ/Stage_0_Hammer");
        HammerObj.transform.position = MyMath.CentimeterToMeter(new Vector3(0, 30, 30));
        HammerObj.transform.SetParent(transform);

        WallObj = new GameObject("WallObj");
        WallObjRenderer = WallObj.AddComponent<SpriteRenderer>();
        WallObjRenderer.sortingOrder = (int)RenderOrder.InGameObject1;
        WallObjRenderer.sprite = Resources.Load<Sprite>("StageScene/StageBg/StageOBJ/Stage_0_Wall");
        WallObj.transform.position = MyMath.CentimeterToMeter(new Vector3(-676, -94, -94));
        WallObj.transform.SetParent(transform);

        CastleFlag0 = new GameObject("CastleFlag0");
        CastleFlag0Renderer = CastleFlag0.AddComponent<SpriteRenderer>();
        CastleFlag0Renderer.sortingOrder = (int)RenderOrder.InGameObject1;
        CastleFlag0Animator = CastleFlag0.AddComponent<Animator>();
        CastleFlag0Animator.runtimeAnimatorController = Resources.Load<AnimatorOverrideController>("Object/Stage/BG/AC_BgDecoFlag");
        CastleFlag0.transform.position = MyMath.CentimeterToMeter(new Vector3(-737, 106, 106));
        CastleFlag0.transform.SetParent(transform);

        CastleFlag1 = new GameObject("CastleFlag1");
        CastleFlag1Renderer = CastleFlag1.AddComponent<SpriteRenderer>();
        CastleFlag1Renderer.sortingOrder = (int)RenderOrder.InGameObject1;
        CastleFlag1Animator = CastleFlag1.AddComponent<Animator>();
        CastleFlag1Animator.runtimeAnimatorController = Resources.Load<AnimatorOverrideController>("Object/Stage/BG/AC_BgDecoFlag");
        CastleFlag1.transform.position = MyMath.CentimeterToMeter(new Vector3(-718, 235, 235));
        CastleFlag1.transform.SetParent(transform);
    }

    GameObject HammerObj;
    SpriteRenderer HammerObjRenderer;

    GameObject WallObj;
    SpriteRenderer WallObjRenderer;
    
    GameObject CastleFlag0;
    SpriteRenderer CastleFlag0Renderer;
    Animator CastleFlag0Animator;

    GameObject CastleFlag1;
    SpriteRenderer CastleFlag1Renderer;
    Animator CastleFlag1Animator;
}
