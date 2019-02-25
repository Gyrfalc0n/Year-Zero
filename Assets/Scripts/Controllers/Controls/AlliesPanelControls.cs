using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliesPanelControls : PlayerControls
{
    [SerializeField]
    AlliesMenu obj;

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
        if (active)
        {
            CheckMenu();
        }
    }

    public override void Cancel()
    {
        base.Cancel();
        obj.Cancel();
    }

    void CheckMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cancel();
        }
    }
}
