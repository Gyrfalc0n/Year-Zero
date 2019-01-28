using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FieldOfViewCollider : MonoBehaviour
{
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
