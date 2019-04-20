using System;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioMixerGroup Sound;
    [SerializeField]public AudioMixerGroup Music;

    public Sound[] sounds;

    public static AudioManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            //crée les composants dans le audio manager pour avoir les sons à disposition grâce aux attributs de la classe Sound( à edit à la main dans l'audio manager)
            foreach (Sound sound in sounds) 
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.isMusic;
                sound.isPlaying = false;
                if (sound.isMusic)
                {
                    sound.source.outputAudioMixerGroup = Music;
                }
                else
                {
                    sound.source.outputAudioMixerGroup = Sound;
                }
            }
        }
        
    }

    private void Start() //détermine music en fonction de la scène
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "MainMenu" || sceneName == "WaitingRoom")
        {
            PlaySoundStart("MainMenuMusic");
            
        }
        else
        {
            PlayRandomSoundStart(new []{"UniverseMusic","09. Genesis","06. Spatial Lullaby"} );
        }
        
    }

    public void PlaySound(string name) //lance l'audio clip de sounds avec le nom "name"
    {
        if (name == "NameNotMissing")
        {
            return;
        }
        Sound soundToPlay = Array.Find(sounds, soundSearch => soundSearch.name == name);
        if (soundToPlay == null)
        {
            Debug.Log("typo in to play sound: " +name);
            return;
        }

        if (soundToPlay.isMusic)
        {
            Sound soundCurrentlyPlaying = Array.Find(sounds, soundSearch => soundSearch.name == GetActiveMusic());
            if ( soundCurrentlyPlaying == null)
            {
                Debug.Log("no active music or typo in current sound: " + GetActiveMusic());
            }
            else
            {
                soundCurrentlyPlaying.source.Stop();
            }
        }
        soundToPlay.source.Play();
        soundToPlay.isPlaying = true;
    }
    
    public void PlaySoundStart(string name) //lance l'audio clip de sounds avec le nom "name"
    {
        Sound soundToPlay = Array.Find(sounds, soundSearch => soundSearch.name == name);
        if (soundToPlay == null)
        {
            Debug.Log("typo in to play sound: " +name);
            return;
        }
        soundToPlay.source.Play();
        soundToPlay.isPlaying = true;
    }

    public void PlayRandomSound(string[] sounds)
    {
        Random dice = new Random();
        name = sounds[dice.Next(0, sounds.Length)];
        PlaySound(name);
    }
    
    public void PlayRandomSoundStart(string[] sounds)
    {
        Random dice = new Random();
        name = sounds[dice.Next(0, sounds.Length)];
        PlaySoundStart(name);
    }

    private string GetActiveMusic()
    {
        string activeMusicName = "NameMissing";
        foreach (Sound sound in sounds)
        {
            if (sound.isPlaying && sound.isMusic)
            {
                activeMusicName = sound.name;
            }
        }

        return activeMusicName;
    }

    
}
