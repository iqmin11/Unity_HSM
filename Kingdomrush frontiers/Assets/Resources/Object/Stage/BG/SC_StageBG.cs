using Assets.Scenes.Object.Base.MyInterface;
using Assets.Scenes.Object.Stage.ContentsEnum;
using System.Collections.Generic;
using UnityEngine;

public class SC_StageBG : MonoBehaviour
{
    private void Awake()
    {
        BGRenderer = gameObject.AddComponent<SpriteRenderer>();
        BGRenderer.sortingOrder = (int)RenderOrder.Bg;

        InitStageBgSprite();
        InitStageBgDeco();
        InitSoundClips();
        InitSoundPlayer();
    }

    
    public void SetStageBG(int CurStage)
    {
        BGRenderer.sprite = BGSprite[CurStage];

        for(int i = 0; i < BgDeco.Count; i++)
        {
            if (BgDeco[i] == null)
            {
                continue;
            }

            if(i == CurStage)
            {
                BgDeco[CurStage].SetActive(true);
                continue;
            }

            BgDeco[i].SetActive(false);
        }
    }
    public void PlaySound(string Name)
    {
        StageBgmPlayer.clip = StageBgmClips[Name];
        StageBgmPlayer.Play();
    }

    private void InitStageBgSprite()
    {
        BGSprite.Add(Resources.Load<Sprite>("StageScene/StageBg/Stage_1"));
        BGSprite.Add(Resources.Load<Sprite>("StageScene/StageBg/Stage_2"));
        BGSprite.Add(Resources.Load<Sprite>("StageScene/StageBg/Stage_3"));
        BGSprite.Add(Resources.Load<Sprite>("StageScene/StageBg/Stage_4"));
        BGSprite.Add(Resources.Load<Sprite>("StageScene/StageBg/Stage_5"));
        BGSprite.Add(Resources.Load<Sprite>("StageScene/StageBg/Stage_6"));
    }
    private void InitStageBgDeco()
    {
        BgDeco.Add(new GameObject("Deco0"));
        BgDeco[0].AddComponent<SC_BgDeco0>();
        BgDeco[0].SetActive(false);
        BgDeco[0].transform.SetParent(transform);

        BgDeco.Add(null);

        BgDeco.Add(new GameObject("Deco2"));
        BgDeco[2].AddComponent<SC_BgDeco2>();
        BgDeco[2].SetActive(false);
        BgDeco[2].transform.SetParent(transform);

        BgDeco.Add(new GameObject("Deco3"));
        BgDeco[3].AddComponent<SC_BgDeco3>();
        BgDeco[3].SetActive(false);
        BgDeco[3].transform.SetParent(transform);

        BgDeco.Add(new GameObject("Deco4"));
        BgDeco[4].AddComponent<SC_BgDeco4>();
        BgDeco[4].SetActive(false);
        BgDeco[4].transform.SetParent(transform);

        BgDeco.Add(null);
    }
    private void InitSoundClips()
    {
        AudioClip temp = Resources.Load<AudioClip>("Sounds/PlayStage/BGM/savage_music_desert_preparation");
        StageBgmClips.Add("Preparation", temp);
        temp = Resources.Load<AudioClip>("Sounds/PlayStage/BGM/Desert_Battle");
        StageBgmClips.Add("Battle0", temp);
    }
    private void InitSoundPlayer()
    {
        StageBgmPlayer = gameObject.AddComponent<AudioSource>();
        StageBgmPlayer.loop = true;
    }

    private List<Sprite> BGSprite = new List<Sprite>();
    private List<GameObject> BgDeco = new List<GameObject>();
    private SpriteRenderer BGRenderer;

    private Dictionary<string, AudioClip> StageBgmClips = new Dictionary<string, AudioClip>();
    private AudioSource StageBgmPlayer;
}
