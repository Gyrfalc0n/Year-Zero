using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendHelp : MonoBehaviour
{
    [TextArea(3, 5)]
    [SerializeField]
    string description;

    public void CallHelpPanel()
    {
        HelpPanel.helpPanel.Call(description);
    }

    public void Hide()
    {
        HelpPanel.helpPanel.Hide();
    }
}
