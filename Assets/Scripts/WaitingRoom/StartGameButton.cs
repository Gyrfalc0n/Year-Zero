using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class StartGameButton : MonoBehaviourPunCallbacks {

    public void Activate()
    {
        GetComponent<Button>().interactable = true;
    }

    public void Deactivate()
    {
        GetComponent<Button>().interactable = false;
    }

    public bool IsActive()
    {
        return GetComponent<Button>().interactable;
    }

    public void MainMenuBack()
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
}
