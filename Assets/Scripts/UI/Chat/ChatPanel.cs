using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class ChatPanel : MonoBehaviour
{
    [SerializeField]
    Transform content;
    [SerializeField]
    GameObject chatEntryObj;
    [SerializeField]
    InputField input;

    [SerializeField]
    MessagePrefab messagePrefab;

    public void InstantiateMessage(Player sender, string message, bool disappear)
    {
        MessagePrefab tmp = Instantiate(messagePrefab, content);
        tmp.Init(sender, message, disappear);
    }

    public void HideEntry()
    {
        input.text = "";
        chatEntryObj.SetActive(true);
    }

    public void ShowEntry()
    {
        chatEntryObj.SetActive(true);
        input.Select();
    }

    public void Send()
    {
        ChatManager.chatManager.AddMessage(input.text);
    }
}
