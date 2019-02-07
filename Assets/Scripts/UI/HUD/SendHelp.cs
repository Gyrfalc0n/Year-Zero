using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendHelp : MonoBehaviour
{
    [TextArea(3, 5)]
    [SerializeField]
    string description;

    [SerializeField]
    HelpPanel panel;

    public void CallHelpPanel()
    {
        panel.Call(description);
    }

    public void Hide()
    {
        panel.Hide();
    }
}
