using System.Collections.Generic;

using UnityEngine;

using Assets.Scenes.Object.Stage.ContentsEnum;
using Assets.Scenes.Object.Stage.StageData;

public class SC_MeleeTower : SC_BaseTower
{
    protected override void Awake()
    {
        base.Awake();
        TowerSprite = MeleeTowerSpriteCache;
        TowerRenderer.sprite = TowerSprite[Data.Level - 1];

        MeleeRallyPointInst = Instantiate(MeleeRallyPointPrefab, transform);
        MeleeRallyPointSetting = MeleeRallyPointInst.GetComponent<SC_MeleeRallyPoint>();
        PlaySound("0");
    }

    protected override void SpriteCaching()
    {
        if (MeleeTowerSpriteCache.Count == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                MeleeTowerSpriteCache.Add(Resources.Load<Sprite>("StageScene/Tower/Melee/MeleeTower/tower_barracks_lvl" + (i + 1).ToString()));
            }
        }
    }
    protected override void InitData()
    {
        Data.SetData(TowerEnum.MeleeTower_Level1);
    }

    private void Start()
    {
        MeleeRallyPointInst.transform.position = DefaultRallyPos;
    }
    protected override void ChangeTower(TowerEnum TowerValue)
    {
        if (!TowerData.IsMeleeTower(TowerValue))
        {
            Debug.LogAssertion("Set Melee Tower : Input Wrong EnumValue");
            return;
        }

        Data.SetData(TowerValue);
        TowerRenderer.sprite = TowerSprite[Data.Level - 1];
        MeleeRallyPointSetting.ChangeTower(Data.Level);

        if(Data.Level < 4)
        {
            PlaySound(Random.Range(1,3).ToString());
        }
        else
        {
            PlaySound("3");
        }
    }

    [SerializeField]
    private GameObject MeleeRallyPointPrefab;
    private GameObject MeleeRallyPointInst = null;
    private SC_MeleeRallyPoint MeleeRallyPointSetting = null;
    private Vector4 defaultRallyPos = Vector4.zero;
    public Vector4 DefaultRallyPos
    {
        get
        {
            return defaultRallyPos;
        }

        set
        {
            defaultRallyPos = value;
        }
    }

    private static List<Sprite> MeleeTowerSpriteCache = new List<Sprite>();
    
    // Sound //////////////////////////////////////////////
    public override void InitSoundClips()
    {
        AddAudioClip("0", "Sounds/PlayStage/Tower/Melee/Barrack_Ready");
        AddAudioClip("1", "Sounds/PlayStage/Tower/Melee/Barrack_Taunt1");
        AddAudioClip("2", "Sounds/PlayStage/Tower/Melee/Barrack_Taunt2");
        AddAudioClip("3", "Sounds/PlayStage/Tower/Melee/assassin_taunt_ready");
    }
}
