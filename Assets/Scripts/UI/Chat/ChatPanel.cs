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

    public void InstantiateMessage(Player sender, string message)
    {
        MessagePrefab tmp = Instantiate(messagePrefab, content);
        tmp.Init(sender, message, true);
    }

    public void HideEntry()
    {
        input.text = "";
        chatEntryObj.SetActive(false);
    }

    public void ShowEntry()
    {
        chatEntryObj.SetActive(true);
        input.ActivateInputField();
    }

    public void Send()
    {
        ChatManager.chatManager.AddMessage(input.text);
    }

    public bool InputEmpty()
    {
        return input.text == "";
    }

    public void ActivateInputField()
    {
        input.ActivateInputField();
    }
}
