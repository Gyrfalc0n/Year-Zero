using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class InstanceManager : MonoBehaviourPunCallbacks {

    #region Singleton

    public static InstanceManager instanceManager;
    public bool offlineMode;

    void Awake()
    {
        instanceManager = this;
        PhotonNetwork.OfflineMode = offlineMode;
    }

    #endregion

    private void Start()
    {
        Vector3 myCoords;
        if (!PhotonNetwork.OfflineMode)
            myCoords = (Vector3)PhotonNetwork.LocalPlayer.CustomProperties["MyCoords"];
        else
            myCoords = new Vector3(0, 0, 0);
        PlayerManager.playerManager.AddHome(InstantiateUnit("Buildings/TownHall/TownHallUnit", new Vector3(myCoords.x+2, 0.51f, myCoords.y+2), Quaternion.Euler(0, 0, 0)).GetComponent<TownHall>());
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
}
