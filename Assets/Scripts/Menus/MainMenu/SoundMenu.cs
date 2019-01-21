using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMenu : MonoBehaviour {

    [SerializeField]
    private Slider generalSlider;

    public AudioMixer audioMixer;

    private void Awake()
    {
        float value;
        audioMixer.GetFloat("GeneralVolume", out value);
        generalSlider.value = value;
    }

    public void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("GeneralVolume", value);
    }
}
