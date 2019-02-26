using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackerHackSpell : Spell
{
    [SerializeField]
    LayerMask interactableLayer;

    public override void Effect()
    {
        SendRay();
    }

    void SendRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, interactableLayer))
        {
            if (hit.collider.GetComponent<MovableUnit>() != null && InstanceManager.instanceManager.IsEnemy(hit.collider.GetComponent<MovableUnit>().photonView.Owner) && Vector3.Distance(associatedUnit.transform.position, hit.point) < 3)
            {
                hit.collider.GetComponent<MovableUnit>().Hack();
                return;
            }
            else
            {
                SendError();
            }
        }
        TemporaryMessage.temporaryMessage.Add("Bad Hacking");
    }
}
