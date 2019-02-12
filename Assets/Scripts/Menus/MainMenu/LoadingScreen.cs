using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    GameObject obj;
    [SerializeField]
    Slider progressionSlider;

    void Update()
    {
        obj.SetActive(PhotonNetwork.LevelLoadingProgress > 0 && PhotonNetwork.LevelLoadingProgress < 1);
        progressionSlider.value = PhotonNetwork.LevelLoadingProgress;
    }
}
