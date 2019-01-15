using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MainMenu : MonoBehaviourPunCallbacks {

    const string playerNamePrefKey = "PlayerName";

    bool clickedSolo = false;
    bool clickedMulti = false;

    string gameVersion = "1";

    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject multiplayerMenu;
    [SerializeField]
    private GameObject optionsMenu;
    [SerializeField]
    private GameObject connection;
    [SerializeField]
    private GameObject createGameMenu;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        mainMenu.SetActive(true);
        multiplayerMenu.SetActive(false);
        optionsMenu.SetActive(false);

        CheckPseudo();
    }

    void CheckPseudo()
    {
        if (!PlayerPrefs.HasKey(playerNamePrefKey))
        {
            PlayerPrefs.SetString(playerNamePrefKey, "Default Name");
        }
        PhotonNetwork.NickName = PlayerPrefs.GetString(playerNamePrefKey);

    }

    public void Singleplayer()
    {
        clickedSolo = true;
        if (PhotonNetwork.IsConnected)
            PhotonNetwork.Disconnect(); 
        else
        {
            PhotonNetwork.OfflineMode = true;
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (clickedSolo)
        {
            PhotonNetwork.OfflineMode = true;
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
        }
    }

    public void Multiplayer()
    {
        clickedMulti = true;
        Connect();
    }

    private void Connect()
    {
        mainMenu.SetActive(false);

        if (PhotonNetwork.IsConnected)
        {
            JoinOrCreateGameMenu();
        }
        else
        {
            connection.SetActive(true);
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    private void JoinOrCreateGameMenu()
    {
        connection.SetActive(false);
        mainMenu.SetActive(false);
        multiplayerMenu.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        if (clickedMulti)
            JoinOrCreateGameMenu();
    }

    public void OptionsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void CreateGame()
    {
        multiplayerMenu.SetActive(false);
        createGameMenu.SetActive(true);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("no rooms available");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room.");

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("We load the waiting room");
            PhotonNetwork.LoadLevel("WaitingRoom");
        }
    }
}