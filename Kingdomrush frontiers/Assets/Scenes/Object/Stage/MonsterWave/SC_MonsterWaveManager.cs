using Assets.Scenes.Object.Stage.StageData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SC_MonsterWaveManager : MonoBehaviour
{
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

    //public func
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

        //gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
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
            }

            return;
        }

        Destroy(gameObject);
    }

    //private member
    public void SpawnMonster(MonsterSpawnData CurMonster)
    {
        Debug.Log("SummonMonster " + CurMonster.Monster.ToString());
    }
    
    SortedDictionary<float, Queue<MonsterSpawnData>> SpawnDatas = new SortedDictionary<float, Queue<MonsterSpawnData>>();

    float WaveTime = 0.0f;
    float WaveEndTime = 5.0f; //마지막 몬스터가 소환되고 몇초후에 Wave가 끝나는가
}
