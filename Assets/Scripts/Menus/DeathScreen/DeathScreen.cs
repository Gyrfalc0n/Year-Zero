using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField]
    GameObject obj;
    [SerializeField]
    GameObject retryButton;

    public void Show()
    {
        Time.timeScale = 0;
        obj.SetActive(true);
        retryButton.SetActive(!PhotonNetwork.InRoom);
    }

    public void TryAgain()
    {
        Time.timeScale = 1;
        PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex);
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
