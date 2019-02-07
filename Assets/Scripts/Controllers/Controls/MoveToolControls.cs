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
            GetComponent<FormationSystem>().MoveSelection(hit.point);
        }
    }
}
