using UnityEngine;

namespace Src.Audio
{
    public interface IAudioPlayer
    {
        void Play(AudioClip clip);
        
        void Play(AudioClip clip, float volume);
    }
}