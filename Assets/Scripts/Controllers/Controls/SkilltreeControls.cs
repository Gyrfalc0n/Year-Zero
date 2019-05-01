using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkilltreeControls : PlayerControls
{
    [SerializeField]
    SkilltreePanel obj;

    public override void Init()
    {
        obj.Show();
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
        obj.Hide();
    }

    void CheckMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.T) || !obj.Activated())
        {
            Cancel();
        }
    }
}
