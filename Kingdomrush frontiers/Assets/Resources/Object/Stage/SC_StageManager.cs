using System.Collections.Generic;

using UnityEngine;

using Assets.Scenes.Object.Stage.StageData;
using Assets.Scenes.Object.Base;

public class SC_StageManager : MonoBehaviour
{
    public List<StageData> AllStageData = new List<StageData>();

    private void Awake()
    {
        LoadData();
        StageBGManagerInst = Instantiate(StageBGManagerPrefab);
        StageMouseInsts = Instantiate(StageMousePrefab);
    }

    // Start is called before the first frame update
    void Start()
    {
        CurStage = 0;
        InitStage(CurStage);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            StartWave();
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(CurStage == 0)
            {
                return;
            }

            InitStage(--CurStage);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (CurStage == 5)
            {
                return;
            }

            InitStage(++CurStage);
        }
    }

    //Func For Stage
    private void InitStage(int Stage)
    {
        //PlayStageBGM
        ClearStage();
        CurStage = Stage;
        NextWave = 0;
        MaxWave = AllStageData[CurStage].Waves.Count;
        SetStageBG(CurStage);
        SetStagePath(CurStage);
        SetStageBuildArea(CurStage);
    }

    // MonsterManagerInterface//////////////////////////////////////////////////////
    public static void PushLiveMonster(GameObject MonsterInst)
    {
        LiveMonsterManager.Add(MonsterInst.GetInstanceID(), MonsterInst);
    }
    public static void MonsterDeathNotify(int MonsterInstanceID)
    {
        LiveMonsterManager.Remove(MonsterInstanceID);
    }

    private static SortedDictionary<int, GameObject> LiveMonsterManager = new SortedDictionary<int, GameObject>();

    // WaveManagerInterface//////////////////////////////////////////////////////
    private static SortedDictionary<int, GameObject> LiveWaveManager = new SortedDictionary<int, GameObject>();
    public static void PushLiveWave(GameObject WaveInst)
    {
        LiveMonsterManager.Add(WaveInst.GetInstanceID(), WaveInst);
    }
    public static void WaveEndNotify(int WaveInstanceID)
    {
        LiveWaveManager.Remove(WaveInstanceID);
    }
    private void StartWave()
    {
        if (AllStageData[CurStage].Waves.Count <= NextWave)
        {
            return;
        }

        GameObject CurWave = Instantiate(MonsterWavePrefab);
        SC_MonsterWaveManager CurWaveSC = CurWave.GetComponent<SC_MonsterWaveManager>();
        CurWaveSC.Setting(AllStageData[CurStage].Waves[NextWave++].MonsterSpawn);
    }

    //Use Prefab And Instance
    [SerializeField]
    private GameObject StageBGManagerPrefab;
    private GameObject StageBGManagerInst;

    [SerializeField]
    private GameObject MonsterWavePrefab;

    [SerializeField]
    private GameObject BuildAreaPrefab;
    private List<GameObject> BuildAreaInsts = new List<GameObject>();

    [SerializeField]
    private GameObject StageMousePrefab;
    private GameObject StageMouseInsts;

    private int curstage = -1;
    private int CurStage
    {
        get
        {
            return curstage;
        }

        set
        {
            if (value < 0 || value >= 6)
            {
                return;
            }

            curstage = value;
        }
    }

    private int NextWave = -1;
    private int MaxWave = -1;

    // SetStage//////////////////////////////////////////////////////
    private void SetStageBG(int CurStage)
    {
        StageBGManagerInst.GetComponent<SC_StageBG>().SetStageBG(CurStage);
    }
    private void SetStagePath(int CurStage)
    {
        SC_MonsterWaveManager.CurStagePaths = AllStageData[CurStage].Lines;
    }
    private void SetStageBuildArea(int CurStage)
    {
        BuildAreaInsts.Capacity = AllStageData[CurStage].BuildAreaPos.Count;
        for (int i = 0; i < BuildAreaInsts.Capacity; i++)
        {
            BuildAreaInsts.Add(Instantiate(BuildAreaPrefab, AllStageData[CurStage].BuildAreaPos[i], Quaternion.identity));
            BuildAreaInsts[i].GetComponent<SC_BuildArea>().DefaultRallyPos = AllStageData[CurStage].AreaStartRallyPos[i];
        }
    }

    // ClearStage//////////////////////////////////////////////////////
    private void ClearStage()
    {
        CurStage = -1;
        NextWave = -1;
        MaxWave = -1;
        ClearStagePath();
        ClearStageBuildArea();
        ClearLiveWave();
        ClearLiveMonster();
    }

    private void ClearStagePath()
    {
        SC_MonsterWaveManager.CurStagePaths = null;
    }

    private void ClearStageBuildArea()
    {
        for (int i = 0; i < BuildAreaInsts.Capacity; i++)
        {
            Destroy(BuildAreaInsts[i]);
        }
        BuildAreaInsts.Clear();
    }

    private void ClearLiveWave()
    {
        var CurEnumerator = LiveWaveManager.GetEnumerator();
        while (CurEnumerator.MoveNext())
        {
            Destroy(CurEnumerator.Current.Value);
        }
        LiveWaveManager.Clear();
    }
    private void ClearLiveMonster()
    {
        var CurEnumerator = LiveMonsterManager.GetEnumerator();
        while (CurEnumerator.MoveNext())
        {
            Destroy(CurEnumerator.Current.Value);
        }
        LiveMonsterManager.Clear();
    }

    // LoadData//////////////////////////////////////////////////////
    private void LoadData()
    {
        LoadPathBinData(); //반드시 가장 먼저 로드해야하는 데이터
        LoadWaveBinData();
        LoadAreaBinData();
        LoadRallyBinData();
    }

    // LoadPath
    private void LoadPathBinData()
    {
        TextAsset tempLoadData = Resources.Load<TextAsset>("StageScene/Data/PathData");
        MyDeserializer LoadDesirializer = new MyDeserializer();
        LoadDesirializer.ReadFile(tempLoadData.bytes);
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
            temp = MyMath.CentimeterToMeter(temp); 

            CurLineData.Points.Add(temp);
        }
    }

    // LoadWave
    private void LoadWaveBinData()
    {
        TextAsset tempLoadData = Resources.Load<TextAsset>("StageScene/Data/WaveData");
        MyDeserializer LoadDesirializer = new MyDeserializer();
        LoadDesirializer.ReadFile(tempLoadData.bytes);
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

    // LoadArea 
    private void LoadAreaBinData()
    {
        TextAsset tempLoadData = Resources.Load<TextAsset>("StageScene/Data/BuildAreaData");
        MyDeserializer LoadDesirializer = new MyDeserializer();
        LoadDesirializer.ReadFile(tempLoadData.bytes);
        int StgSize = 0;
        LoadDesirializer.Read(ref StgSize);

        for (int StageIndex = 0; StageIndex < StgSize; StageIndex++)
        {
            LoadOneStageAreas(LoadDesirializer, StageIndex);
        }
    }
    private void LoadOneStageAreas(MyDeserializer Buffer, int StageIndex)
    {
        int AreaSize = 0;
        Buffer.Read(ref AreaSize);
        List<Vector4> CurBuildAreaInfos = AllStageData[StageIndex].BuildAreaPos;
        for (int i = 0; i < AreaSize; i++)
        {
            Vector4 temp = new Vector4(0f, 0f, 0f, 0f);

            Buffer.Read(ref temp.x);
            Buffer.Read(ref temp.y);
            Buffer.Read(ref temp.z);
            Buffer.Read(ref temp.w);
            temp = MyMath.CentimeterToMeter(temp);

            CurBuildAreaInfos.Add(temp);
        }
    }

    // LoadRally
    private void LoadRallyBinData()
    {
        TextAsset tempLoadData = Resources.Load<TextAsset>("StageScene/Data/RallyData");
        MyDeserializer LoadDesirializer = new MyDeserializer();
        LoadDesirializer.ReadFile(tempLoadData.bytes);
        int StgSize = 0;
        LoadDesirializer.Read(ref StgSize);

        for (int StageIndex = 0; StageIndex < StgSize; StageIndex++)
        {
		    LoadOneStageRally(LoadDesirializer, StageIndex);
        }
    }
    private void LoadOneStageRally(MyDeserializer Buffer, int StageIndex)
    {
        int RallySize = 0;
        Buffer.Read(ref RallySize);
        List<Vector4> CurRallyInfo = AllStageData[StageIndex].AreaStartRallyPos;

        for (int i = 0; i < RallySize; i++)
        {
            Vector4 temp = new Vector4(0f, 0f, 0f, 0f);

            Buffer.Read(ref temp.x);
            Buffer.Read(ref temp.y);
            Buffer.Read(ref temp.z);
            Buffer.Read(ref temp.w);
            temp = MyMath.CentimeterToMeter(temp);

            CurRallyInfo.Add(temp);
        }
    }
}
