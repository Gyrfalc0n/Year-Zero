﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

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
        if (GetComponentInParent<SelectableObj>().botIndex == -1 && collision.GetComponent<SelectableObj>() != null)
        {
            if (!PhotonNetwork.OfflineMode)
            {
                if (collision.GetComponent<SelectableObj>().botIndex == -1)
                {
                    if ((int)collision.gameObject.GetComponent<SelectableObj>().photonView.Owner.CustomProperties["Team"] != InstanceManager.instanceManager.GetTeam())
                    {
                        collision.gameObject.GetComponent<SelectableObj>().UnHide();
                    }
                }
                else if (collision.GetComponent<SelectableObj>().botIndex == -2 || InstanceManager.instanceManager.GetBot(collision.GetComponent<SelectableObj>().botIndex).GetTeam() != InstanceManager.instanceManager.GetTeam())
                {
                    collision.gameObject.GetComponent<SelectableObj>().UnHide();
                }
            }
            else
            {
                if (collision.GetComponent<SelectableObj>().botIndex != -1)
                {
                    if (collision.GetComponent<SelectableObj>().botIndex == -2 || InstanceManager.instanceManager.GetBot(collision.GetComponent<SelectableObj>().botIndex).GetTeam() != InstanceManager.instanceManager.GetTeam())
                    {
                        collision.gameObject.GetComponent<SelectableObj>().UnHide();
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<SelectableObj>() != null && collision.gameObject.GetComponent<SelectableObj>() != null)
        {
            if (!PhotonNetwork.OfflineMode)
            {
                if (collision.GetComponent<SelectableObj>().botIndex == -1)
                {
                    if ((int)collision.gameObject.GetComponent<SelectableObj>().photonView.Owner.CustomProperties["Team"] != InstanceManager.instanceManager.GetTeam())
                    {
                        collision.gameObject.GetComponent<SelectableObj>().Hide();
                    }
                }
                else if (collision.GetComponent<SelectableObj>().botIndex == -2 || InstanceManager.instanceManager.GetBot(collision.GetComponent<SelectableObj>().botIndex).GetTeam() != InstanceManager.instanceManager.GetTeam())
                {
                    collision.gameObject.GetComponent<SelectableObj>().Hide();
                }
            }
            else
            {
                if (collision.GetComponent<SelectableObj>().botIndex != -1)
                {
                    if (collision.GetComponent<SelectableObj>().botIndex == -2 || InstanceManager.instanceManager.GetBot(collision.GetComponent<SelectableObj>().botIndex).GetTeam() != InstanceManager.instanceManager.GetTeam())
                    {
                        collision.gameObject.GetComponent<SelectableObj>().Hide();
                    }
                }
            }
        }
    }
}
