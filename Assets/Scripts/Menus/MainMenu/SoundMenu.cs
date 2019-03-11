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
        generalSlider.value = PlayerPrefs.GetFloat("GeneralAudio");
        soundSlider.value = PlayerPrefs.GetFloat("SoundAudio");
        musicSlider.value = PlayerPrefs.GetFloat("MusicAudio");
        SetMasterVolume();
        SetSoundVolume();
        SetMusicVolume();
    }

    public void SetVolume(string exposeName, string prefName, float value)
    {
        audioMixer.SetFloat(exposeName, value);
        PlayerPrefs.SetFloat(prefName, value);
    }

    public void SetMasterVolume()
    {
        SetVolume("GeneralVolume", "GeneralAudio", generalSlider.value);
    }

    public void SetSoundVolume()
    {
        SetVolume("SoundVolume", "SoundAudio", soundSlider.value);
    }

    public void SetMusicVolume()
    {
        SetVolume("MusicVolume", "MusicAudio", musicSlider.value);
    }
}
