using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ChatManager : MonoBehaviourPunCallbacks
{
    #region Singleton

    public static ChatManager chatManager;

    void Awake()
    {
        chatManager = this;
    }

    #endregion

    List<Player> authors = new List<Player>();
    List<string> messages = new List<string>();

    [SerializeField]
    ChatPanel panel;

    public void AddMessage(string message)
    {
        if (PhotonNetwork.OfflineMode)
        {
            RPCSendMessage(PhotonNetwork.LocalPlayer, message);
        }
        else
        {
            photonView.RPC("RPCSendMessage", RpcTarget.All, PhotonNetwork.LocalPlayer, message);
        }
    }

    [PunRPC]
    public void RPCSendMessage(Player sender, string message)
    {
        authors.Add(sender);
        messages.Add(message);
        panel.InstantiateMessage(sender, message, true);
    }

    public List<Player> GetAuthors()
    {
        return authors;
    }

    public List<string> GetMessages()
    {
        return messages;
    }
}
