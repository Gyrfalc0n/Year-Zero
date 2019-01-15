using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameOptionMenu : MonoBehaviour {

    [SerializeField]
    private GameObject video;
    [SerializeField]
    private GameObject sound;

    public void VideoMenu()
    {
        video.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SoundMenu()
    {
        sound.SetActive(true);
        gameObject.SetActive(false);
    }
}
