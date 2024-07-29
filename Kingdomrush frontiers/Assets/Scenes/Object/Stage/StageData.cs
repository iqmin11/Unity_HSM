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

    public class MonsterData
    {
        public static readonly float Def_None = 0.0f;
        public static readonly float Def_Low = 0.3f;
        public static readonly float Def_Midium = 0.5f;
        public static readonly float Def_High = 0.85f;
        public static readonly float Speed_Slow = 25.0f / 100.0f;
        public static readonly float Speed_Midium = 50.0f / 100.0f;
        public static readonly float Speed_Fast = 100.0f / 100.0f;

        public void SetData(MonsterEnum _MonsterType)
        {
            switch (_MonsterType)
            {
                case MonsterEnum.Null:
                    break;
                case MonsterEnum.DesertThug:
                    MonsterType = _MonsterType;
                    Hp = 50;
                    AttackRate = 1.0f;
                    Damage_min = 2;
                    Damage_MAX = 6;
                    Armor = Def_None;
                    MagicResistance = Def_None;
                    Speed = Speed_Midium;
                    Dodge = 0.0f;
                    LivesTaken = 1;
                    Bounty = 5;
                    IsBurrow = false;
                    IsFlying = false;
                    IsRanged = false;
                    break;
                case MonsterEnum.DuneRaider:
                    MonsterType = _MonsterType;
                    Hp = 160;
                    AttackRate = 1.2f;
                    Damage_min = 6;
                    Damage_MAX = 10;
                    Armor = Def_Low;
                    MagicResistance = Def_None;
                    Speed = Speed_Midium;
                    Dodge = 0.0f;
                    LivesTaken = 1;
                    Bounty = 16;
                    IsBurrow = false;
                    IsFlying = false;
                    IsRanged = false;
                    break;
                case MonsterEnum.DessertArcher:
                    MonsterType = _MonsterType;
                    Hp = 150;
                    AttackRate = 1.0f;
                    Damage_min = 10;
                    Damage_MAX = 20;
                    RangedAttackRate = 1.4f;
                    RangedDamage_min = 20;
                    RangedDamage_MAX = 30;
                    Armor = Def_None;
                    MagicResistance = Def_Low;
                    Speed = Speed_Midium;
                    Dodge = 0.0f;
                    LivesTaken = 1;
                    Bounty = 12;
                    IsBurrow = false;
                    IsFlying = false;
                    IsRanged = true;
                    break;
                case MonsterEnum.SandHound:
                    MonsterType = _MonsterType;
                    Hp = 30;
                    AttackRate = 1.0f;
                    Damage_min = 1;
                    Damage_MAX = 3;
                    Armor = Def_None;
                    MagicResistance = Def_None;
                    Speed = Speed_Fast;
                    Dodge = 0.25f;
                    LivesTaken = 1;
                    Bounty = 5;
                    IsBurrow = false;
                    IsFlying = false;
                    IsRanged = false;
                    break;
                case MonsterEnum.WarHound:
                    MonsterType = _MonsterType;
                    Hp = 100;
                    AttackRate = 1.5f;
                    Damage_min = 12;
                    Damage_MAX = 18;
                    Armor = Def_None;
                    MagicResistance = Def_Midium;
                    Speed = Speed_Fast;
                    Dodge = 0.3f;
                    LivesTaken = 1;
                    Bounty = 10;
                    IsBurrow = false;
                    IsFlying = false;
                    IsRanged = false;
                    break;
                case MonsterEnum.Immortal:
                    MonsterType = _MonsterType;
                    Hp = 290;
                    AttackRate = 1.5f;
                    Damage_min = 12;
                    Damage_MAX = 28;
                    Armor = Def_Midium;
                    MagicResistance = Def_None;
                    Speed = Speed_Midium;
                    Dodge = 0.0f;
                    LivesTaken = 2;
                    Bounty = 24;
                    IsBurrow = false;
                    IsFlying = false;
                    IsRanged = false;
                    break;
                case MonsterEnum.Fallen:
                    MonsterType = _MonsterType;
                    Hp = 100;
                    AttackRate = 1.0f;
                    Damage_min = 12;
                    Damage_MAX = 28;
                    Armor = Def_None;
                    MagicResistance = Def_None;
                    Speed = Speed_Midium;
                    Dodge = 0.0f;
                    LivesTaken = 1;
                    Bounty = 0;
                    IsBurrow = false;
                    IsFlying = false;
                    IsRanged = false;
                    break;
                case MonsterEnum.Executioner:
                    MonsterType = _MonsterType;
                    Hp = 1600;
                    AttackRate = 1.5f;
                    Damage_min = 30;
                    Damage_MAX = 60;
                    Armor = Def_None;
                    MagicResistance = Def_None;
                    Speed = Speed_Slow;
                    Dodge = 0.0f;
                    LivesTaken = 2;
                    Bounty = 130;
                    IsBurrow = false;
                    IsFlying = false;
                    IsRanged = false;
                    break;
                case MonsterEnum.GiantScorpion:
                    MonsterType = _MonsterType;
                    Hp = 400;
                    AttackRate = 1.0f;
                    Damage_min = 12;
                    Damage_MAX = 28;
                    Armor = Def_High;
                    MagicResistance = Def_None;
                    Speed = Speed_Slow;
                    Dodge = 0.0f;
                    LivesTaken = 2;
                    Bounty = 28;
                    IsBurrow = false;
                    IsFlying = false;
                    IsRanged = false;
                    break;
                case MonsterEnum.GiantWasp:
                    MonsterType = _MonsterType;
                    Hp = 70;
                    Armor = Def_None;
                    MagicResistance = Def_None;
                    Speed = Speed_Midium;
                    Dodge = 0.0f;
                    LivesTaken = 1;
                    Bounty = 8;
                    IsBurrow = false;
                    IsFlying = true;
                    IsRanged = false;
                    break;
                case MonsterEnum.GiantWaspQueen:
                    MonsterType = _MonsterType;
                    Hp = 320;
                    Armor = Def_None;
                    MagicResistance = Def_None;
                    Speed = Speed_Midium;
                    Dodge = 0.0f;
                    LivesTaken = 5;
                    Bounty = 40;
                    IsBurrow = false;
                    IsFlying = true;
                    IsRanged = false;
                    break;
                case MonsterEnum.DuneTerror:
                    MonsterType = _MonsterType;
                    Hp = 100;
                    AttackRate = 1.0f;
                    Damage_min = 4;
                    Damage_MAX = 8;
                    Armor = Def_None;
                    MagicResistance = Def_None;
                    Speed = Speed_Midium;
                    Dodge = 0.0f;
                    LivesTaken = 1;
                    Bounty = 10;
                    IsBurrow = true;
                    IsFlying = false;
                    IsRanged = false;
                    break;
                case MonsterEnum.SandWraith:
                    MonsterType = _MonsterType;
                    Hp = 800;
                    AttackRate = 1.0f;
                    Damage_min = 30;
                    Damage_MAX = 60;
                    RangedAttackRate = 1.3f;
                    RangedDamage_min = 20;
                    RangedDamage_MAX = 40;
                    Armor = Def_None;
                    MagicResistance = Def_None;
                    Speed = Speed_Slow;
                    Dodge = 0.0f;
                    LivesTaken = 1;
                    Bounty = 100;
                    IsBurrow = false;
                    IsFlying = false;
                    IsRanged = true;
                    break;
                default:
                    break;
            }
        }

        public MonsterEnum MonsterType = MonsterEnum.Null;
        public float Hp = 0;
        public float AttackRate = -1;
        public int Damage_min = -1;
        public int Damage_MAX = -1;
        public float RangedAttackRate = -1;
        public int RangedDamage_min = -1;
        public int RangedDamage_MAX = -1;
        public float Armor = 0.0f;
        public float MagicResistance = 0.0f;
        public float Speed = 0.0f;
        public float Dodge = 0.0f;
        public int LivesTaken = 0;
        public int Bounty = 0;
        public bool IsBurrow = false;
        public bool IsFlying = false;
        public bool IsRanged = false;
    }
}
