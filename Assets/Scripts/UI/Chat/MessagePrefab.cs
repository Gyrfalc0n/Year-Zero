using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MessagePrefab : MonoBehaviour
{
    [SerializeField]
    Text playerName;
    [SerializeField]
    Text message;

    float remainingTime = 5f;
    bool disappear;

    public void Init(Player sender, string message, bool disappear)
    {
        this.disappear = disappear;

        if (PhotonNetwork.OfflineMode)
        {
            playerName.color = Color.white;
            playerName.text = PlayerPrefs.GetString("PlayerName");
        }
        else
        {
            playerName.color = InstanceManager.instanceManager.GetPlayerColor(sender);
            playerName.text = sender.NickName;
        }
        this.message.text = message;
    }

    void Update()
    {
        if (disappear)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
