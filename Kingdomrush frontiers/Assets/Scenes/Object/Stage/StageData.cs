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
        MonsterEnum Monster;
        int LineIndex;
        float StartTime;
    }

    public class WaveData
    {
        public List<MonsterSpawnData> MonsterSpawn;
    }

    public class StageData
    {
        public List<LinePath> Lines = new List<LinePath>();
        public List<WaveData> Waves;
        public System.Collections.Generic.List<UnityEngine.Vector4> BuildAreaPos;
        public System.Collections.Generic.List<UnityEngine.Vector4> AreaStartRallyPos;
        public List<System.Collections.Generic.List<UnityEngine.Vector4>> WaveStartButtonPos;
        public int StartGold;
        public UnityEngine.Vector4 HeroStartPos;
    }
}
