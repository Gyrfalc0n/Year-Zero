using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PlayersManager : MonoBehaviourPunCallbacks {

    readonly int soloMaxPlayer = 4;

    Hashtable customProp;
    [SerializeField]
    Button startButton;
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

    [SerializeField]
    TemporaryMenuMessage notReady;
    [SerializeField]
    TemporaryMenuMessage lobbyFull;
    [SerializeField]
    Text playerAmountText;
    [SerializeField]
    GameObject waittingMessage;
    [SerializeField]
    Text waittingDots;
    float timer;

    PlayerSettings playerSettings;

    #region customProp

    string isReady = "IsReady";

    #endregion

    void Start()
    {
        timer = 0.3f;
        if (PhotonNetwork.IsMasterClient)
            coords = new List<Vector3>[4] { topLeft, bottomLeft , topRight , bottomRight };
        playerSettings = PhotonNetwork.Instantiate("UI/WaitingRoom/PlayerSettingsPrefab", Vector3.zero, Quaternion.identity).GetComponent<PlayerSettings>();
        InitSettings();
        UpdatePlayerAmountText();
    }

    void InitSettings()
    {
        customProp = new Hashtable();
        readyToggle.gameObject.SetActive(!PhotonNetwork.IsMasterClient);
        startButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
        addBotButton.SetActive(PhotonNetwork.IsMasterClient);
        customProp.Add(isReady, PhotonNetwork.IsMasterClient);
        PhotonNetwork.LocalPlayer.SetCustomProperties(customProp);
    }

    public void TryAddBot()
    {
        int maxPlayer = (PhotonNetwork.OfflineMode) ? soloMaxPlayer : PhotonNetwork.CurrentRoom.MaxPlayers;
        if (playersList.childCount < maxPlayer)
        {
            AddBot();
        }
        else
        {
            lobbyFull.Activate();
        }
    }

    void AddBot()
    {
        PhotonNetwork.Instantiate("UI/WaitingRoom/BotSettingsPrefab", Vector3.zero, Quaternion.identity);
    }

    void Update()
    {
        UpdatePlayerAmountText();
        UpdateWaitingMessage();
        if (PhotonNetwork.IsMasterClient && Input.GetKeyUp(KeyCode.Return))
        {
            TryStartGame();
        }
    }

    public void TryStartGame()
    {
        if (CheckIsReady())
        {
            StartGame();
        }
        else
        {
            notReady.Activate();
        }
    }

    void StartGame()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        SetCustomPropForAll();
        string mapName = PlayerPrefs.GetString("MapName");
        PhotonNetwork.LoadLevel(mapName);
        FindObjectOfType<AudioManager>().PlayRandomSound(new []{"UniverseMusic","09. Genesis","06. Spatial Lullaby"} );
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
                AddOrReplace(myTable, "Race" + i, settings.raceDropdown.value);
                AddOrReplace(myTable, "Team" + i, settings.teamDropdown.value);
                AddOrReplace(myTable, "Color" + i, settings.colorDropdown.value);
                AddOrReplace(myTable, "MyCoords" + i, SetCoords(settings.teamDropdown.value));
                i++;
            }
        }
        PlayerPrefs.SetInt("BotNumber", i - 1);
        PhotonNetwork.LocalPlayer.SetCustomProperties(myTable);
    }

    void AddOrReplace<T>(Hashtable table, string key, T val)
    {
        if (table.ContainsKey(key))
            table[key] = val;
        else
            table.Add(key, val);
    }

    Vector3 SetCoords(int team)
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

        if (val)
            playerSettings.Lock();
        else
            playerSettings.Unlock();
    }

    bool CheckIsReady()
    {
        bool allReady = true;
        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; allReady && i < players.Length; i++)
        {
            if ((bool)players[i].CustomProperties[isReady] == false)
                allReady = false;
        }
        return allReady;
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Leave room");
        PhotonNetwork.LoadLevel("MainMenu");
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("Master Client switched to" + newMasterClient.NickName);
        PhotonNetwork.LeaveRoom();
    }

    void UpdatePlayerAmountText()
    {
        if (playersList == null || !PhotonNetwork.InRoom) return; 
        playerAmountText.text = playersList.childCount + "/" + ((PhotonNetwork.OfflineMode) ? soloMaxPlayer : PhotonNetwork.CurrentRoom.MaxPlayers);
    }

    [SerializeField] Canvas cnvs;
    void UpdateWaitingMessage()
    {
        if (playersList == null || !PhotonNetwork.InRoom) return;
        waittingMessage.SetActive(!PhotonNetwork.OfflineMode && playersList.childCount < PhotonNetwork.CurrentRoom.MaxPlayers);
        if (PhotonNetwork.OfflineMode) return;
        UpdateDots();
        UpdateWaitingMessagePos();
    }

    void UpdateDots()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0.3f;
            waittingDots.text = (waittingDots.text.Length == 5) ? "" : new string('.', (waittingDots.text.Length + 1));
        }
    }

    void UpdateWaitingMessagePos()
    {
        Vector3 tmp = waittingMessage.transform.position;
        if (playersList.childCount > 0)
            tmp.y = playersList.GetChild(playersList.childCount - 1).position.y - 40 * cnvs.scaleFactor;
        waittingMessage.transform.position = tmp;
    }
}
