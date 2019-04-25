using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour
{
    [SerializeField]
    GameObject obj;
    [SerializeField]
    Text text;

    [SerializeField]
    string[] tips;
    int index = 0;

    public void Show()
    {
        obj.SetActive(true);
        DisplayText(index);
    }

    public void Cancel()
    {
        obj.SetActive(false);
    }

    public void Next()
    {
        index++;
        if (index >= tips.Length)
            index = 0;
        DisplayText(index);
    }

    public void Previous()
    {
        index--;
        if (index < 0)
            index = tips.Length - 1;
        DisplayText(index);
    }

    void DisplayText(int i)
    {
        text.text = tips[i];
    }
}
