using UnityEngine;
using UnityEngine.Audio;

namespace _Workspace.Jordan.Script.AudioListener
{
    public class SoundMixerManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;

        public void SetMasterVolume(float level)
        {
            _audioMixer.SetFloat("MasterVolume", Mathf.Log10(level) * 20f);
        }

        public void SetSFXSoundVolume(float level)
        {
            _audioMixer.SetFloat("SFXVolume", Mathf.Log10(level) * 20f);
        }

        public void SetMusicVolume(float level)
        {
            _audioMixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20f);
        }
    
        public void SetAmbianceVolume(float level)
        {
            _audioMixer.SetFloat("AmbianceVolume", Mathf.Log10(level) * 20f);
        }
    }
}
