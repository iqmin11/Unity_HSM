using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class SC_SoundManager : MonoBehaviour
{
    private void Awake()
    {
        SoundPlayer = gameObject.AddComponent<AudioSource>();
        SoundPlayer.volume = 0.5f;
    }

    public void AddSoundClip(string Name, string Path)
    {
        SoundClips.Add(Name, Resources.Load<AudioClip>(Path));
    }

    public void PlaySound(string Name)
    {
        if (SoundPlayer.isPlaying)
        {
            return;
        }

        if (!SoundClips.ContainsKey(Name))
        {
            Debug.LogAssertion("No Contain this Key - " + Name);
            return;
        }

        SoundPlayer.clip = SoundClips[Name];
        SoundPlayer.Play();
    }

    public int RefCount
    {
        get
        {
            return refCount;
        }

        set
        {
            refCount = value;
        }
    }
    public int ClipCount
    {
        get
        {
            return SoundClips.Count;
        }
    }
    
    private int refCount = 0;
    private Dictionary<string, AudioClip> SoundClips = new Dictionary<string, AudioClip>();
    private AudioSource SoundPlayer = null;

    // Sound /////////////////////////////////////////////

    //static private GameObject SoundManagerInst;
    //static private SC_SoundManager SoundManagerSetting;

    //private void SoundManager_AwakeParentInst()
    //{
    //    InitSoundManager();
    //    InitSoundClips();
    //    ++SoundManagerSetting.RefCount;
    //}

    //private void InitSoundManager()
    //{
    //    if (SoundManagerInst != null)
    //    {
    //        return;
    //    }

    //    SoundManagerInst = new GameObject("RangedBullet_SoundManager");
    //    SoundManagerSetting = SoundManagerInst.AddComponent<SC_SoundManager>();
    //}

    //private void InitSoundClips()
    //{
    //    if (SoundManagerSetting.ClipCount > 0)
    //    {
    //        return;
    //    }

    //    SoundManagerSetting.AddSoundClip("0", "Sounds/PlayStage/Tower/Ranged/Sound_ArrowRelease2");
    //    SoundManagerSetting.AddSoundClip("1", "Sounds/PlayStage/Tower/Ranged/Sound_ArrowRelease3");
    //}

    //private void SoundManager_OnDestroyParentInst()
    //{
    //    if (--SoundManagerSetting.RefCount == 0)
    //    {
    //        Destroy(SoundManagerInst);
    //        SoundManagerSetting = null;
    //        SoundManagerInst = null; ;
    //    }
    //}
}
