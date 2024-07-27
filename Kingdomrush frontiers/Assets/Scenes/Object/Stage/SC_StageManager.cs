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


    // LoadData//////////////////////////////////////////////////////
    private void LoadData()
    {
        LoadPathBinData();
        LoadWaveBinData();
    }

    // LoadPath
    private void LoadPathBinData()
    {
        MyDeserializer LoadDesirializer = new MyDeserializer();
        LoadDesirializer.ReadFile(System.IO.File.ReadAllBytes("Assets/Resource/StageScene/Data/PathData.txt"));
        int StgSize = 0;
        LoadDesirializer.Read(ref StgSize);
        AllStageData.Capacity = StgSize;

        for (int StageIndex = 0; StageIndex < AllStageData.Capacity; StageIndex++)
        {
            AllStageData.Add(new StageData());
            LoadOneStageLines(LoadDesirializer, StageIndex);
        }
    }

    private void LoadOneStageLines(MyDeserializer Buffer, int StageIndex)
    {
        int LineSize = 0;
        Buffer.Read(ref LineSize);

        List<LinePath> CurLinesData = AllStageData[StageIndex].Lines;
        CurLinesData.Capacity = LineSize;
        for (int LineIndex = 0; LineIndex < LineSize; LineIndex++)
        {
            CurLinesData.Add(new LinePath());
            LoadOneLine(Buffer, StageIndex, LineIndex);
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
            Buffer.Read(ref temp.x);
            Buffer.Read(ref temp.y);
            Buffer.Read(ref temp.z);
            Buffer.Read(ref temp.w);

            CurLineData.Points.Add(temp);
        }
    }

    // LoadWave
    private void LoadWaveBinData()
    {
        MyDeserializer LoadDesirializer = new MyDeserializer();
        LoadDesirializer.ReadFile(System.IO.File.ReadAllBytes("Assets/Resource/StageScene/Data/WaveData.txt"));
        int StgSize = 0;
        LoadDesirializer.Read(ref StgSize);
        
        for (int StageIndex = 0; StageIndex < StgSize; StageIndex++)
        {
            LoadOneStageWave(LoadDesirializer, StageIndex);
        }
    }

    private void LoadOneStageWave(MyDeserializer Buffer, int StageIndex)
    {
        int WaveSize = 0;
        Buffer.Read(ref WaveSize);

        List<WaveData> CurWaveData = AllStageData[StageIndex].Waves;
        CurWaveData.Capacity = WaveSize;
        for (int WaveIndex = 0; WaveIndex < WaveSize; WaveIndex++)
        {
            CurWaveData.Add(new WaveData());
            LoadOneWave(Buffer, StageIndex, WaveIndex);
        }
    }

    private void LoadOneWave(MyDeserializer Buffer, int StageIndex, int WaveIndex)
    {
        int MonsterSpawnDataSize = 0;
        Buffer.Read(ref MonsterSpawnDataSize);
        List<MonsterSpawnData> CurMonsterSpawnData = AllStageData[StageIndex].Waves[WaveIndex].MonsterSpawn;
        CurMonsterSpawnData.Capacity = MonsterSpawnDataSize;

        for (int i = 0; i < MonsterSpawnDataSize; i++)
        {
            MonsterSpawnData temp = new MonsterSpawnData();

            Buffer.Read(ref temp.Monster);
            Buffer.Read(ref temp.LineIndex);
            Buffer.Read(ref temp.StartTime);

            CurMonsterSpawnData.Add(temp);
        }
    }
}
