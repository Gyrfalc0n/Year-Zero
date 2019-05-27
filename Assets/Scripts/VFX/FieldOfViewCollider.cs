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

    Collider[] colliders = new Collider[0];
    protected void Update()
    {
        Collider[] tmpcolliders = Physics.OverlapSphere(transform.position, dist);

        for (int i = colliders.Length - 1; i >= 0; i--)
        {
            if (Array.IndexOf(tmpcolliders, colliders[i]) == -1)
            {
                OnExit(colliders[i]);
            }
        }

        for (int i = 0; i < tmpcolliders.Length; i++)
        {
            OnStay(tmpcolliders[i]);
        }
        colliders = tmpcolliders;
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
