using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ResourceUnit : Interactable
{
    protected int resourceIndex;
    protected float resources;

    public float TakeResource(float val)
    {
        float res;
        if (resources - val < 0)
        {
            res = resources;
            resources = 0;
        }
        else
        {
            res = val;
            resources -= val;
        }
        return res;
    }

    public int GetResourceIndex()
    {
        return resourceIndex;
    }

    public bool StillResource()
    {
        return resources > 0;
    }

    public virtual void OnNoMoreResources() { }

    protected void SetResources(float value)
    {
        resources = value;
    }

    protected void Destroy()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}