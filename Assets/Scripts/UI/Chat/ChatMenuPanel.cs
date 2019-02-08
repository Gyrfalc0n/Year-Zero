using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class ChatMenuPanel : MonoBehaviour
{
    ChatManager manager;

    [SerializeField]
    GameObject obj;
    [SerializeField]
    Scrollbar scrollbar;
    [SerializeField]
    Transform content;
    RectTransform contentRect;

    [SerializeField]
    InputField entry;

    [SerializeField]
    MessagePrefab messagePrefab;

    void Start()
    {
        manager = ChatManager.chatManager;
        contentRect = content.GetComponent<RectTransform>();
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta[0], 20);
    }

    public void ShowPanel()
    {
        obj.SetActive(true);
        entry.ActivateInputField();
    }

    public void InstantiateMessage(Player sender, string message)
    {
        MessagePrefab tmp = Instantiate(messagePrefab, content);
        tmp.Init(sender, message, false);
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta[0], contentRect.sizeDelta[1] + content.GetComponent<GridLayoutGroup>().cellSize[1]);
        scrollbar.value = 0.000000001f;
    }

    public void Cancel()
    {
        obj.SetActive(false);
    }

    public void Send()
    {
        if (entry.text != "")
        {
            manager.AddMessage(entry.text);
            entry.text = "";
            entry.ActivateInputField();
        }
            
    }
}
