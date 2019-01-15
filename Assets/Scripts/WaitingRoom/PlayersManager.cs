using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PlayersManager : MonoBehaviourPunCallbacks {

    Hashtable customProp;
    [SerializeField]
    private StartGameButton startButton;
    [SerializeField]
    private Toggle readyToggle;

    [SerializeField]
    Transform playersList;

    private void Awake()
    {
        GameObject obj = PhotonNetwork.Instantiate("UI/WaittingRoom/PlayerSettingsPrefab", Vector3.zero, Quaternion.identity);
        obj.transform.SetParent(playersList);

        customProp = PhotonNetwork.LocalPlayer.CustomProperties;
        if (!PhotonNetwork.IsMasterClient)
        {
            readyToggle.gameObject.SetActive(true);
            startButton.gameObject.SetActive(false);
            customProp.Add("IsReady", false);

        }
        else
        {
            readyToggle.gameObject.SetActive(false);
            startButton.gameObject.SetActive(true);
            customProp.Add("IsReady", true);
        }
        PhotonNetwork.LocalPlayer.SetCustomProperties(customProp);
        photonView.RPC("CheckIsReady", RpcTarget.MasterClient);
    }

    [SerializeField]
    private List<Vector3> coords = new List<Vector3>();

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
        customProp["IsReady"] = val;
        PhotonNetwork.LocalPlayer.SetCustomProperties(customProp);
        photonView.RPC("CheckIsReady", RpcTarget.MasterClient);
    }

    [PunRPC]
    public void CheckIsReady()
    {
        bool allReady = true;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if ((bool)player.CustomProperties["IsReady"] == false)
                allReady = false;
        }

        if (allReady)
            startButton.Activate();
        else
            startButton.Deactivate();
    }
}
