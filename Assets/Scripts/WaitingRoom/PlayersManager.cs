using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PlayersManager : MonoBehaviourPunCallbacks {

    Hashtable customProp;
    [SerializeField]
    StartGameButton startButton;
    [SerializeField]
    Toggle readyToggle;

    #region customProp

    string isReady = "IsReady";
    string checkIsReady = "CheckIsReady";
    string race = "Race";
    string team = "Team";
    string color = "Color";
    string place = "Place";

    #endregion

    [SerializeField]
    List<PlayerSettings> playerSettings;

    [SerializeField]
    List<Vector3> coords = new List<Vector3>();

    void Start()
    {
        customProp = new Hashtable();
        if (!PhotonNetwork.IsMasterClient)
        {
            readyToggle.gameObject.SetActive(true);
            startButton.gameObject.SetActive(false);
            customProp.Add(isReady, false);
        }
        else
        {
            readyToggle.gameObject.SetActive(false);
            startButton.gameObject.SetActive(true);
            customProp.Add(isReady, true);
        }
        InitSettings();
        photonView.RPC(checkIsReady, RpcTarget.MasterClient);
    }

    void InitSettings()
    {
        int playerPlace = GetFirstAvailablePlace();
        customProp.Add(race, 0);
        customProp.Add(team, 0);
        customProp.Add(color, 0);
        customProp.Add(place, playerPlace);
        playerSettings[playerPlace].InitPanel();
        PhotonNetwork.LocalPlayer.SetCustomProperties(customProp);
    }

    public override void OnPlayerPropertiesUpdate(Player target, Hashtable changedProps)
    {
        UpdateSettings();
    }

    void UpdateSettings()
    { 
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            playerSettings[(int)player.CustomProperties[place]].UpdatePanel(player);
        }
    }

    int GetFirstAvailablePlace()
    {
        /*List<int> remainingPlace = new List<int>() { 0, 1, 2, 3 };
        foreach (Player player in PhotonNetwork.PlayerListOthers)
        {
            int tmp = (int)player.CustomProperties[place];
            remainingPlace.Remove(tmp);
        }
        return remainingPlace[0];*/

        if (PhotonNetwork.IsMasterClient)
        {
            return 0;
        }

        for (int i = 1; i < PhotonNetwork.CurrentRoom.MaxPlayers; i++)
        {
            if (!playerSettings[0].photonView.IsOwnerActive || playerSettings[0].photonView.Owner == PhotonNetwork.MasterClient)
            {
                return i;
            }
        }
        return -1;
    }















    public void StartGame()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            int tmp = Random.Range(0, coords.Count - 1);
            photonView.RPC("SetCoords", player, coords[tmp]);
            coords.RemoveAt(tmp);
        }
        string mapName = PlayerPrefs.GetString("MapName");
        PhotonNetwork.LoadLevel(mapName);
    }

    [PunRPC]
    public void SetCoords(Vector3 baseCoords)
    {
        customProp.Add("MyCoords", baseCoords);
        PhotonNetwork.LocalPlayer.SetCustomProperties(customProp);
    }

    public void ToggleReady(bool val)
    {
        customProp[isReady] = val;
        PhotonNetwork.LocalPlayer.SetCustomProperties(customProp);
        photonView.RPC(checkIsReady, RpcTarget.MasterClient);
    }

    [PunRPC]
    public void CheckIsReady()
    {
        bool allReady = true;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if ((bool)player.CustomProperties[isReady] == false)
                allReady = false;
        }

        if (allReady)
            startButton.Activate();
        else
            startButton.Deactivate();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("MainMenu");
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        PhotonNetwork.LeaveRoom();
    }
}
