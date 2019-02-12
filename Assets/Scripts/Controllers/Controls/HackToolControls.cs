using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class HackToolControls : PlayerControls
{
    [SerializeField]
    LayerMask interactableLayer;
    [SerializeField]
    TemporaryMessage tooFar;

    public override void LeftClick()
    {
        SendRay();
        Cancel();
    }

    void SendRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, interactableLayer))
        {
            if (hit.collider.GetComponent<MovableUnit>() != null && Vector3.Distance(SelectUnit.selectUnit.selected[SelectUnit.selectUnit.underSelected].transform.position, hit.point) < 3)
            {
                hit.collider.GetComponent<MovableUnit>().Hack();
            }
            else
            {
                tooFar.Activate();
            }
        }
    }
}
