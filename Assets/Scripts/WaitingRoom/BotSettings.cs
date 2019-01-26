using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class BotSettings : MonoBehaviourPunCallbacks, IPunObservable
{
    public Dropdown raceDropdown;
    public Dropdown teamDropdown;
    public Dropdown colorDropdown;
    [SerializeField]
    GameObject kickButton;

    void Start()
    {
        InitPanel();
    }


    public void InitPanel()
    {
        Transform playersList = GameObject.Find("PlayersList").transform;
        transform.SetParent(playersList);
        transform.localScale = new Vector3(1, 1, 1);
        kickButton.SetActive(PhotonNetwork.IsMasterClient);
        if (photonView.IsMine)
        {
            raceDropdown.interactable = true;
            teamDropdown.interactable = true;
            colorDropdown.interactable = true;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(raceDropdown.value);
            stream.SendNext(teamDropdown.value);
            stream.SendNext(colorDropdown.value);
        }
        else
        {
            raceDropdown.value = (int)stream.ReceiveNext();
            teamDropdown.value = (int)stream.ReceiveNext();
            colorDropdown.value = (int)stream.ReceiveNext();
        }
    }

    public void Kick()
    {
        PhotonNetwork.Destroy(photonView);
    }
}
