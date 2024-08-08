using Assets.Scenes.Object.Base.MyInterface;
using Assets.Scenes.Object.Stage.ContentsEnum;
using Assets.Scenes.Object.Stage.StageData;
using System.Collections.Generic;
using UnityEngine;

public abstract class SC_BaseTower : MonoBehaviour, ISoundPlayer
{
    protected virtual void Awake()
    {
        SpriteCaching();

        TowerRenderer = gameObject.AddComponent<SpriteRenderer>();
        TowerRenderer.sortingOrder = (int)RenderOrder.InGameObject0;

        InitData();
        
        if (ButtonPrefab == null)
        {
            ButtonPrefab = Resources.Load<GameObject>("Object/Stage/Tower/Base/UI/PF_TowerButton");
        }
        ButtonInst = Instantiate(ButtonPrefab, transform);
        ButtonInst.transform.localPosition = new Vector3(0, 0, 1);
        ButtonSetting = ButtonInst.GetComponent<SC_TowerButton>();

        TowerUiInst = Instantiate(TowerUiPrefab, transform);

        ButtonSetting.Click = () =>
        {
            TowerUiInst.SetActive(true);
        };

        TowerUiSetting = TowerUiInst.GetComponent<SC_UpgradeUI>();
        TowerUiSetting.SettingButtonCallback(UpgradeUiEnum.Upgrade, () =>
        {
            ChangeTower(Data.TowerType + Data.Level + 1);
            TowerUiInst.SetActive(false);
        });

        TowerUiSetting.SettingButtonCallback(UpgradeUiEnum.Sell, () =>
        {
            SellTower();
        });

        InitSoundPlayer();
    }

    protected abstract void SpriteCaching();
    protected abstract void InitData();
    protected abstract void ChangeTower(TowerEnum TowerValue);

    private void SellTower()
    {
        transform.parent.GetComponent<SC_BuildArea>().OnBuildAreaButton();
        Destroy(gameObject);
    }

    protected TowerData Data = new TowerData();
    protected List<Sprite> TowerSprite = null;
    protected SpriteRenderer TowerRenderer;

    private static GameObject ButtonPrefab = null;
    private GameObject ButtonInst;
    private SC_TowerButton ButtonSetting;

    [SerializeField]
    private GameObject TowerUiPrefab;
    private GameObject TowerUiInst;
    private SC_BaseTowerUI TowerUiSetting;

    // Sound /////////////////////////////////////////////
    protected AudioSource SoundPlayer;
    protected Dictionary<string, AudioClip> SoundClips = new Dictionary<string, AudioClip>();

    public virtual void InitSoundPlayer()
    {
        SoundPlayer = gameObject.AddComponent<AudioSource>();
    }

    public virtual void AddAudioClip(string Name, string Path)
    {
        SoundClips.Add(Name, Resources.Load<AudioClip>(Path));
    }

    public virtual void PlaySound(string Name)
    {
        if(SoundPlayer.isPlaying)
        {
            return;
        }

        SoundPlayer.clip = SoundClips[Name];
        SoundPlayer.Play();
    }
}
