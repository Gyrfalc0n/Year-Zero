using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementControls : PlayerControls
{
    public LayerMask groundLayer;

    public override void RightClick()
    {
        if (SelectUnit.selectUnit.selected[0].GetComponent<ProductionBuilding>() != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, groundLayer))
            {
                SelectUnit.selectUnit.selected[0].GetComponent<ProductionBuilding>().MoveBanner(hit.point);
            }
        }
        else if (!SelectUnit.selectUnit.InstantSelect())
        {
            MoveToMousePoint();
        }
    }

    public override void Update()
    {
        if (active && !MouseOverUI())
        {
            if (Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
            {
                LeftClick();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                RightClick();
            }
            SelectUnit.selectUnit.UpdateSelection();
        }
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
