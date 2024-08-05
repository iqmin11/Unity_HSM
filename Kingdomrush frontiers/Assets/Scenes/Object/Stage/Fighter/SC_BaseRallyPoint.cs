using Assets.Scenes.Object.Base;
using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BaseRallyPoint : MonoBehaviour
{
    protected virtual void Awake()
    {
        Pivots.Add(new GameObject());
        Pivots.Add(new GameObject());
        Pivots.Add(new GameObject());

        Pivots[0].transform.SetParent(transform);
        Pivots[1].transform.SetParent(transform);
        Pivots[2].transform.SetParent(transform);

        Pivots[0].transform.localPosition = MyMath.CentimeterToMeter(new Vector3(-25f, 0f, 0f));
        Pivots[1].transform.localPosition = MyMath.CentimeterToMeter(new Vector3(0f, -25f, -25f));
        Pivots[2].transform.localPosition = MyMath.CentimeterToMeter(new Vector3(25f, 0f, 0f));
    }

    virtual protected void SetFighter(int Count, FighterEnum Value)
    {
        for (int i = 0; i < Count; i++)
        {
            FighterInsts.Add(Instantiate(FighterPrefab));
            FighterInsts[FighterInsts.Count - 1].transform.SetParent(transform, false);
            FighterInsts[FighterInsts.Count - 1].transform.position = transform.position;

            SC_BaseFighter CurFighterSetting = FighterInsts[FighterInsts.Count - 1].GetComponent<SC_BaseFighter>();
            CurFighterSetting.PrevPos = transform.position;
            CurFighterSetting.RallyPos = Pivots[i].transform.position;
            CurFighterSetting.Data.SetData(Value);
        }
    }

    [SerializeField]
    private GameObject FighterPrefab;
    protected List<GameObject> FighterInsts = new List<GameObject>();

    private List<GameObject> Pivots = new List<GameObject>();
}
