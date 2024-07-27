using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;
using Assets.Scenes.Object.Stage.ContentsEnum;

namespace Assets.Scenes.Object.Stage.StageData
{
    public class LinePath
    {
        public List<UnityEngine.Vector4> Points = new List<UnityEngine.Vector4>();
    }
    public class MonsterSpawnData
    {
        public MonsterEnum Monster = MonsterEnum.Null;
        public int LineIndex = -1;
        public float StartTime = -1;
    }

    public class WaveData
    {
        public List<MonsterSpawnData> MonsterSpawn = new List<MonsterSpawnData>();
    }

    public class StageData
    {
        public List<LinePath> Lines = new List<LinePath>();
        public List<WaveData> Waves = new List<WaveData>();
        public List<UnityEngine.Vector4> BuildAreaPos = new List<UnityEngine.Vector4>();
        public List<UnityEngine.Vector4> AreaStartRallyPos = new List<UnityEngine.Vector4>();
        public List<List<UnityEngine.Vector4>> WaveStartButtonPos = new List<List<UnityEngine.Vector4>>();
        public int StartGold = -1;
        public UnityEngine.Vector4 HeroStartPos = new UnityEngine.Vector4();
    }
}
