using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackerIEMSpell : Spell
{
    [SerializeField]
    LayerMask groundLayer;

    public override void Effect()
    {
        SendRay();
    }

    void SendRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, groundLayer))
        {
            if (Vector3.Distance(associatedUnit.transform.position, hit.point) < 3)
            {
                //Create IEM
            }
        }
    }
}
