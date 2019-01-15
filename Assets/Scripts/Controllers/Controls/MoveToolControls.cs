using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToolControls : PlayerControls
{
    public LayerMask groundLayer;

    public override void LeftClick()
    {
        MoveToMousePoint();
        Cancel();
    }

    public void MoveToMousePoint()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, groundLayer))
        {
            MoveSelection(hit.point);
        }
    }

    public void MoveSelection(Vector3 newPos)
    {
        foreach (SelectableObj unit in SelectUnit.selectUnit.selected)
        {
            if (unit.GetComponent<MovableUnit>() != null)
                unit.GetComponent<MovableUnit>().SetDestination(newPos, 1);
        }
    }
}
