using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class BotSettings : WaitingRoomSettings
{
    protected override void InitPanel()
    {
        base.InitPanel();
        kickButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void Kick()
    {
        PhotonNetwork.Destroy(photonView);
    }
}
