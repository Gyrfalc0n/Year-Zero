using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerSettings : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    Text pseudoText;
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
        if (playersList.childCount >= PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.LeaveRoom();
        }
        transform.SetParent(playersList);
        transform.localScale = new Vector3(1, 1, 1);
        pseudoText.text = photonView.Owner.NickName;
        kickButton.SetActive(PhotonNetwork.IsMasterClient && !photonView.IsMine);
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
            stream.SendNext(pseudoText.text);
        }
        else
        {
            raceDropdown.value = (int)stream.ReceiveNext();
            teamDropdown.value = (int)stream.ReceiveNext();
            colorDropdown.value = (int)stream.ReceiveNext();
            pseudoText.text = (string)stream.ReceiveNext();
        }
    }

    public void Kick()
    {
        PhotonNetwork.CleanRpcBufferIfMine(photonView);
        PhotonNetwork.CloseConnection(photonView.Owner);
    }
}
