using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FieldOfViewCollider : MonoBehaviour
{
    public void SetRange(Vector3 vec)
    {
        transform.localScale = vec;
    }

    public Vector3 GetRange()
    {
        return transform.localScale;
    }

    private void Start()
    {
        Vector3 tmp = GetRange();
        if (GetComponentInParent<Radar>() != null)
        {
            tmp.x = SkilltreeManager.manager.radarRange;
            tmp.z = SkilltreeManager.manager.radarRange;
            SetRange(tmp);
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.GetComponent<SelectableObj>() != null)
        {
            if (!PhotonNetwork.OfflineMode)
            {
                if ((int)collision.gameObject.GetComponent<SelectableObj>().photonView.Owner.CustomProperties["Team"] != InstanceManager.instanceManager.GetTeam())
                {
                    collision.gameObject.GetComponent<SelectableObj>().UnHide();
                }
            }
            else
            {
                //Need bot system
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<SelectableObj>() != null)
        {
            if (!PhotonNetwork.OfflineMode)
            {
                if ((int)collision.gameObject.GetComponent<SelectableObj>().photonView.Owner.CustomProperties["Team"] != InstanceManager.instanceManager.GetTeam())
                {
                    collision.gameObject.GetComponent<SelectableObj>().Hide();
                }
            }
            else
            {
                //Need bot system
            }
        }
    }
}
