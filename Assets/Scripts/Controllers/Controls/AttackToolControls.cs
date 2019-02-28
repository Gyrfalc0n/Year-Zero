using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackToolControls : PlayerControls
{
    [SerializeField]
    LayerMask interactableLayer;

    public override void LeftClick()
    {
        InitToolAttack();
        Cancel();
    }

    void InitToolAttack()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, interactableLayer))
        {
            SelectUnit.selectUnit.selected[SelectUnit.selectUnit.underSelected].GetComponent<MovableUnit>().Attack(hit.collider.GetComponent<DestructibleUnit>());
        }
    }
}
