using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    MessagePrefab messagePrefab;

    private void Start()
    {
        manager = ChatManager.chatManager;
        contentRect = content.GetComponent<RectTransform>();
    }

    public void ShowPanel()
    {
        obj.SetActive(true);
        scrollbar.value = 0.000000001f;
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta[0], 20);
        for (int i = manager.GetMessages().Count - 1; i >= 0; i--)
        {
            MessagePrefab tmp = Instantiate(messagePrefab, content);
            tmp.Init(manager.GetAuthors()[i], manager.GetMessages()[i], false);
            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta[0], contentRect.sizeDelta[1] + content.GetComponent<GridLayoutGroup>().cellSize[1]);
        }
        scrollbar.value = 0;
    }

    public void Cancel()
    {
        for (int i = content.childCount-1; i >= 0 ; i--)
        {
            Destroy(content.GetChild(i).gameObject);
        }
        obj.SetActive(false);
    }
}
