using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HelpPanel : MonoBehaviour
{
    [SerializeField]
    GameObject obj;

    [SerializeField]
    Text text;

    public void Call(string text)
    {
        if (PlayerPrefs.GetInt("helpBubble") == 1)
        {
            obj.SetActive(true);
            this.text.text = text;
        }
    }

    public void Hide()
    {
        obj.SetActive(false);
    }
}
