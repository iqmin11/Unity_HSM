using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scenes.Object.Stage.StageData;
using Assets.Scenes.Object.Stage.ContentsEnum;
using Assets.Scenes.Object.Base;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using Unity.Mathematics;
using UnityEngine.UIElements;

public class SC_StageManager : MonoBehaviour
{
    public List<StageData> AllStageData = new List<StageData>();

    private void Awake()
    {
        LoadData();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadData()
    {
        LoadPathBinData();
    }

    private void LoadPathBinData()
    {
        MyDeserializer LoadDesirializer = new MyDeserializer();
        LoadDesirializer.ReadFile(System.IO.File.ReadAllBytes("Assets/Resource/StageScene/Data/PathData.txt"));
        int StgSize = 0;
        LoadDesirializer.Read(ref StgSize);
        AllStageData.Capacity = StgSize;

        for (int i = 0; i < AllStageData.Capacity; i++)
        {
            AllStageData.Add(new StageData());
            LoadOneStageLines(LoadDesirializer, i);
        }
    }

    private void LoadOneStageLines(MyDeserializer Buffer, int StageIndex)
    {
        int LineSize = 0;
        Buffer.Read(ref LineSize);

        StageData CurStageData = AllStageData[StageIndex];
        CurStageData.Lines.Capacity = LineSize;
        for (int i = 0; i < LineSize; i++)
        {
            CurStageData.Lines.Add(new LinePath());
            LoadOneLine(Buffer, StageIndex, i);
        }
    }

    private void LoadOneLine(MyDeserializer Buffer, int StageIndex, int PathIndex)
    {
        int PointSize = 0;
        Buffer.Read(ref PointSize);
        LinePath CurLineData = AllStageData[StageIndex].Lines[PathIndex];
        CurLineData.Points.Capacity = PointSize;
        for (int i = 0; i < PointSize; i++)
        {
            Vector4 temp = new Vector4(0f, 0f, 0f, 0f);
            float FBuffer = 0.0f;
            Buffer.Read(ref FBuffer);
            temp.x = FBuffer;
            Buffer.Read(ref FBuffer);
            temp.y = FBuffer;
            Buffer.Read(ref FBuffer);
            temp.z = FBuffer;
            Buffer.Read(ref FBuffer);
            temp.w = FBuffer;

            CurLineData.Points.Add(temp);
        }

    }
}
