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
    public GameObject readyText;
    public GameObject notReadyText;
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
        readyText.SetActive(PhotonNetwork.IsMasterClient);
        notReadyText.SetActive(!PhotonNetwork.IsMasterClient);
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
            stream.SendNext(readyText.activeInHierarchy);
            stream.SendNext(notReadyText.activeInHierarchy);
        }
        else
        {
            raceDropdown.value = (int)stream.ReceiveNext();
            teamDropdown.value = (int)stream.ReceiveNext();
            colorDropdown.value = (int)stream.ReceiveNext();
            pseudoText.text = (string)stream.ReceiveNext();
            readyText.SetActive((bool)stream.ReceiveNext());
            notReadyText.SetActive((bool)stream.ReceiveNext());
        }
    }

    public void Kick()
    {
        PhotonNetwork.CleanRpcBufferIfMine(photonView);
        PhotonNetwork.CloseConnection(photonView.Owner);
    }

    public void Lock()
    {
        raceDropdown.interactable = false;
        teamDropdown.interactable = false;
        colorDropdown.interactable = false;
        readyText.SetActive(true);
        notReadyText.SetActive(false);
    }

    public void Unlock()
    {
        raceDropdown.interactable = true;
        teamDropdown.interactable = true;
        colorDropdown.interactable = true;
        readyText.SetActive(false);
        notReadyText.SetActive(true);
    }
}
