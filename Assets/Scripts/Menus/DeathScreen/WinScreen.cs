using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    [SerializeField]
    GameObject obj;

    public void Show()
    {
        Time.timeScale = 0;
        obj.SetActive(true);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        if (PhotonNetwork.InRoom)
            PhotonNetwork.LeaveRoom();
        else
            PhotonNetwork.LoadLevel("MainMenu");
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}
