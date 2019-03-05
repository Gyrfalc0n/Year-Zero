using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class KeepMusicThroughScenes : MonoBehaviour
{
    AudioSource source;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic()
    {
        if (!IsPlaying())
        {
            source.Play();
        }
    }

    public void StopMusic()
    {
        source.Stop();
    }

    public bool IsPlaying()
    {
        return source.isPlaying;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 &&
            SceneManager.GetActiveScene().buildIndex != 1 &&
            SceneManager.GetActiveScene().buildIndex != 2)
        {
            Destroy(gameObject);
        }
    }
}
