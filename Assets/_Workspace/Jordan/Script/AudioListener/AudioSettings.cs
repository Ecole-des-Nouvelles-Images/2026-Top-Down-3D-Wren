using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace _Workspace.Jordan.Script.AudioListener
{
    public class AudioSettings : MonoBehaviour
    {

        [SerializeField] private Slider _sliderGeneral;
        [SerializeField] private Slider _sliderMusic;
        [SerializeField] private Slider _sliderSfx;
        [SerializeField] private AudioMixer _audioMixer;

        private void Awake()
        {
            _sliderGeneral.onValueChanged.AddListener(SetMasterVolume);
            _sliderMusic.onValueChanged.AddListener(SetMusicVolume);
            _sliderSfx.onValueChanged.AddListener(SetSFXVolume);

            _audioMixer.GetFloat("MasterVolume" ,out float masterVolume);
            _sliderGeneral.SetValueWithoutNotify(Mathf.Pow(10, masterVolume/20));
            
            _audioMixer.GetFloat("MusicVolume" ,out float musicVolume);
            _sliderMusic.SetValueWithoutNotify(Mathf.Pow(10, musicVolume/20));
            
            _audioMixer.GetFloat("SFXVolume" ,out float sfxVolume);
            _sliderSfx.SetValueWithoutNotify(Mathf.Pow(10, sfxVolume/20));
        }
        
        public void SetMasterVolume(float value)
        {
            _audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20f);
        }

        public void SetMusicVolume(float value)
        {
            _audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20f);
        }

        public void SetSFXVolume(float value)
        {
            _audioMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20f);
        }

    }
}
