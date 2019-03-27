using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PlayersManager : MonoBehaviourPunCallbacks {

    readonly int soloMaxPlayer = 3;

    Hashtable customProp;
    [SerializeField]
    StartGameButton startButton;
    [SerializeField]
    Toggle readyToggle;
    [SerializeField]
    GameObject addBotButton;
    [SerializeField]
    Transform playersList;

    [SerializeField]
    List<Vector3> topLeft;
    [SerializeField]
    List<Vector3> bottomLeft;
    [SerializeField]
    List<Vector3> topRight;
    [SerializeField]
    List<Vector3> bottomRight;
    List<Vector3>[] coords;

    #region customProp

    string isReady = "IsReady";
    string checkIsReady = "CheckIsReady";

    #endregion

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
            coords = new List<Vector3>[4] { topLeft, bottomLeft , topRight , bottomRight };
        PhotonNetwork.Instantiate("UI/WaittingRoom/PlayerSettingsPrefab", Vector3.zero, Quaternion.identity);
        InitSettings();
    }

    void InitSettings()
    {
        customProp = new Hashtable();
        if (!PhotonNetwork.IsMasterClient)
        {
            readyToggle.gameObject.SetActive(true);
            startButton.gameObject.SetActive(false);
            addBotButton.SetActive(false);
            customProp.Add(isReady, false);
        }
        else
        {
            readyToggle.gameObject.SetActive(false);
            startButton.gameObject.SetActive(true);
            customProp.Add(isReady, true);
        }
        PhotonNetwork.LocalPlayer.SetCustomProperties(customProp);
        photonView.RPC(checkIsReady, RpcTarget.MasterClient);
    }

    public void AddBot()
    {
        PhotonNetwork.Instantiate("UI/WaittingRoom/BotSettingsPrefab", Vector3.zero, Quaternion.identity);
    }

    public void CheckAddBot()
    {
        if (PhotonNetwork.OfflineMode)
        {
            addBotButton.SetActive(playersList.childCount < soloMaxPlayer);
        }
        else
        {
            addBotButton.SetActive(playersList.childCount < PhotonNetwork.CurrentRoom.MaxPlayers);
        }
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
            CheckAddBot();
    }



    public void StartGame()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        SetCustomPropForAll();
        string mapName = PlayerPrefs.GetString("MapName");
        PhotonNetwork.LoadLevel(mapName);
    }

    void SetCustomPropForAll()
    {
        foreach (Transform playerSettings in playersList)
        {
            if (playerSettings.GetComponent<PlayerSettings>() != null)
            {
                PlayerSettings settings = playerSettings.GetComponent<PlayerSettings>();
                Hashtable table = new Hashtable();
                table.Add("Race", settings.raceDropdown.value);
                table.Add("Team", settings.teamDropdown.value);
                table.Add("Color", settings.colorDropdown.value);
                table.Add("MyCoords", SetCoords(settings.teamDropdown.value));
                playerSettings.GetComponent<PhotonView>().Owner.SetCustomProperties(table);
            }
        }
        int i = 1;
        Hashtable myTable = PhotonNetwork.LocalPlayer.CustomProperties;
        foreach (Transform playerSettings in playersList)
        {
            if (playerSettings.GetComponent<BotSettings>() != null)
            {
                BotSettings settings = playerSettings.GetComponent<BotSettings>();
                AddOrReplaceInt(myTable, "Race" + i, settings.raceDropdown.value);
                AddOrReplaceInt(myTable, "Team" + i, settings.teamDropdown.value);
                AddOrReplaceInt(myTable, "Color" + i, settings.colorDropdown.value);
                AddOrReplaceVect(myTable, "MyCoords" + i, SetCoords(settings.teamDropdown.value));
                i++;
            }
        }
        PlayerPrefs.SetInt("BotNumber", i - 1);
        PhotonNetwork.LocalPlayer.SetCustomProperties(myTable);
    }

    public void AddOrReplaceInt(Hashtable table, string key, int val)
    {
        if (table.ContainsKey(key))
            table[key] = val;
        else
            table.Add(key, val);
    }

    public void AddOrReplaceVect(Hashtable table, string key, Vector3 val)
    {
        if (table.ContainsKey(key))
            table[key] = val;
        else
            table.Add(key, val);
    }

    public Vector3 SetCoords(int team)
    {
        int tmp = Random.Range(0, coords[team].Count - 1);
        Vector3 res = coords[team][tmp];
        coords[team].RemoveAt(tmp);
        return res;
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
