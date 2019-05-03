using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class FieldOfViewCollider : MonoBehaviour
{
    float dist;
    public virtual void Init(Vector3 vec)
    {
        Vector3 tmp = vec;
        if (GetComponentInParent<Radar>() != null)
        {
            tmp.x = SkilltreeManager.manager.radarRange;
            tmp.z = SkilltreeManager.manager.radarRange;
        }

        SetRange(tmp);
    }

    public void SetRange(Vector3 vec)
    {
        dist = vec.x * ((GetComponentInParent<MovableUnit>() != null) ? 2.4f:5);
        transform.localScale = vec;
    }

    public Vector3 GetRange()
    {
        return transform.localScale;
    }

    List<Collider> collidersList = new List<Collider>();
    protected void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, dist);

        for (int i = colliders.Length-1; i >= 0; i--)
        {
            if (!collidersList.Contains(colliders[i]))
            {
                collidersList.Add(colliders[i]);
            }
        }

        for (int i = collidersList.Count - 1; i >= 0; i--)
        {
            if (Array.IndexOf(colliders, collidersList[i]) == -1)
            {
                OnExit(collidersList[i]);
                collidersList.RemoveAt(i);
            }
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            OnStay(colliders[i]);
        }
    }

    protected virtual void OnStay(Collider collision)
    {
        if (collision == null) return;
        if (GetComponentInParent<SelectableObj>().botIndex == -1 && collision.GetComponent<SelectableObj>() != null)
        {
            if (MultiplayerTools.GetTeamOf(collision.GetComponent<SelectableObj>()) != InstanceManager.instanceManager.GetTeam())
                collision.GetComponent<SelectableObj>().UnHide();
        }
    }

    protected virtual void OnExit(Collider collision)
    {
        if (collision == null) return;
        if (collision.GetComponent<SelectableObj>() != null && collision.GetComponent<SelectableObj>() != null)
        {
            if (MultiplayerTools.GetTeamOf(collision.GetComponent<SelectableObj>()) != InstanceManager.instanceManager.GetTeam())
                collision.GetComponent<SelectableObj>().Hide();
        }
    }
}
