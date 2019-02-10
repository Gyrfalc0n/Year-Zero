using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameMenu : MonoBehaviour {

    [SerializeField]
    GameObject menu;
    [SerializeField]
    private GameObject optionsMenu;
    [SerializeField]
    GameObject gameplayMenu;
    [SerializeField]
    GameObject videoMenu;
    [SerializeField]
    GameObject soundMenu;

    void OnEnable()
    {
        gameplayMenu.SetActive(false);
        videoMenu.SetActive(false);
        soundMenu.SetActive(false);
        optionsMenu.SetActive(false);
        menu.SetActive(true);
    }

    public void OptionsMenu()
    {
        optionsMenu.SetActive(true);

        menu.SetActive(false);
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
