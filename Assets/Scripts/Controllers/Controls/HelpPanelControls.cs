using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPanelControls : PlayerControls
{
    [SerializeField]
    HelpMenu obj;

    public override void Init()
    {
        Debug.Log("helpinit");
        obj.Show();
    }

    public override void RightClick()
    {
        //parent : Cancel()
    }

    public override void Update()
    {
        if (!CanUpdate()) return;

        CheckMenu();
    }

    public override void Cancel()
    {
        base.Cancel();
        obj.Cancel();
    }

    void CheckMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.F9))
        {
            Debug.Log("helpcancel");
            Cancel();
        }
    }
}
