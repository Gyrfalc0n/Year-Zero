using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class StartGameButton : MonoBehaviourPunCallbacks {

    public void Activate()
    {
        GetComponent<Button>().interactable = true;
    }

    public void Deactivate()
    {
        GetComponent<Button>().interactable = false;
    }

    public void MainMenuBack()
    {
        PhotonNetwork.LeaveRoom();
    }
}
