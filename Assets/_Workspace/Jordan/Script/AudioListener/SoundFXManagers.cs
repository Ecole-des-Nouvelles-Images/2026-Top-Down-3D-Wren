using System;
using UnityEngine;

namespace _Workspace.Jordan.Script.AudioListener
{
    [Serializable]
    public enum SoundGroups
    {
        Music,
        Sfx,
        Ambiance
    }
    public class SoundFXManager : MonoBehaviourSingleton<SoundFXManager>
    {
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private AudioSource _sfxAudioSource;
        [SerializeField] private AudioSource _ambianceAudioSource;
        
        private void Awake()
        { 
            _sfxAudioSource = GetComponent<AudioSource>();
        }
        
        public void PlaySoundFXClip(AudioClip audioClip, SoundGroups soundGroup)
        { 
            AudioSource audioSource = GetAudioSource(soundGroup); 
            audioSource.PlayOneShot(audioClip);
        }

        public void SetSoundGroupVolume(SoundGroups soundGroup, float volume)
        { 
            AudioSource audioSource = GetAudioSource(soundGroup);
            audioSource.volume = volume;
        }

        private AudioSource GetAudioSource(SoundGroups soundGroup)
        {
            return soundGroup switch
            {
                SoundGroups.Music => _musicAudioSource,
                SoundGroups.Sfx => _sfxAudioSource,
                SoundGroups.Ambiance => _ambianceAudioSource,
                _ => throw new ArgumentOutOfRangeException(nameof(soundGroup), soundGroup, null)
            };
        }
    }
}
