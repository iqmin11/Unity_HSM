using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class SC_SoundManager : MonoBehaviour
{
    private void Awake()
    {
        SoundPlayer = gameObject.AddComponent<AudioSource>();
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
}
