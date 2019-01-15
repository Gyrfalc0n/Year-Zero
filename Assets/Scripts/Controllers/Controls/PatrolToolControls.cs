using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolToolControls : PlayerControls
{
    [SerializeField]
    LayerMask groundLayer;

    public override void LeftClick()
    {
        InitToolPatrol();
        Cancel();
    }

    void InitToolPatrol()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, groundLayer))
        {
            foreach (SelectableObj obj in SelectUnit.selectUnit.selected)
            {
                if (obj.GetComponent<MovableUnit>() != null)
                {
                    obj.GetComponent<MovableUnit>().Patrol(obj.transform.position, hit.point, 0.5f);
                }
            }
        }
    }
}
