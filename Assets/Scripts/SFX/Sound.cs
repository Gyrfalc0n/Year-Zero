using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Sound
{

    public string name;
    public AudioClip clip;

    [Range(0f,2f)]
    public float volume;
    [Range(0.1f,3f)]
    public float pitch;

    public bool isMusic;

    [HideInInspector]
    public bool isPlaying;

    [HideInInspector] 
    public AudioSource source;

}
