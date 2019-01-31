using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuControls : PlayerControls
{
    [SerializeField]
    ChatPanel obj;

    public void Init()
    {
        obj.ShowEntry();
    }

    public override void RightClick()
    {
        //parent : Cancel()
    }

    public override void Update()
    {
        if (active)
        {
            CheckMenu();
        }
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
            obj.Send();
            Cancel();
        }
    }
}
