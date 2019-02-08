using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class InstanceManager : MonoBehaviourPunCallbacks {

    #region Singleton

    public static InstanceManager instanceManager;
    public bool offlineMode;

    void Awake()
    {
        instanceManager = this;
        if (offlineMode)
            PhotonNetwork.OfflineMode = offlineMode;
    }

    #endregion

    int race;
    int team;
    int color;

    private void Start()
    {
        Vector3 myCoords;
        if (!PhotonNetwork.OfflineMode)
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
        PlayerManager.playerManager.AddHome(InstantiateUnit("Buildings/TownHall/TownHall", new Vector3(myCoords.x+2, 0.5f, myCoords.z+2), Quaternion.Euler(0, 0, 0)).GetComponent<TownHall>());
        InstantiateUnit("Units/BuilderUnit", myCoords, Quaternion.Euler(0, 0, 0));
        InstantiateUnit("Units/BuilderUnit", myCoords, Quaternion.Euler(0, 0, 0));
        InstantiateUnit("Units/BuilderUnit", myCoords, Quaternion.Euler(0, 0, 0));
        InstantiateUnit("Units/BuilderUnit", myCoords, Quaternion.Euler(0, 0, 0));
        InstantiateUnit("Units/BuilderUnit", myCoords, Quaternion.Euler(0, 0, 0));
        InstantiateUnit("Units/BuilderUnit", myCoords, Quaternion.Euler(0, 0, 0));
        InstantiateUnit("Units/BuilderUnit", myCoords, Quaternion.Euler(0, 0, 0));
        InstantiateUnit("Units/BuilderUnit", myCoords, Quaternion.Euler(0, 0, 0));
        //InstantiateUnit("Units/MovableUnit", new Vector3 (5, 0, 0), Quaternion.Euler(0, 0, 0));
        //InstantiateUnit("Units/CombatUnit", new Vector3(-5, 0, 0), Quaternion.Euler(0, 0, 0));
    }

    private void CheckDeath()
    {
        if (mySelectableObjs.Count == 0)
        {
            Debug.Log("You're Dead");
        }
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("MainMenu");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        PhotonNetwork.LoadLevel("MainMenu");
    }

    public List<SelectableObj> allSelectableObjs = new List<SelectableObj>();
    public List<SelectableObj> mySelectableObjs = new List<SelectableObj>();

    public GameObject InstantiateUnit(string prefab, Vector3 pos, Quaternion rot)
    {
        GameObject obj = PhotonNetwork.Instantiate(prefab, pos, rot);
        return obj;
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        PhotonNetwork.Disconnect();
    }

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

    int colorLevel = 1;
    public void ChangeColorLevel()
    {
        if (++colorLevel > 2)
            colorLevel = 0;

        foreach (SelectableObj obj in allSelectableObjs)
        {
            obj.ToggleColor(colorLevel);
        }
    }
}
