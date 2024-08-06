using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
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
        public static readonly float Speed_Slow = Base.MyMath.CentimeterToMeter(25.0f);
        public static readonly float Speed_Midium = Base.MyMath.CentimeterToMeter(50.0f);
        public static readonly float Speed_Fast = Base.MyMath.CentimeterToMeter(100.0f);

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

    public struct TowerData
    {
        public static readonly float Short = Base.MyMath.CentimeterToMeter(140.0f);
        public static readonly float Average = Base.MyMath.CentimeterToMeter(160.0f);
        public static readonly float Long = Base.MyMath.CentimeterToMeter(180.0f);
        public static readonly float Great = Base.MyMath.CentimeterToMeter(198.0f);

        public TowerData GetNextTowerData()
        {
            if (Level == 4)
            {
                return this;
            }
            TowerData ResultDataTowerType = new TowerData();
            ResultDataTowerType.SetData((TowerEnum)(((int)TowerType) + Level + 1));
            return ResultDataTowerType;
        }

        public float GetNextLvRange()
        {
            TowerEnum NextEnum = (TowerEnum)(((int)TowerType) + Level + 1);
            TowerData NextData = new TowerData();
            NextData.SetData(NextEnum);
            if (NextData.TowerType != TowerType)
            {
                UnityEngine.Debug.Log("Over Index");
                return this.Range;
            }

            return NextData.Range;
        }

        public void SetData(TowerEnum Enum)
        {
            switch (Enum)
            {
                case TowerEnum.Null:
                    break;
                case TowerEnum.RangedTower_CityTower:
                    TowerType = TowerEnum.RangedTower;
                    Level = 0;
                    FireRate = 0.8f;
                    Range = Short;
                    Damage_min = 4;
                    Damage_MAX = 6;
                    BulletTime = 1.0f;
                    BuildCost = 0;
                    SellCost = 0;
                    break;
                case TowerEnum.RangedTower_Level1:
                    TowerType = TowerEnum.RangedTower;
                    Level = 1;
                    FireRate = 0.8f;
                    Range = Short;
                    Damage_min = 4;
                    Damage_MAX = 6;
                    BulletTime = 1.0f;
                    BuildCost = 70;
                    SellCost = 70;
                    break;
                case TowerEnum.RangedTower_Level2:
                    TowerType = TowerEnum.RangedTower;
                    Level = 2;
                    FireRate = 0.6f;
                    Range = Average;
                    Damage_min = 7;
                    Damage_MAX = 11;
                    BulletTime = 1.0f;
                    BuildCost = 110;
                    SellCost = 180;
                    break;
                case TowerEnum.RangedTower_Level3:
                    TowerType = TowerEnum.RangedTower;
                    Level = 3;
                    FireRate = 0.5f;
                    Range = Long;
                    Damage_min = 10;
                    Damage_MAX = 16;
                    BulletTime = 1.0f;
                    BuildCost = 160;
                    SellCost = 340;
                    break;
                case TowerEnum.RangedTower_Level4:
                    TowerType = TowerEnum.RangedTower;
                    Level = 4;
                    FireRate = 0.5f;
                    Range = Great;
                    Damage_min = 15;
                    Damage_MAX = 23;
                    BulletTime = 1.0f;
                    BuildCost = 230;
                    SellCost = 570;
                    break;
                case TowerEnum.MeleeTower_Level1:
                    TowerType = TowerEnum.MeleeTower;
                    Level = 1;
                    FireRate = 1.0f;
                    Range = Short;
                    Damage_min = 1;
                    Damage_MAX = 3;
                    BuildCost = 70;
                    SellCost = 70;
                    break;
                case TowerEnum.MeleeTower_Level2:
                    TowerType = TowerEnum.MeleeTower;
                    Level = 2;
                    FireRate = 1.36f;
                    Range = Short;
                    Damage_min = 3;
                    Damage_MAX = 4;
                    BuildCost = 110;
                    SellCost = 180;
                    break;
                case TowerEnum.MeleeTower_Level3:
                    TowerType = TowerEnum.MeleeTower;
                    Level = 3;
                    FireRate = 1.36f;
                    Range = Short;
                    Damage_min = 6;
                    Damage_MAX = 10;
                    BuildCost = 160;
                    SellCost = 340;
                    break;
                case TowerEnum.MeleeTower_Level4:
                    TowerType = TowerEnum.MeleeTower;
                    Level = 4;
                    FireRate = 1.0f;
                    Range = Short;
                    Damage_min = 10;
                    Damage_MAX = 14;
                    BuildCost = 230;
                    SellCost = 570;
                    break;
                case TowerEnum.MagicTower_Level1:
                    TowerType = TowerEnum.MagicTower;
                    Level = 1;
                    FireRate = 1.5f;
                    Range = Short;
                    Damage_min = 9;
                    Damage_MAX = 17;
                    BulletTime = 0.4f;
                    BuildCost = 100;
                    SellCost = 100;
                    break;
                case TowerEnum.MagicTower_Level2:
                    TowerType = TowerEnum.MagicTower;
                    Level = 2;
                    FireRate = 1.5f;
                    Range = Average;
                    Damage_min = 23;
                    Damage_MAX = 43;
                    BulletTime = 0.4f;
                    BuildCost = 160;
                    SellCost = 260;
                    break;
                case TowerEnum.MagicTower_Level3:
                    TowerType = TowerEnum.MagicTower;
                    Level = 3;
                    FireRate = 1.5f;
                    Range = Long;
                    Damage_min = 40;
                    Damage_MAX = 74;
                    BulletTime = 0.4f;
                    BuildCost = 240;
                    SellCost = 500;
                    break;
                case TowerEnum.MagicTower_Level4:
                    TowerType = TowerEnum.MagicTower;
                    Level = 4;
                    FireRate = 1.5f;
                    Range = Great;
                    Damage_min = 60;
                    Damage_MAX = 120;
                    BulletTime = 0.4f;
                    BuildCost = 300;
                    SellCost = 800;
                    break;
                case TowerEnum.ArtilleryTower_Level1:
                    TowerType = TowerEnum.ArtilleryTower;
                    Level = 1;
                    FireRate = 3.0f;
                    Range = Average;
                    Damage_min = 8;
                    Damage_MAX = 15;
                    BulletTime = 1.0f;
                    BuildCost = 125;
                    SellCost = 125;
                    break;
                case TowerEnum.ArtilleryTower_Level2:
                    TowerType = TowerEnum.ArtilleryTower;
                    Level = 2;
                    FireRate = 3.0f;
                    Range = Average;
                    Damage_min = 20;
                    Damage_MAX = 40;
                    BulletTime = 1.0f;
                    BuildCost = 220;
                    SellCost = 345;
                    break;
                case TowerEnum.ArtilleryTower_Level3:
                    TowerType = TowerEnum.ArtilleryTower;
                    Level = 3;
                    FireRate = 3.0f;
                    Range = Long;
                    Damage_min = 30;
                    Damage_MAX = 60;
                    BulletTime = 1.0f;
                    BuildCost = 320;
                    SellCost = 665;
                    break;
                case TowerEnum.ArtilleryTower_Level4:
                    TowerType = TowerEnum.ArtilleryTower;
                    Level = 4;
                    FireRate = 3.0f;
                    Range = Long;
                    Damage_min = 25;
                    Damage_MAX = 45;
                    BulletTime = 1.0f;
                    BuildCost = 400;
                    SellCost = 1065;
                    break;
                default:
                    UnityEngine.Debug.LogAssertion("Wrong TowerTypeEnum Value");
                    break;
            }
        }

        public static bool IsRangedTower(TowerEnum Enum)
        {
            return !(TowerEnum.RangedTower_Level1 > Enum || TowerEnum.RangedTower_Level4 < Enum);
        }

        public static bool IsMeleeTower(TowerEnum Enum)
        {
            return !(TowerEnum.MeleeTower_Level1 > Enum || TowerEnum.MeleeTower_Level4 < Enum);
        }

        public static bool IsMagicTower(TowerEnum Enum)
        {
            return !(TowerEnum.MagicTower_Level1 > Enum || TowerEnum.MagicTower_Level4 < Enum);
        }

        public static bool IsArtilleryTower(TowerEnum Enum)
        {
            return !(TowerEnum.ArtilleryTower_Level1 > Enum || TowerEnum.ArtilleryTower_Level4 < Enum);
        }

        public TowerEnum TowerType;
        public int Level;
        public float FireRate;
        public float Range;
        public int Damage_min;
        public int Damage_MAX;
        public float BulletTime;
        public int BuildCost;
        public int SellCost;
    }


    public struct FighterData
    {
        public void SetData(int TowerLevel)
        {
            if (TowerLevel <= 1 && TowerLevel >= 4)
            {
                UnityEngine.Debug.LogAssertion("Wrong Tower Level Index");
                return;
            }
            SetData((FighterEnum)TowerLevel);
        }
        public void SetData(FighterEnum FighterEnum)
        {
            switch (FighterEnum)
            {
                case FighterEnum.MeleeLv1:
                    FighterType = FighterEnum.Melee;
                    Level = 1;
                    Hp = 50.0f;
                    AttackRate = 1.0f;
                    Damage_min = 1;
                    Damage_MAX = 3;
                    RangedAttackRate = -1;
                    RangedDamage_min = -1;
                    RangedDamage_MAX = -1;
                    Armor = 0.0f;
                    break;
                case FighterEnum.MeleeLv2:
                    FighterType = FighterEnum.Melee;
                    Level = 2;
                    Hp = 100.0f;
                    AttackRate = 1.36f;
                    Damage_min = 3;
                    Damage_MAX = 4;
                    RangedAttackRate = -1;
                    RangedDamage_min = -1;
                    RangedDamage_MAX = -1;
                    Armor = 0.15f;
                    break;
                case FighterEnum.MeleeLv3:
                    FighterType = FighterEnum.Melee;
                    Level = 3;
                    Hp = 150.0f;
                    AttackRate = 1.36f;
                    Damage_min = 6;
                    Damage_MAX = 10;
                    RangedAttackRate = -1;
                    RangedDamage_min = -1;
                    RangedDamage_MAX = -1;
                    Armor = 0.3f;
                    break;
                case FighterEnum.MeleeLv4:
                    FighterType = FighterEnum.Melee;
                    Level = 4;
                    Hp = 200.0f;
                    AttackRate = 1.0f;
                    Damage_min = 10;
                    Damage_MAX = 14;
                    RangedAttackRate = -1;
                    RangedDamage_min = -1;
                    RangedDamage_MAX = -1;
                    Armor = 0.0f;
                    break;
                case FighterEnum.ReinforceLv0:
                    FighterType = FighterEnum.Reinforce;
                    Level = 0;
                    Hp = 30.0f;
                    AttackRate = 1.0f;
                    Damage_min = 1;
                    Damage_MAX = 2;
                    RangedAttackRate = -1;
                    RangedDamage_min = -1;
                    RangedDamage_MAX = -1;
                    Armor = 0.0f;
                    break;
                case FighterEnum.ReinforceLv1:
                    FighterType = FighterEnum.Reinforce;
                    Level = 1;
                    Hp = 50.0f;
                    AttackRate = 1.0f;
                    Damage_min = 1;
                    Damage_MAX = 3;
                    RangedAttackRate = -1;
                    RangedDamage_min = -1;
                    RangedDamage_MAX = -1;
                    Armor = 0.0f;
                    break;
                case FighterEnum.ReinforceLv2:
                    FighterType = FighterEnum.Reinforce;
                    Level = 2;
                    Hp = 70.0f;
                    AttackRate = 1.0f;
                    Damage_min = 2;
                    Damage_MAX = 4;
                    RangedAttackRate = -1;
                    RangedDamage_min = -1;
                    RangedDamage_MAX = -1;
                    Armor = 0.1f;
                    break;
                case FighterEnum.ReinforceLv3:
                    FighterType = FighterEnum.Reinforce;
                    Level = 3;
                    Hp = 90.0f;
                    AttackRate = 1.0f;
                    Damage_min = 3;
                    Damage_MAX = 6;
                    RangedAttackRate = -1;
                    RangedDamage_min = -1;
                    RangedDamage_MAX = -1;
                    Armor = 0.2f;
                    break;
                case FighterEnum.ReinforceLv4:
                    FighterType = FighterEnum.Reinforce;
                    Level = 4;
                    Hp = 110.0f;
                    AttackRate = 1.0f;
                    Damage_min = 6;
                    Damage_MAX = 10;
                    RangedAttackRate = -1;
                    RangedDamage_min = -1;
                    RangedDamage_MAX = -1;
                    Armor = 0.3f;
                    break;
                case FighterEnum.ReinforceLv5:
                    FighterType = FighterEnum.Reinforce;
                    Level = 5;
                    Hp = 110.0f;
                    AttackRate = 1.0f;
                    Damage_min = 6;
                    Damage_MAX = 10;
                    RangedAttackRate = 1.4f;
                    RangedDamage_min = 16;
                    RangedDamage_MAX = 30;
                    Armor = 0.3f;
                    break;
                case FighterEnum.Hero_Alric:
                    FighterType = FighterEnum.Hero;
                    Level = 10;
                    Hp = 560.0f;
                    AttackRate = 1.0f;
                    Damage_min = 27;
                    Damage_MAX = 40;
                    RangedAttackRate = -1.0f;
                    RangedDamage_min = -1;
                    RangedDamage_MAX = -1;
                    Armor = 0.65f;
                    break;
                case FighterEnum.Creature_Sandman:
                    FighterType = FighterEnum.Creature;
                    Level = 3;
                    Hp = 140.0f;
                    AttackRate = 1.0f;
                    Damage_min = 2;
                    Damage_MAX = 6;
                    RangedAttackRate = -1.0f;
                    RangedDamage_min = -1;
                    RangedDamage_MAX = -1;
                    Armor = 0.0f;
                    break;
                default:
                    UnityEngine.Debug.LogAssertion("Wrong FighterEnum");
                    break;
            }
        }

        public FighterEnum FighterType;
        public int Level;
        public float Hp;
        public float AttackRate;
        public int Damage_min;
        public int Damage_MAX;
        public float RangedAttackRate;
        public int RangedDamage_min;
        public int RangedDamage_MAX;
        public float Armor;
    }
}
