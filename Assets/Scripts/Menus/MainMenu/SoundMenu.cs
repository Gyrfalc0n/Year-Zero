using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMenu : MonoBehaviour {

    [SerializeField]
    Slider generalSlider;
    [SerializeField]
    Slider soundSlider;
    [SerializeField]
    Slider musicSlider;

    public AudioMixer audioMixer;

    void OnEnable()
    {
        SetMasterVolume(PlayerPrefs.GetFloat("GeneralAudio"));
        SetSoundVolume(PlayerPrefs.GetFloat("SoundAudio"));
        SetMusicVolume(PlayerPrefs.GetFloat("MusicAudio"));
    }

    public void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("GeneralVolume", value);
        PlayerPrefs.SetFloat("GeneralAudio", value);
    }

    public void SetSoundVolume(float value)
    {
        audioMixer.SetFloat("SoundVolume", value);
        PlayerPrefs.SetFloat("SoundAudio", value);
    }

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("MusicVolume", value);
        PlayerPrefs.SetFloat("MusicAudio", value);
    }
}
