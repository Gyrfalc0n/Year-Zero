using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerSettings : WaitingRoomSettings
{
    [SerializeField]
    Text pseudoText;

    protected override void InitPanel()
    {
        base.InitPanel();
        Transform playersList = GameObject.Find("PlayersList").transform;
        if (playersList.childCount > PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.LeaveRoom();
        }
        pseudoText.text = photonView.Owner.NickName;
        kickButton.SetActive(PhotonNetwork.IsMasterClient && !photonView.IsMine);
        readyText.SetActive(PhotonNetwork.IsMasterClient);
        notReadyText.SetActive(!PhotonNetwork.IsMasterClient);
    }

    public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        base.OnPhotonSerializeView(stream, info);
        if (stream.IsWriting)
        {
            stream.SendNext(pseudoText.text);
            stream.SendNext(readyText.activeInHierarchy);
            stream.SendNext(notReadyText.activeInHierarchy);
        }
        else
        {
            pseudoText.text = (string)stream.ReceiveNext();
            readyText.SetActive((bool)stream.ReceiveNext());
            notReadyText.SetActive((bool)stream.ReceiveNext());
        }
    }

    public override void Kick()
    {
        PhotonNetwork.CleanRpcBufferIfMine(photonView);
        PhotonNetwork.CloseConnection(photonView.Owner);
    }
}
