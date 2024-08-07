using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scenes.Object.Stage.ContentsEnum
{
    public enum MonsterEnum
    {
        Null = -1,
        DesertThug,
        DuneRaider,
        Immortal,
        SandHound,
        WarHound,
        DuneTerror,
        GiantWasp,
        GiantWaspQueen,
        GiantScorpion,
        DesertArcher,
        Executioner,
        SandWraith,
        Fallen,
    }

    public enum TowerEnum
    {
        Null = -1,
        RangedTower_CityTower,
        RangedTower,
        RangedTower_Level1,
        RangedTower_Level2,
        RangedTower_Level3,
        RangedTower_Level4,
        MeleeTower,
        MeleeTower_Level1,
        MeleeTower_Level2,
        MeleeTower_Level3,
        MeleeTower_Level4,
        MagicTower,
        MagicTower_Level1,
        MagicTower_Level2,
        MagicTower_Level3,
        MagicTower_Level4,
        ArtilleryTower,
        ArtilleryTower_Level1,
        ArtilleryTower_Level2,
        ArtilleryTower_Level3,
        ArtilleryTower_Level4,
    };

    public enum FighterEnum
    {
        Null = -1,
        Melee,
        MeleeLv1,
        MeleeLv2,
        MeleeLv3,
        MeleeLv4,
        Reinforce,
        ReinforceLv0,
        ReinforceLv1,
        ReinforceLv2,
        ReinforceLv3,
        ReinforceLv4,
        ReinforceLv5,
        Hero,
        Hero_Alric,
        Creature,
        Creature_Sandman
    };

    public enum RenderOrder
    {
        Bg = 0,
        InGameObject,
        InGameUI0,
        InGameUI1,
        InGameUI2,
        InGameUI3,
        InGameUI4,
        InGameUI5,
        InGameUI6,
        InGameUI7,
    };

}
