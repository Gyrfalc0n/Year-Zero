using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class FirstLoading : MonoBehaviour
{
    void Start()
    {
        if (!PlayerPrefs.HasKey("Cinematic") || PlayerPrefs.GetInt("Cinematic") == 0)
        {
            PhotonNetwork.LoadLevel("CinematicScene");
        }
        else
        {
            PhotonNetwork.LoadLevel("MainMenu");
        }
    }
}
