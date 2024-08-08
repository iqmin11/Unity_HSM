using Assets.Scenes.Object.Stage.StageData;
using System.Collections.Generic;
using UnityEngine;

public class SC_MonsterWaveManager : MonoBehaviour
{
    private void Awake()
    {
        SC_StageManager.PushLiveWave(gameObject);
        if(Clip == null)
        {
            Clip = Resources.Load<AudioClip>("Sounds/PlayStage/UI/Sound_WaveIncoming");
        }
        AudioSource Player = gameObject.AddComponent<AudioSource>();
        Player.clip = Clip;
        Player.Play();
    }

    // static member
    static private List<LinePath> curstagepaths;
    static public List<LinePath> CurStagePaths
    {
        get 
        { 
            return curstagepaths; 
        }

        set
        {
            curstagepaths = value; 
        }
    }
    
    [SerializeField]
    private List<GameObject> MonsterPrefabs;

    //public member
    public void Setting(List<MonsterSpawnData> OneWave)
    {
        for (int i = 0; i < OneWave.Count; i++)
        {
            Queue<MonsterSpawnData> Temp;
            if (SpawnDatas.TryGetValue(OneWave[i].StartTime, out Temp))
            {
                Temp.Enqueue(OneWave[i]);
                continue;
            }

            Temp = new Queue<MonsterSpawnData>();
            Temp.Enqueue(OneWave[i]);
            SpawnDatas.Add(OneWave[i].StartTime, Temp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //몬스터 한 웨이브 정보를 받아서 만들어짐.
        //만들어지게 되면, 몬스터정보를 통해서 몬스터를 소환하고,
        //다 소환하면 사라집니다.

        WaveTime += Time.deltaTime;

        if (SpawnDatas.Count != 0)
        {
            SortedDictionary<float, Queue<MonsterSpawnData>>.Enumerator Top = SpawnDatas.GetEnumerator();
            Top.MoveNext();

            if (Top.Current.Key <= WaveTime)
            {
                Queue<MonsterSpawnData> CurSpawnTop = Top.Current.Value;
                SpawnDatas.Remove(Top.Current.Key);

                while(CurSpawnTop.Count != 0)
                {
                    SpawnMonster(CurSpawnTop.Dequeue());
                }

                if (SpawnDatas.Count == 0)
                {
                    WaveTime = 0; //여기서 코루틴 함수를 딱 스타트해버리면 될듯?
                }
            }
            return;
        }

        //이걸 코루틴으로 만드는것도 생각해봅시다. 이럴필요가 없는거같은데
        if (WaveTime >= WaveEndTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SC_StageManager.WaveEndNotify(gameObject.GetInstanceID());
    }

    //private member
    private void SpawnMonster(MonsterSpawnData CurMonster)
    {
        GameObject SpawnMonster = Instantiate(MonsterPrefabs[(int)CurMonster.Monster]);
        SC_BaseMonster SpawnMonsterSC = SpawnMonster.GetComponent<SC_BaseMonster>();
        SpawnMonsterSC.SetPathInfo(CurStagePaths[CurMonster.LineIndex].Points);
        SpawnMonster.SetActive(true);
    }
    SortedDictionary<float, Queue<MonsterSpawnData>> SpawnDatas = new SortedDictionary<float, Queue<MonsterSpawnData>>();

    float WaveTime = 0.0f;
    float WaveEndTime = 5.0f; //마지막 몬스터가 소환되고 몇초후에 Wave가 끝나는가

    static AudioClip Clip = null;
}
