using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameMenu : MonoBehaviour {

    [SerializeField]
    private GameObject optionsMenu;

    public void OptionsMenu()
    {
        optionsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        if (!PhotonNetwork.IsConnected)
            PhotonNetwork.LoadLevel("MainMenu");
        else
            PhotonNetwork.LeaveRoom();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
