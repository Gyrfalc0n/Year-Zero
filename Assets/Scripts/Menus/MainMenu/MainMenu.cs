using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MainMenu : MonoBehaviourPunCallbacks {

    const string playerNamePrefKey = "PlayerName";

    string gameVersion = "1";

    bool clickedMulti = false;

    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    GameObject singleplayerMenu;
    [SerializeField]
    private GameObject multiplayerMenu;
    [SerializeField]
    private GameObject optionsMenu;
    [SerializeField]
    private GameObject connection;
    [SerializeField]
    private GameObject createGameMenu;

    void Awake()
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
        if (PhotonNetwork.IsConnected)
            PhotonNetwork.Disconnect();
        else
        {
            GotoSingleplayerMenu();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        GotoSingleplayerMenu();
    }

    void GotoSingleplayerMenu()
    {
        PhotonNetwork.OfflineMode = true;
        mainMenu.SetActive(false);
        singleplayerMenu.SetActive(true);
    }

    public void Multiplayer()
    {
        clickedMulti = true;
        Connect();
    }

    private void Connect()
    {
        if (PhotonNetwork.OfflineMode)
            PhotonNetwork.OfflineMode = false;
        mainMenu.SetActive(false);

        if (PhotonNetwork.IsConnected && !PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
        else if (PhotonNetwork.IsConnected && PhotonNetwork.InLobby)
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
        if (clickedMulti && !PhotonNetwork.OfflineMode)
            PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        if (clickedMulti && !PhotonNetwork.OfflineMode)
        {
            JoinOrCreateGameMenu();
            clickedMulti = false;
        }
            
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

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("We load the waiting room");
            PhotonNetwork.LoadLevel("WaitingRoom");
        }
    }
}