
using UnityEngine;

namespace Assets.Scenes.Object.Base.MyInterface
{
    internal interface IEffectPlayer
    {
        void PlayEffect(); // Setting Play Logic
    }

    internal interface ISoundPlayer
    {
        public void InitSoundPlayer();
        public void InitSoundClips();
        public void AddAudioClip(string Name, string Path);
        public void PlaySound(string Name);
    }
    internal interface ISoundManager
    {
        public void SoundManager_AwakeParentInst();
        public void InitSoundManager();
        public void InitSoundClips();
        public void SoundManager_OnDestroyParentInst();
    }
}
