using Assets.Scenes.Object.Base;
using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections.Generic;
using UnityEngine;

public class SC_BaseRallyPoint : MonoBehaviour
{
    // Debug
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(gameObject.transform.position, Radius);
    }

    protected virtual void Awake()
    {
        Pivots.Add(new GameObject("Pivot"));
        Pivots.Add(new GameObject("Pivot"));
        Pivots.Add(new GameObject("Pivot"));

        Pivots[0].transform.SetParent(transform);
        Pivots[1].transform.SetParent(transform);
        Pivots[2].transform.SetParent(transform);

        Pivots[0].transform.localPosition = MyMath.CentimeterToMeter(new Vector4(-25f, 0f, 0f, 1f));
        Pivots[1].transform.localPosition = MyMath.CentimeterToMeter(new Vector4(0f, -25f, -25f, 1f));
        Pivots[2].transform.localPosition = MyMath.CentimeterToMeter(new Vector4(25f, 0f, 0f, 1f));

        Layer |= (1 << LayerMask.NameToLayer("Monster"));
    }

    virtual protected void SetFighter(int Count, FighterEnum Value)
    {
        for (int i = 0; i < Count; i++)
        {
            FighterInsts.Add(Instantiate(FighterPrefab));
            FighterInsts[FighterInsts.Count - 1].transform.SetParent(transform, false);
            FighterInsts[FighterInsts.Count - 1].transform.position = transform.parent.position;

            SC_BaseFighter CurFighterSetting = FighterInsts[FighterInsts.Count - 1].GetComponent<SC_BaseFighter>();
            CurFighterSetting.PrevPos = transform.parent.position;
            CurFighterSetting.RallyPos = Pivots[i].transform.position;
            CurFighterSetting.Data.SetData(Value);

            FighterSettings.Add(CurFighterSetting);
        }
    }

    protected virtual void Update()
    {
        Collider2D[] Hits = Physics2D.OverlapCircleAll(transform.position, Radius, Layer);
        
        if (Hits.Length == 0)
        {
            PrevColCount = 0;
            return;
        }

        int ColCount = Hits.Length;
        ColCount = System.Math.Min(ColCount, FighterInsts.Count);
        
        if(ColCount != PrevColCount)
        {
            FighterSettings[0].ClearTarget();
            FighterSettings[1].ClearTarget();
            FighterSettings[2].ClearTarget();
        }

        FindTarget(Hits);

        PrevColCount = ColCount;
    }

    void FindTarget(Collider2D[] Hits)
    {
        if (Hits.Length == 1)
        {
            if (CanAssignTargetToFighter(0))
            {
                FighterSettings[0].SetTarget(Hits[0].gameObject.GetComponent<SC_Monster2DCol>().ParentMonster);
            }

            if(CanAssignTargetToFighter(1))
            {
                FighterSettings[1].SetTarget(Hits[0].gameObject.GetComponent<SC_Monster2DCol>().ParentMonster);
            }

            if (CanAssignTargetToFighter(2))
            {
                FighterSettings[2].SetTarget(Hits[0].gameObject.GetComponent<SC_Monster2DCol>().ParentMonster);
            }
        }
        else if (Hits.Length == 2)
        {
            if (CanAssignTargetToFighter(0))
            {
                FighterSettings[0].SetTarget(Hits[0].gameObject.GetComponent<SC_Monster2DCol>().ParentMonster);
            }

            if (CanAssignTargetToFighter(1))
            {
                FighterSettings[1].SetTarget(Hits[1].gameObject.GetComponent<SC_Monster2DCol>().ParentMonster);
            }

            if (CanAssignTargetToFighter(2))
            {
                FighterSettings[2].SetTarget(Hits[0].gameObject.GetComponent<SC_Monster2DCol>().ParentMonster);
            }
        }
        else if (Hits.Length >= 3)
        {
            if (CanAssignTargetToFighter(0))
            {
                FighterSettings[0].SetTarget(Hits[0].gameObject.GetComponent<SC_Monster2DCol>().ParentMonster);
            }

            if (CanAssignTargetToFighter(1))
            {
                FighterSettings[1].SetTarget(Hits[1].gameObject.GetComponent<SC_Monster2DCol>().ParentMonster);
            }

            if (CanAssignTargetToFighter(2))
            {
                FighterSettings[2].SetTarget(Hits[2].gameObject.GetComponent<SC_Monster2DCol>().ParentMonster);
            }
        }
    }

    bool CanAssignTargetToFighter(int Index)
    {
        return !FighterSettings[Index].IsWork 
            && FighterSettings[Index].GetCurState() != (int)FighterState.Death 
            && FighterSettings[Index].GetCurState() != (int)FighterState.Move;
    }

    [SerializeField]
    private GameObject FighterPrefab;
    protected List<GameObject> FighterInsts = new List<GameObject>();
    protected List<SC_BaseFighter> FighterSettings = new List<SC_BaseFighter>();

    private List<GameObject> Pivots = new List<GameObject>();

    float Radius = 0.6f;
    LayerMask Layer = 0;
    int PrevColCount = 0;
}
