using UnityEngine;

namespace Src.Audio
{
    public class AudioPlayer : IAudioPlayer
    {
        private readonly Camera camera;
        private readonly AudioSource audioSource;

        public AudioPlayer(Camera camera)
        {
            this.camera = camera;
            audioSource = this.camera.GetComponent<AudioSource>();
        }

        public void Play(AudioClip clip)
        {
            Play(clip, 1f);
        }

        public void Play(AudioClip clip, float volume)
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }
}