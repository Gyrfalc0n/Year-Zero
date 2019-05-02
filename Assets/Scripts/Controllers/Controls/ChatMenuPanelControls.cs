using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatMenuPanelControls : PlayerControls
{
    [SerializeField]
    ChatMenuPanel obj;

    public override void Init()
    {
        obj.ShowPanel();
    }

    public override void RightClick()
    {
        //parent : Cancel()
    }

    public override void Update()
    {
        if (!isActive) return;

        CheckMenu();
    }

    public override void Cancel()
    {
        base.Cancel();
        obj.Cancel();
    }

    void CheckMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.F12))
        {
            Cancel();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            obj.Send();
        }
    }
}
