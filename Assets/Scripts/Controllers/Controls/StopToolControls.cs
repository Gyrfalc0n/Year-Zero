using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopToolControls : PlayerControls
{
    public override void LeftClick()
    {
        StopSelection();
    }

    public void StopSelection()
    {
        foreach (SelectableObj unit in SelectUnit.selectUnit.selected)
        {
            if (unit.GetComponent<MovableUnit>() != null)
            {
                unit.GetComponent<MovableUnit>().ResetAction();
            }
        }
    }
}
