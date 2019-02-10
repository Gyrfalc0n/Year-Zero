using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FirstLoading : MonoBehaviour
{
    void Start()
    {
        PhotonNetwork.LoadLevel("MainMenu");
    }
}
