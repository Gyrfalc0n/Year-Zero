using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairToolControls : PlayerControls
{
    public LayerMask interactableLayer;

    public override void LeftClick()
    {
        GoToRepair();
        Cancel();
    }

    public void GoToRepair()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, interactableLayer))
        {
            Collider obj = hit.collider;
            if (obj.GetComponent<ConstructedUnit>() != null && obj.GetComponent<ConstructedUnit>().photonView.IsMine && obj.GetComponent<ConstructedUnit>().GetLife() < obj.GetComponent<ConstructedUnit>().GetMaxlife())
            {
                SelectUnit.selectUnit.selected[SelectUnit.selectUnit.underSelected].GetComponent<BuilderUnit>().Repair(hit.collider.GetComponent<ConstructedUnit>());
            }
        }
    }
}
