using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatPanelControls : PlayerControls
{
    [SerializeField]
    ChatPanel obj;

    public override void Init()
    {
        obj.ShowEntry();
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
        obj.HideEntry();
    }

    void CheckMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cancel();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!obj.InputEmpty())
            {
                obj.Send();
                Cancel();
            }
            else
            {
                obj.ActivateInputField();
            }
        }
    }
}
