using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Cinematic : MonoBehaviour
{
    [SerializeField] GameObject rawImage;
    [SerializeField] VideoPlayer player;
    [SerializeField] AudioManager music;

    private void Start()
    {
        player.loopPointReached += EndReached;
        if (!PlayerPrefs.HasKey("Cinematic") || PlayerPrefs.GetInt("Cinematic") == 0)
        {
            Show();
            PlayerPrefs.SetInt("Cinematic", 1);
        }
    }

    public void Show()
    {
        if (music != null)
            music.Pause(0);
        rawImage.SetActive(true);
        player.Play();
    }

    public void Stop()
    {
        if (music != null)
            music.UnPause(0);
        player.Stop();
        rawImage.SetActive(false);
        if (SceneManager.GetActiveScene().name == "CinematicScene")
        {
            PhotonNetwork.LoadLevel("LoadingScene");
        }
    }

    void EndReached(VideoPlayer vp)
    {
        Stop();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Return))
        {
            Stop();
        }
    }
}
