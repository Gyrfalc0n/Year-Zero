using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

public class InstanceManager : MonoBehaviourPunCallbacks {

    #region Singleton

    public static InstanceManager instanceManager;
    public bool offlineMode;
    public bool debugMode;

    public void Awake()
    {
        instanceManager = this;
        if (offlineMode)
            PhotonNetwork.OfflineMode = offlineMode;
    }

    #endregion

    protected int race;
    protected int team;
    protected int color;

    string botPrefab = "IA/BotPrefab";

    protected string[] townhalls = new string[2] { "Buildings/TownHall/TownHall", "Buildings/TownHall/TownHall" };
    protected string[] builders = new string[2] { "Units/Builder", "Units/Builder" };

    protected int botIndex;

    void Start()
    {
        bulletHolder = GameObject.Find("BulletsHolder").transform;
        botIndex = -1;
        InitStartingTroops(InitProp());
        if (PhotonNetwork.IsMasterClient)
            InitBots();
    }

    public float timer { get; private set; }
    private void Update()
    {
        timer += Time.deltaTime;
    }

    void InitBots()
    {
        int i = 1;
        Hashtable myTable = PhotonNetwork.LocalPlayer.CustomProperties;
        while (myTable.ContainsKey("Race" + i))
        {
            IAManager bot = PhotonNetwork.Instantiate(botPrefab, Vector3.zero, Quaternion.identity).GetComponent<IAManager>();
            bot.gameObject.name = "Bot" + i;
            bot.Init(i, (int)myTable["Race"+ i], (int)myTable["Team" + i], (int)myTable["Color" + i], (Vector3)myTable["MyCoords" + i]);
            i++;
        }
        if (debugMode)
        {
            IAManager bot = Instantiate((GameObject)Resources.Load(botPrefab)).GetComponent<IAManager>();
            bot.gameObject.name = "Bot0";
            bot.Init(0, 1, 1, 1, new Vector3 (10, 1, 10));
            i++;
        }
    }

    Vector3 InitProp()
    {
        Vector3 myCoords;
        if (!PhotonNetwork.OfflineMode && SceneManager.GetActiveScene().name != "Tutorial")
        {
            myCoords = (Vector3)PhotonNetwork.LocalPlayer.CustomProperties["MyCoords"];

            Hashtable customProp = PhotonNetwork.LocalPlayer.CustomProperties;
            race = (int)customProp["Race"];
            team = (int)customProp["Team"];
            color = (int)customProp["Color"];
        }
        else
        {
            myCoords = new Vector3(0, 0, 0);

            race = 0;
            team = 0;
            color = 0;
        }
        return myCoords;

    }

    void InitStartingTroops(Vector3 coords)
    {
        PlayerManager.playerManager.AddHome(InstantiateUnit(townhalls[race], new Vector3(coords.x + 2, 0.5f, coords.z + 2), Quaternion.Euler(0, 0, 0), -1).GetComponent<TownHall>());
        if (SceneManager.GetActiveScene().name != "Tutorial")
            InstantiateUnit(builders[race], coords, Quaternion.Euler(0, 0, 0), -1);
        if (SceneManager.GetActiveScene().name == "Mission")
        {
            int comp = 0;
            while(comp < 3)
            {
                InstantiateUnit(builders[race], coords, Quaternion.Euler(0, 0, 0), -1);
                comp++;
            }
        }
        Camera.main.GetComponent<CameraController>().LookTo(PlayerManager.playerManager.GetHomes()[0].transform.position);
    }

    protected void CheckDeath()
    {
        if (mySelectableObjs.Count == 0)
        {
            Debug.Log("You're Dead");
        }
    }

    public List<SelectableObj> allSelectableObjs = new List<SelectableObj>();
    public List<SelectableObj> mySelectableObjs = new List<SelectableObj>();
    public List<ResourceUnit> allResourceUnits = new List<ResourceUnit>();

    public virtual GameObject InstantiateUnit(string prefab, Vector3 pos, Quaternion rot, int botIndex)
    {
        if (botIndex == -1)
        {
            GameObject obj = PhotonNetwork.Instantiate(prefab, pos, rot);
            obj.GetComponent<SelectableObj>().InitUnit(-1);
            return obj;
        }
        else
        {
            return GetBot(botIndex).InstantiateUnit(prefab, pos, rot);
        }

    }

    #region Network

    public override void OnLeftRoom()
    {
        Debug.Log("Leave room");
        PhotonNetwork.LoadLevel("MainMenu");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        PhotonNetwork.LoadLevel("MainMenu");
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PhotonNetwork.Disconnect();
    }

    #endregion

    public int GetTeam()
    {
        return team;
    }

    public int GetRace()
    {
        return race;
    }

    public Color32 GetColor()
    {
        return Int2Color(color);
    }

    public Color32 GetPlayerColor(Player player)
    {
        if (player == PhotonNetwork.LocalPlayer)
        {
            return Int2Color(color);
        }
        return Int2Color((int)player.CustomProperties["Color"]);
    }

    public Color32 GetBotColor(int index)
    {
        return GameObject.Find("Bot" + index).GetComponent<IAManager>().GetColor();
    }

    public Color32 Int2Color(int val)
    {
        Color32 res;
        if (val == 0)
        {
            res = new Color32(255, 0, 0, 255);
        }
        else if (val == 1)
        {
            res = new Color32(0, 255, 0, 255);
        }
        else
        {
            res = new Color32(0, 0, 255, 255);
        }
        return res;
    }

    protected int colorLevel = 1;
    public void ChangeColorLevel()
    {
        if (++colorLevel > 2)
            colorLevel = 0;

        foreach (SelectableObj obj in allSelectableObjs)
        {
            obj.ToggleColor(colorLevel);
        }
    }

    public bool IsEnemy(SelectableObj unit)
    {
        if (unit.botIndex == -1)
        {
            if (PhotonNetwork.OfflineMode)
            {
                return false;
            }
            else
            {
                return (int)unit.photonView.Owner.CustomProperties["Team"] != team;
            }
        }
        else if (unit.botIndex != -2)
        {
            return (GetTeam() != GetBot(unit.botIndex).GetTeam());
        }
        else
        {
            return true;
        }
    }

    public void AllSelectableRemoveAt(int i)
    {
        if (PhotonNetwork.OfflineMode)
            return;
        photonView.RPC("RPCAllSelectableRemoveAt", RpcTarget.Others, i);
    }

    [PunRPC]
    public void RPCAllSelectableRemoveAt(int i)
    {
        allSelectableObjs.RemoveAt(i);
    }

    public IAManager GetBot(int index)
    {
        return GameObject.Find("Bot" + index).GetComponent<IAManager>();
    }

    public void AllResourceUnitsRemoveAt(int i)
    {
        if (PhotonNetwork.OfflineMode)
            return;
        photonView.RPC("RPCAllResourceUnitsRemoveAt", RpcTarget.Others, i);
    }

    [PunRPC]
    public void RPCAllResourceUnitsRemoveAt(int i)
    {
        allResourceUnits.RemoveAt(i);
    }

    public Transform bulletHolder { get; private set; }
}
