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
            if (hit.collider.GetComponent<DestructibleUnit>() != null && InstanceManager.instanceManager.IsEnemy(hit.collider.GetComponent<DestructibleUnit>().photonView.Owner))
            {
                SelectUnit.selectUnit.selected[SelectUnit.selectUnit.underSelected].GetComponent<MovableUnit>().Attack(hit.collider.GetComponent<DestructibleUnit>());
            }
        }
    }
}
